using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class StationEditController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IUnitOfWork Db { get; set; }

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public StationEditController() { }

        public StationEditController(IUnitOfWork db)
        {
            this.Db = db;
        }

        // GET: api/StationEdit/GetStations
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(List<string>))]
        [Route("api/StationEdit/GetStations")]
        public IHttpActionResult GetStations()
        {
            List<string> ret = new List<string>();
            List<Station> stations = new List<Station>();

            stations = Db.stationRepository.GetAll().ToList();

            foreach (Station l in stations)
            {
                ret.Add(l.Name);
            }

            return Ok(ret);
        }

        // GET: api/StationEdit/SelectedLine/{serial}
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(BusLine))]
        [Route("api/StationEdit/SelectedLine/{serial}")]
        public IHttpActionResult GetSelectedLine(string serial)
        {
            List<BusLine> line = new List<BusLine>();
            BusLine ret = new BusLine();
            line = Db.busLineRepository.GetAll().ToList();
            int serialNumber = Int32.Parse(serial);

            foreach (BusLine l in line)
            {
                if (l.SerialNumber.Equals(serialNumber))
                {
                    ret = l;
                    break;
                }
            }

            if (ret != null)
                return Ok(ret);
            else
                return StatusCode(HttpStatusCode.BadRequest);

        }

        // GET: api/StationEdit/GetStations/{serial}
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(List<string>))]
        [Route("api/StationEdit/GetLines/{name}")]
        public IHttpActionResult GetLines(string name)
        {
            List<Station> stations = new List<Station>();
            Station ret = new Station();
            stations = Db.stationRepository.GetAll().ToList();

            foreach (Station l in stations)
            {
                if (l.Name.Equals(name))
                {
                    ret = l;
                    break;
                }
            }

            List<BusLine> list = Db.busLineRepository.Find(x => x.Stations.Any(y => y.Id.Equals(ret.Id))).ToList();

            List<string> returnsList = new List<string>();

            foreach (BusLine s in list)
            {
                returnsList.Add(s.SerialNumber.ToString());
            }

            if (returnsList != null)
                return Ok(returnsList);
            else
                return StatusCode(HttpStatusCode.BadRequest);

        }

        // GET: api/StationEdit/GetSelectedStation/{name}
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Station))]
        [Route("api/StationEdit/GetSelectedStation/{name}")]
        public IHttpActionResult GetSelectedStation(string name)
        {
            Station stanica = db.Station.Where(x => x.Name == name).FirstOrDefault();
            if (stanica == null)
            {
                return NotFound();
            }

            //List<Station> stations = new List<Station>();
            //Station ret = new Station();
            //stations = Db.stationRepository.GetAll().ToList();

            //foreach (Station l in stations)
            //{
            //    if (l.Name.Equals(name))
            //    {
            //        ret = l;
            //        break;
            //    }
            //}

            if (stanica != null)
                return Ok(stanica);
            else
                return StatusCode(HttpStatusCode.BadRequest);

        }

        // POST: api/StationEdit/UpdateStation
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(string))]
        [Route("api/StationEdit/UpdateStation")]
        public IHttpActionResult UpdateStation(Station station)
        {
            bool postoji = false;
            List<Station> provera = new List<Station>();
            provera = Db.stationRepository.GetAll().ToList();

            foreach (Station s in provera)
            {
                if (s.Name.Equals(station.Name) && !s.Id.Equals(station.Id))
                {
                    postoji = true;
                    break;
                }
            }

            if (!postoji)
            {
                db.Entry(station).State = EntityState.Modified;

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
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // GET: api/StationEdit/GetAllLines
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(List<string>))]
        [Route("api/StationEdit/GetAllLines")]
        public IHttpActionResult GetAllLines()
        {
            List<BusLine> lines = new List<BusLine>();
            List<string> ret = new List<string>();

            lines = Db.busLineRepository.GetAll().ToList();

            foreach (BusLine l in lines)
            {
                ret.Add(l.SerialNumber.ToString());
            }

            if (ret != null)
                return Ok(ret);
            else
                return StatusCode(HttpStatusCode.BadRequest);

        }

        // POST: api/StationEdit/AddStation
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(string))]
        [Route("api/StationEdit/AddStation")]
        public IHttpActionResult AddStation(AddStation station)
        {
            bool postoji = false;
            List<Station> provera = new List<Station>();
            provera = Db.stationRepository.GetAll().ToList();

            foreach (Station l in provera)
            {
                if (l.Name.Equals(station.Name))
                {
                    postoji = true;
                    break;
                }
            }

            if (!postoji)
            {
                Station ret = new Station();

                ret.Name = station.Name;
                ret.Address = station.Address;
                ret.X = station.X;
                ret.Y = station.Y;

                List<BusLine> temp = new List<BusLine>();
                temp = Db.busLineRepository.GetAll().ToList();
                List<BusLine> lines = new List<BusLine>();

                foreach (BusLine s in temp)
                {
                    if (station.LinesAdd.Contains(s.SerialNumber.ToString()))
                    {
                        lines.Add(s);
                    }
                }

                ret.Lines = lines;

                Db.stationRepository.Add(ret);
                Db.Complete();

                return Ok("uspesno");
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/StationEdit/DeleteSelectedStation/{serial}
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(string))]
        [Route("api/StationEdit/DeleteSelectedStation/{id}")]
        public IHttpActionResult DeleteSelectedStation(string id)
        {
            //List<Station> stations = new List<Station>();
            //Station ret = new Station();
            //stations = Db.stationRepository.GetAll().ToList();

            //foreach (Station l in stations)
            //{
            //    if (l.Id.Equals(id))
            //    {
            //        ret = l;
            //        break;
            //    }
            //}

            Station stanica = db.Station.Where(x => x.Name == id).FirstOrDefault();
            if(stanica == null)
            {
                return NotFound();
            }

            db.Station.Remove(stanica);
            db.SaveChanges();

            return Ok("uspesno");

            //if (ret != null)
            //{
            //    Db.stationRepository.Remove(ret);
            //    //db.Station.Remove(ret);

            //    try
            //    {
            //        db.SaveChanges();
            //    }
            //    catch (DbUpdateConcurrencyException e)
            //    {
            //        return StatusCode(HttpStatusCode.BadRequest);
            //    }

            //    return Ok("uspesno");
            //}
            //else
            //    return StatusCode(HttpStatusCode.BadRequest);

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
