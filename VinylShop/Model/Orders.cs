using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Model
{
    public class Orders
    {
        public int Id { get; set; }
        public virtual Users Iduser { get; set; }
        public virtual ICollection<HelpOrders> helpOrders_ { get; set; }
        public DateTime dateOrder { get; set; }

        public double TotalPrice { get; set; } 

        public Orders() 
        {
            helpOrders_ = new List<HelpOrders>();
        }

        public override string ToString()
        {
            return $"Дата: {dateOrder.ToShortDateString()} | Общая сумма: {TotalPrice}";
        }

    }
}
