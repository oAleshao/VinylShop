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
        public Music_Records() 
        {
            songs = new List<Songs>();
        }

        public override string ToString()
        {
           return $"{Name} | Кол-во песен: {CountSongs} | Год: {Year} | Цена: {Price} | Издательство {pubishHouse.Name}";
        }
    }
}
