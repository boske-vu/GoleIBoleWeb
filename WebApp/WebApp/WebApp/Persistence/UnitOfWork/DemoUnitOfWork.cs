using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Models;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        [Dependency]
        public IBusLineRepository busLineRepository { get; set; }
        [Dependency]
        public IDayTypeRepository dayTypeRepository { get; set; }
        [Dependency]
        public IPriceListRepository priceListRepository { get; set; }
        [Dependency]
        public IStationRepository stationRepository { get; set; }
        [Dependency]
        public ITicketPriceRepository ticketPriceRepository { get; set; }
        [Dependency]
        public ITicketRepository ticketRepository { get; set; }
        [Dependency]
        public ITicketTypeRepository ticketTypeRepository { get; set; }
        [Dependency]
        public ITimeTableRepository timeTableRepository { get; set; }
        [Dependency]
        public IUserTypeRepository userTypeRepository { get; set; }
        [Dependency]
        public IVehicleRepository vehicleRepository { get; set; }

        //[Dependency]
        //public List<PriceList> BusLines { get; set; }

        //[Dependency]
        //public List<PriceList> DayTypes { get; set; }

        //[Dependency]
        //public List<PriceList> Pricelists { get; set; }

        //[Dependency]
        //public List<PriceList> Stations { get; set; }

        //[Dependency]
        //public List<PriceList> Tickets { get; set; }

        //[Dependency]
        //public List<PriceList> TicketPrices { get; set; }

        //[Dependency]
        //public List<TicketPrice> TicketTypes { get; set; }
        //[Dependency]
        //public List<PriceList> TimeTables { get; set; }

        //[Dependency]
        //public List<PriceList> TameTableTypes { get; set; }

        //[Dependency]
        //public List<TicketPrice> UserTypes { get; set; }

        //[Dependency]
        //public List<TicketPrice> Vehicles { get; set; }






        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}