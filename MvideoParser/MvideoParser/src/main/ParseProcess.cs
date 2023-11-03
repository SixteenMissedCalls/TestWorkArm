using AngleSharp.Browser;
using AngleSharp.Html.Parser;
using MvideoParser.src.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvideoParser.src.main
{
    public class ParseProcess<T> where T : class
    {
        private IParser<T> _parser;
        private IParserParams _paramsParser;
        private bool _isWork = true;
        private HtmlLoader _loader;

        public IParser<T> Parser { get => _parser; set => _parser = value; }
        public IParserParams ParamsParser 
        {
            get => _paramsParser;
            set
            {
                _paramsParser = value;
                _loader = new HtmlLoader(value);
            }
        }
        public bool IsWork { get => _isWork; set => _isWork = value; }
        public Action<object> SatusEvent { get; set; }
        public Action<object, T> DataChangeEvent { get; set; }

        public ParseProcess(IParser<T> parser)
        {
            Parser = parser;
        }

        public void Started()
        {
            _isWork = true;
            GetProductJsonInfoAsync();
        }

        public void Abort()
        {
            _isWork = false;
            SatusEvent?.Invoke(this);
        }

        public async void GetProductJsonInfoAsync()
        {
            for(int i = _paramsParser.StartPoint; i < _paramsParser.EndPoint; i++)
            {
                if (_isWork == false)
                {
                    SatusEvent?.Invoke(this);
                    return;
                }
                else
                {
                    var source = await _loader.GetPageByIDAsync(i);
                    HtmlParser parser = new HtmlParser();
                    var response = await parser.ParseDocumentAsync(source);
                    var result = _parser.Parse(response);
                    DataChangeEvent?.Invoke(this, result);
                }
            }
            IsWork = false;
            SatusEvent?.Invoke(this);
        }
    }
}
