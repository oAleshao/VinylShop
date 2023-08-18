using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class SellsDay
    {
        public int Id { get; set; }
        public DateTime startDayForSell { get; set; }
        public DateTime endDayForSell { get; set; }
        public virtual GanreMusic ganre { get; set; }
        public double Sell { get; set; }

        public override string ToString()
        {
            return $"Начало: {startDayForSell.ToShortDateString()} | Конец: {endDayForSell.ToShortDateString()} | " +
                $"Жанр: {ganre.Name} | Скидка: {Sell}%";
        }

    }
}
