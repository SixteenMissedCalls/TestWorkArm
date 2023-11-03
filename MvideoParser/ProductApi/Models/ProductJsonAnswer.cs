using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVapi.Models
{
    public class ProductJsonAnswer
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class App
        {
            public string appId { get; set; }
            public string appSecret { get; set; }
        }
        public class Root
        {
            public List<Variant> variants { get; set; }
            public App app { get; set; }
        }

        public class Variant
        {
            public string phrase { get; set; }
            public List<Product> products { get; set; }
        }


    }
}