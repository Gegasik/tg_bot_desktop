using Rattler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rattler.Controller
{
    class RegistrationController
    {


        public bool CheckIfUserExists(Client user)
        {
            MyDbContext context = new MyDbContext();
            if (context.Clients.FirstOrDefault(User => User.Login == user.Login) != null)
            {
                return true;
            }
            else if (context.Clients.FirstOrDefault(User => User.Email == user.Email) != null)
            {
                return true;
            }
            return false;
        }

        public bool Register(Client user)
        {
            try
            {
                if (!CheckIfUserExists(user))
                {
                    MyDbContext context = new MyDbContext();

                    user.Password = hashUser(user.Password);
                    context.Clients.Add(user);
                    context.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        private string hashUser(string pass)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}
