using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class GanreMusic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Songs> songs { get; set; }

        public GanreMusic()
        {
            songs = new List<Songs>();
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
