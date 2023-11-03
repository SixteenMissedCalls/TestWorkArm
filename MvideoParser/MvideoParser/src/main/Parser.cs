using AngleSharp.Browser;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MvideoParser.src.parameters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvideoParser.src.main
{
    internal class Parser : IParser<int[]>
    {
        public int[] Parse(IHtmlDocument document)
        {
            var lsitId = new List<int>();
            var items = document.QuerySelectorAll("div").Where((item => item.ClassName != null && item.ClassName.Contains("fl-product-tile__picture-holder c-product-tile-picture__holder")));
            foreach(var item in items) 
            {
                var productInfo = item.QuerySelector("[data-product-info]"); // получаем product info json файлы 
                var attribute = productInfo.GetAttribute("data-product-info");
                lsitId.Add(Convert.ToInt32(JsonParamParser(productInfo, attribute, "productId")));
            }
            return lsitId.ToArray();
        }

        private string JsonParamParser(IElement element, string attribute, string key)
        {
            JObject obj = JObject.Parse(attribute);
            JToken token = obj[key];
            return token.ToString();
        }
    }
}
