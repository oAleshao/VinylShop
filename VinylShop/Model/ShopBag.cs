using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class ShopBag
    {
        public int Id { get; set; }
        public virtual Users IdUser { get; set; }
        public virtual Music_Records music_Records { get; set; }

        public ShopBag()
        {
        }
    }
}
