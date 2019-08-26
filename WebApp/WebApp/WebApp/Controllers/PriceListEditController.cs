using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class PriceListEditController : ApiController
    {
            private ApplicationDbContext db = new ApplicationDbContext();
            public IUnitOfWork Db { get; set; }

            public PriceListEditController() { }

            public PriceListEditController(IUnitOfWork db)
            {
                this.Db = db;
            }

            [Route("api/ticketPriceEdit/ticketPriceEditGetPrice/{ticketTypeId}")]
            [HttpGet]
            public IHttpActionResult GetPrice(int ticketTypeId)
            {
                var price = Db.ticketPriceRepository.Find(x => x.TicketTypeId == ticketTypeId).FirstOrDefault().Price;

                return Ok(price);
            }

            [HttpPost]
            [Route("api/ticketPriceEdit/UpdateTicketPrice/{ticketTypeId}/{price}")]
            public IHttpActionResult UpdateTimetable(int ticketTypeId, int price)
            {
                TicketPrice ticket = new TicketPrice();
                ticket = Db.ticketPriceRepository.Find(x => x.TicketTypeId.Equals(ticketTypeId)).FirstOrDefault();
                ticket.Price = price;
                db.Entry(ticket).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }

                return Ok("uspesno");
            }
        }

}
