using Microsoft.AspNetCore.Mvc;
using MVapi.Authentication;
using ProductApi.Models;
using Newtonsoft.Json;
using Pr.Models;
using static Pr.Models.ProductJsonAnswer;

namespace ProductApi.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _configuration;

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("/api/info")]
        [HttpPost]
        public IActionResult ProductInfo([FromBody] SearchProd searchParams, string token)
        {
            var key = _configuration.GetValue<string>(AuthConstants.ApiKeySelectorName);
            if (!string.IsNullOrWhiteSpace(token) && token == key)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(System.IO.File.ReadAllText(@"App_Data\Json.json"));
                if (products != null)
                {
                    {
                        var phrase = searchParams.searchPhraseList[0].ToLower();
                        var answer = products.Where(item => item.Name.ToLower().Contains(phrase));
                        Variant variant = new Variant()
                        {
                            phrase = phrase,
                            products = answer.ToList()
                        };
                        ProductJsonAnswer json = new ProductJsonAnswer
                        {
                            App = searchParams.App,
                            variants = new List<Variant>() { variant }
                        };
                        return Ok(JsonConvert.SerializeObject(json));
                    }
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [Route("/api/details")]
        [ HttpPost]
        public IActionResult ProductDetails([FromBody] SearchJsonDetails detailsParams, string token)
        {
            var key = _configuration.GetValue<string>(AuthConstants.ApiKeySelectorName);
            if (!string.IsNullOrWhiteSpace(token) && token == key)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(System.IO.File.ReadAllText(@"App_Data\Json.json"));
                if (products != null)
                {
                    {
                        var link = detailsParams.productLinks[0].ToLower();
                        var answer = products.FirstOrDefault(item => item.Link.ToLower().Contains(link));
                        RespDetails json = new RespDetails
                        {
                            App = detailsParams.App,
                            products = products.ToList()
                        };
                        return Ok(JsonConvert.SerializeObject(json));
                    }
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [Route("/api/")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");
        }
    }
}