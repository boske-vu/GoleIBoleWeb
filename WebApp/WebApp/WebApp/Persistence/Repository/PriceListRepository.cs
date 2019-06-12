using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class PriceListRepository : Repository<Pricelist, int>, IPriceListRepository
    {
        public PriceListRepository(DbContext context) : base(context)
        {

        }
    }
}