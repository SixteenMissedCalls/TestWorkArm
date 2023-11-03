using Microsoft.AspNetCore.Mvc;
using MVapi.Models;
using Newtonsoft.Json;
using static MVapi.Models.ProductJsonAnswer;

namespace ProductApi.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [Route("/api/info")]
        [HttpPost]
        public IActionResult ProductInfo([FromBody] MVapi.Models.Root searchParams)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(System.IO.File.ReadAllText(@"F:\mvideo\MvideoParser\MvideoParser\Json.json"));
            if (products != null)
            {
                {
                    var phrase = searchParams.searchPhraseList[0].ToLower();
                    var answer = products.Where(item => item.Name.ToLower().Contains(phrase));
                    ProductJsonAnswer.App app = new ProductJsonAnswer.App()
                    {
                        appId = searchParams.app.appId,
                        appSecret = searchParams.app.appSecret
                    };
                    Variant variant = new Variant()
                    {
                        phrase = phrase,
                        products = answer.ToList()
                    };
                    ProductJsonAnswer.Root json = new ProductJsonAnswer.Root()
                    {
                        variants = new List<Variant> { variant },
                        app = app
                    };
                    return Ok(JsonConvert.SerializeObject(json));
                }
            }
            return BadRequest();
        }
    }
}