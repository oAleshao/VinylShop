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
        //public virtual ICollection<HelpOrder> helpOrders { get; set; }

        public Orders() 
        {
          //  helpOrders = new List<HelpOrder>();
        }

    }
}
