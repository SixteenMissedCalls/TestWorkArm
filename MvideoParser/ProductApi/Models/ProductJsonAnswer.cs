using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pr.Models
{
    public class ProductJsonAnswer: BaseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        
        public List<Variant> variants { get; set; }

        public class Variant
        {
            public string phrase { get; set; }
            public List<Product> products { get; set; }
        }


    }
}