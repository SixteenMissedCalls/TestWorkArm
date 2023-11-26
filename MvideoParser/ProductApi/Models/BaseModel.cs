namespace ProductApi.Models
{
    public abstract class BaseModel
    {
        public App App { get; set; }
    }
    public class App
    {
        public string appId { get; set; }
        public string appSecret { get; set; }
    }
}
