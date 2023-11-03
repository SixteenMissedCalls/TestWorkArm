using MvideoParser.src.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvideoParser.src.main
{
    public class HtmlLoader
    {
        private readonly string _url;
        private readonly HttpClient _httpClient;
        public HtmlLoader(IParserParams settings)
        {
            _httpClient = new HttpClient();
            _url = $"{settings.Url}/{settings.Prefix}";
        }

        public async Task<string> GetPageByIDAsync(int id)
        {
            var currentUrl = _url.Replace("{CurrentId}", id.ToString());
            var response = await _httpClient.GetAsync(currentUrl);
            var source = response != null && response.StatusCode == HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : null;
            return source;
        }
    }
}
