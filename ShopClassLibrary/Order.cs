using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int PickupPointId { get; set; }
        public string PickupAddress { get; set; } // для отображения, не в БД
        public int CustomerId { get; set; }        // из таблицы users
        public string CustomerName { get; set; }   // для отображения, можно загрузить из users
        public string PickupCode { get; set; }
        public string Status { get; set; }
    }
}
