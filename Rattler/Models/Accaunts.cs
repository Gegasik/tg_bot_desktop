using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rattler.Models
{
    [Table("accaunts")]
    public class Accaunts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string link { get; set; }
        public virtual ICollection<Client> Clients{ get; set; }
       public Accaunts()
        {
            Clients = new List<Client>();
        }
    }
}
