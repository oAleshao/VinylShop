using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class Songs
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Music_Records IdMusic_Records { get; set; }

        public virtual ICollection<GanreMusic> ganreMusics { get; set; }

        public virtual ICollection<ExecutorMusic> executorMusics { get; set; }

        public Songs()
        {
            ganreMusics = new List<GanreMusic>();
            executorMusics = new List<ExecutorMusic>();
        }
    }
}
