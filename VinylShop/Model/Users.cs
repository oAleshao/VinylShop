using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Adres { get; set; }

        public virtual ICollection<Orders> userOrders { get; set; }

        public Users() 
        {
            userOrders = new List<Orders>();
        }

        public override string ToString()
        {
            return $"{FullName} {Email} {Login} {Password} {PhoneNumber} {Adres}";
        }
    }
}
