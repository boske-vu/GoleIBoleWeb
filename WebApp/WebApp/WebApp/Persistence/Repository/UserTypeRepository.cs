using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class UserTypeRepository : Repository<UserType, int>, IUserTypeRepository
    {
        private ApplicationDbContext appDbContext { get { return context as ApplicationDbContext; } }

        public UserTypeRepository(DbContext context) : base(context)
        {

        }

        public int GetIdFromString(string value)
        {
            return appDbContext.UserType.Where(t => t.Name.Equals("Student")).FirstOrDefault().Id;
        }
    }
}