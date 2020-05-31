using Rattler.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattler.Controller
{
     class MyDbInitialize: DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {

            //String pass = User.ComputeSha256Hash("1111");
            Client user = new Client { Id = 1, Login = "Adminee", Password = "1111", Email = "tansteppp@gmail.com"};
            context.Clients.Add(user);
            context.SaveChanges();
            //base.Seed(context);
        }
    }
}
