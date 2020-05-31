using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattler.Controller
{
    public class MyDbContext:DbContext
    {
         public MyDbContext():base("RattlerConnection")
        {

        }
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new MyDbInitialize());
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.Accaunts> Accaunts { get; set; }

    }
}
