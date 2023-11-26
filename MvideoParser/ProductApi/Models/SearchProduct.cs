using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductApi.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SearchProd: BaseModel
    {
            public List<string> searchPhraseList { get; set; }
            public int waitTimeout { get; set; }
            public int maxProductsCount { get; set; }

    }
}