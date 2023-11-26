namespace ProductApi.Models
{
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class RespDetails: BaseModel
        {
            public List<Product> products { get; set; }
            public class Attachment
            {
                public string name { get; set; }
                public string base64Content { get; set; }
            }

            public class Image
            {
                public string format { get; set; }
                public string base64Content { get; set; }
            }

            public class Property
            {
                public string name { get; set; }
                public string value { get; set; }
            }
        }
}
