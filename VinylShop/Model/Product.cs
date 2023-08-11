using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class Product
    {
        public int Id { get; set; }
        public virtual Music_Records music_Records { get; set; }
        public virtual ICollection<ShopBag> ShopBags { get; set; }


        public Product()
        {
            ShopBags = new List<ShopBag>();
        }
    }
}
