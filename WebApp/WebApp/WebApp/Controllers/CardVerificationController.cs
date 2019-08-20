using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;


namespace WebApp.Controllers
    {
        [Authorize]
        public class CardVerificationController : ApiController
        {
            private IUnitOfWork db;

            private const string LocalLoginProvider = "Local";
            private ApplicationUserManager _userManager;

            public CardVerificationController() { }

            public CardVerificationController(IUnitOfWork db)
            {
                this.db = db;
            }

            // GET: api/CardVerification/Check/{id}
            [Route("api/CardVerification/Check/{id}")]
            [HttpGet]
            [ResponseType(typeof(string))]
            [Authorize(Roles = "Controller")]
            public IHttpActionResult GetCard(int id)
            {
                Ticket ticket = db.ticketRepository.Find(x => x.Id.Equals(id)).FirstOrDefault();

                if (ticket != null)
                {
                    Pricelist pricelist = db.priceListRepository.Find(x => x.Id.Equals(ticket.PricelistId)).FirstOrDefault();
                    if (pricelist != null)
                    {
                        DateTime vaziOd = DateTime.Parse(pricelist.From);
                        DateTime vaziDo = DateTime.Parse(pricelist.To);
                        DateTime sada = DateTime.Now;
                        if (vaziOd < sada && sada < vaziDo)
                        {
                            return Ok("true");
                        }
                        else
                        {
                            return Ok("false");
                        }
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.BadRequest);
                    }
                }
                else
                    return StatusCode(HttpStatusCode.BadRequest);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
