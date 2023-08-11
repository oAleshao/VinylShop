using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace VinylShop.Model
{
    public class Admins
    {
        public int Id { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public bool IsEnabled { get; set; }

        public Admins() { }

        public override string ToString()
        {
            return $"{login} {password} {IsEnabled}";
        }



    }
}
