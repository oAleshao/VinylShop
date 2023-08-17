using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class HelpOrders
    {
        public int Id { get; set; }
        public virtual Music_Records music_Records { get; set; }
        public int Quantity { get; set; }

        public virtual Orders orders { get; set; }

        public HelpOrders() { }

        public override string ToString()
        {
            return $"Название пластинки: {music_Records.Name} кол-во: {Quantity}";
        }
    }
}
