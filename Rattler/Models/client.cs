using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattler.Models
{
    [Table("clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
   
        [Required]
        public string Email { get; set; }
        public int maxCount { get; set; }
        public int role { get; set; }
        public DateTime LastUpdate { get; set; }
        public long ChatId { get; set; }
        public string key { get; set; }

        public bool isActive { get; set; }
        public virtual ICollection<Accaunts> accaunts{ get; set; }
        public Client()
        {
            accaunts = new List<Accaunts>();
        }

        public Client(string login, string password, string email)
        {
            Login = login;
            //SetPassword(password);
            Password = password;
            Email = email;
            accaunts= new List<Accaunts>();
        }
        //static public string ComputeSha256Hash(string rawData)
        //{
        //    // Create a SHA256   
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // ComputeHash - returns byte array  
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        //        // Convert byte array to a string   
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < bytes.Length; i++)
        //        {
        //            builder.Append(bytes[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}
    
    }

}
