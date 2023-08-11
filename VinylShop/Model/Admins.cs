using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class Admins
    {
        public int Id { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }

        public Admins() { }
        
    }
}
