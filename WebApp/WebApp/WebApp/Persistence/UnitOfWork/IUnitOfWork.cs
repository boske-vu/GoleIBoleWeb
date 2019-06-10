using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
      
        IBusLineRepository busLineRepository { get; set; }
        IDayTypeRepository dayTypeRepository { get; set; }
        IPriceListRepository priceListRepository { get; set; }
        IStationRepository stationRepository { get; set; }
        ITicketPriceRepository ticketPriceRepository { get; set; }
        ITicketRepository ticketRepository { get; set; }
        ITicketTypeRepository ticketTypeRepository { get; set; }
        ITimeTableRepository timeTableRepository { get; set; }
        IUserTypeRepository userTypeRepository { get; set; }
        IVehicleRepository vehicleRepository { get; set; }

        int Complete();

    }
}
