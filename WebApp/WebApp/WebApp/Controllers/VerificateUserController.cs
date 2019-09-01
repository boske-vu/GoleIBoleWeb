
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Persistence;
using System.Web;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebApp.Controllers
{
    [Authorize]
    public class VerificateUserController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IUnitOfWork Db { get; set; }
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public VerificateUserController() { }

        public VerificateUserController(IUnitOfWork db)
        {
            this.Db = db;

        }

        [AllowAnonymous]
        [ResponseType(typeof(ApplicationUser))]
        [Route("api/VerificateUser/ReturnUsers")]
        public IHttpActionResult GetUsers()
        {
            //ApplicationUser ret = new ApplicationUser();
            List<ApplicationUser> ret = new List<ApplicationUser>();

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            List<ApplicationUser> list = userManager.Users.ToList();

            foreach (ApplicationUser a in list)
            {
                if (a.VerificateAcc == 0)
                {
                    ret.Add(a);
                }
            }

            return Ok(ret);
        }

        // GET: api/UserVerification/SelectedUser/{username}
        [Authorize(Roles = "Controller")]
        [ResponseType(typeof(ApplicationUser))]
        [Route("api/VerificateUser/SelectedUser/{id}")]
        public IHttpActionResult GetSelectedUser(string id)
        {
            ApplicationUser ret = new ApplicationUser();

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            List<ApplicationUser> list = userManager.Users.ToList();

            foreach (ApplicationUser a in list)
            {
                if (a.Id.Equals(id))
                {
                    ret = a;
                    break;
                }
            }

            if (ret != null)
                return Ok(ret);
            else
                return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("api/VerificateUser/DownloadPicture/{id}")]
        public IHttpActionResult DownloadPicture(string id)
        {

            ApplicationUser ret = new ApplicationUser();

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            List<ApplicationUser> list = userManager.Users.ToList();

            foreach (ApplicationUser a in list)
            {
                if (a.Id.Equals(id))
                {
                    ret = a;
                    break;
                }
            }

            if (ret == null)
            {
                return BadRequest("User doesn't exists.");
            }

            if (ret.ImageUrl == null)
            {
                return BadRequest("Picture doesn't exists.");
            }


            var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + ret.ImageUrl);

            FileInfo fileInfo = new FileInfo(filePath);
            string type = fileInfo.Extension.Split('.')[1];
            byte[] data = new byte[fileInfo.Length];

            HttpResponseMessage response = new HttpResponseMessage();
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(data);
                response.Content.Headers.ContentLength = data.Length;

            }

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/png");

            return Ok(data);

        }

        // GET: api/UserVerification/Users
        [Authorize(Roles = "Controller")]
        [HttpGet]
        [ResponseType(typeof(ApplicationUser))]
        [Route("api/VerificateUser/Odluka/{id}/{odluka}")]
        public IHttpActionResult Odluka(string id, string odluka)
        {
            ApplicationUser ret = new ApplicationUser();

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            List<ApplicationUser> list = userManager.Users.ToList();

            foreach (ApplicationUser a in list)
            {
                if (a.UserName.Equals(id))
                {
                    ret = a;
                    break;
                }
            }

            if (ret == null)
                return StatusCode(HttpStatusCode.BadRequest);

            if (odluka.Equals("prihvati"))
            {
                ret.VerificateAcc = 1;
            }
            else if (odluka.Equals("odbij"))
            {
                ret.VerificateAcc = 2;
            }

            db.Entry(ret).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return Ok(ret);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
