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

        public virtual ICollection<GanreMusic> ganreMusics { get; set; }

        public virtual ICollection<ExecutorMusic> executorMusics { get; set; }

        public virtual ICollection<Music_Records> music_Records { get; set; }

        public Songs()
        {
            ganreMusics = new List<GanreMusic>();
            executorMusics = new List<ExecutorMusic>();
            music_Records = new List<Music_Records>();
        }

        public override string ToString()
        {
            string allGanres = string.Empty;
            string allExecutor = string.Empty;
            int indxHelp = 1;
            if (ganreMusics != null)
            {
                foreach (var ganre in ganreMusics)
                {
                    allGanres += ganre.Name;
                    if (indxHelp != ganreMusics.Count)
                    {
                        allGanres += ", ";
                    }
                    indxHelp++;
                }
            }

            indxHelp = 1;
            if (executorMusics != null)
            {
                foreach (var executor in executorMusics)
                {
                    allExecutor += executor.Name;
                    if (indxHelp != ganreMusics.Count)
                    {
                        allExecutor += " & ";
                    }
                    indxHelp++;
                }
            }

            return $"{Name} Исполнитель: {allExecutor} | Жанр: {allGanres}";
        }
    }
}
