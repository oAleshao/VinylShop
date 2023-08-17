using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class Music_Records
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountSongs { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public double SellingPrice { get; set; }

        public virtual PubishHouse pubishHouse { get; set; }
        public virtual ICollection<Songs> songs { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
        public Music_Records() 
        {
            songs = new List<Songs>();
        }

        public override string ToString()
        {
           return $"{Name} | Цена: {Price} | Год: {Year} | Кол-во песен: {CountSongs} | Издательство {pubishHouse.Name}";
        }
    }


    //public class ShopBag
    //{
    //    public int Id { get; set; }
    //    public virtual Users IdUser { get; set; }
    //    public virtual Music_Records music_Records { get; set; }

    //    public ShopBag()
    //    {
    //    }
    //}
}


