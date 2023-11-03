using MvideoParser.src.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvideoParser.src.main
{
    public class ParserSettings : IParserParams
    {
        #region ConfigParams
        public string Url { get; set; } = "https://www.mvideo.ru/brand/apple-685/f";
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        #endregion

        public ParserSettings(int startPoint, int endPoint, string prefix) // конструктор класса найтройки парсера
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Prefix = prefix;
        }
    }
}
