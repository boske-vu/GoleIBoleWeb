using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class RedVoznjeController : ApiController
    {
        public IUnitOfWork Db { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public RedVoznjeController() { }

        public RedVoznjeController(IUnitOfWork db)
        {
            this.Db = db;
        }


        //[Route("api/RedVoznje/getAll")]
        //[HttpGet]
        //public IHttpActionResult GetAll()
        //{
        //    var lines = Db.red.GetAll();

        //    return Ok(lines);
        //}


        [ResponseType(typeof(string))]
        [Route("api/RedVoznje/GetPolasci/{selectedLinija}/{selectedDan}/{selectedTeritorija}")]
        public IHttpActionResult GetPolasci(int selectedLinija,int selectedDan,int selectedTeritorija)
        {
            List<DayType> dani = new List<DayType>();
            DayType ret1 = new DayType();
            dani = Db.dayTypeRepository.GetAll().ToList();      //preuzeti dani-radni dan,subota,nedelja
            //int selectedDans = Int32.Parse(selectedDan);
            List<Timetable> putanja = new List<Timetable>();    //raspored-sve je ovde uklopljeno
            Timetable ret2 = new Timetable();
            putanja = Db.timeTableRepository.GetAll().ToList();

            string ret = "";                 //vremena
            List<BusLine> line = new List<BusLine>();

            line = Db.busLineRepository.GetAll().ToList();

            foreach (Timetable ttt in putanja)
            {
                if (ttt.TimetableTypeId== selectedTeritorija && ttt.DayTypeId==selectedDan && ttt.BusLineId== selectedLinija)       //izabrali smo izmedju gradskog i prigradskog
                {
                    ret = ttt.Times;
                    break;
                }
            }

            if (ret != null)
                return Ok(ret);
            else
                return StatusCode(HttpStatusCode.Accepted);

        }
    }
}
