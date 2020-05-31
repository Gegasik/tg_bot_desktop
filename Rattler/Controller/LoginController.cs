using Rattler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattler.Controller
{
    class LoginController
    {
        public Client Login(Client user)
        {
            try
            {
                MyDbContext context = new MyDbContext();
                Client foundUser = context.Clients.FirstOrDefault(User => User.Login == user.Login);
                if (foundUser != null)
                {
                    if (foundUser.Password == user.Password)
                    {
                        return foundUser;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
