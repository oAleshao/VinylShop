using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class PubishHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Music_Records> music_Records { get; set; }

        public PubishHouse()
        {
            music_Records = new List<Music_Records>();
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
