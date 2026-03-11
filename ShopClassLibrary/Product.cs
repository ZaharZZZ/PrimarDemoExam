using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary
{
    public class Product
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string Supplier { get; set; }
        public string Manufacture { get; set; }
        public string Category { get; set; }
        public int Discount { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Photo {  get; set; }
    }
}
