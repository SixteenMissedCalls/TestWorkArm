namespace ProductApi.Models
{
    public class SearchJsonDetails:BaseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public List<string> productLinks { get; set; }
            public bool canLoadAttachments { get; set; } = false;
    }
}
