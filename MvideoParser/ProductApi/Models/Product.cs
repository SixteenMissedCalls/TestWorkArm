using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductApi.Models
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        public decimal QuantityCurrent { get; set; }
        public decimal QuantityInStock { get; set; }
        public string Link { get; set; }
        public string CatalogPath { get; set; }
    }
}