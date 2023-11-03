using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVapi.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class App
    {
        public string appId { get; set; }
        public string appSecret { get; set; }
    }

    public class Root
    {
        public App app { get; set; }
        public List<string> searchPhraseList { get; set; }
        public int waitTimeout { get; set; }
        public int maxProductsCount { get; set; }
    }


}