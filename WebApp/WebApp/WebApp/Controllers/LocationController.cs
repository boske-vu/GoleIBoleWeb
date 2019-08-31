using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;


namespace WebApp.Controllers
{
    [RoutePrefix("api/Location")]
    public class LocationController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private LokacijaVozilaHub hub;
        public IUnitOfWork Db { get; set; }

        public LocationController(LokacijaVozilaHub hub, IUnitOfWork db)
        {
            this.hub = hub;
            this.Db = db;
        }


        //[Route("api/Location/StaniceZaHub")]
        public IHttpActionResult StaniceZaHub(LinijaZaHub lin)
        {
            List<BusLine> listaLinija = Db.busLineRepository.GetAll().ToList();
            BusLine linija = null;

            foreach (var l in listaLinija)
            {
                if (l.SerialNumber.ToString() == lin.imeLinije)
                {
                    linija = l;
                    break;
                }
            }

            List<Station> listaStanica = linija.Stations.ToList();

            hub.DodajStanice(listaStanica);
            return Ok("Pronadjene su stanice");
        }
    }
}
