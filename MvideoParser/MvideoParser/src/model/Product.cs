using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvideoParser.src.model
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; } = "RUB";
        public decimal QuantityCurrent { get; set; } = 0;
        public decimal QuantityInStock { get; set; } = 0;
        public string Link {  get; set; }
        public string CatalogPath { get; set; } = null;
    }
}
