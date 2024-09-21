namespace DomainModels
{
    public class UrlToImageConfig : BaseClass
    {
        public int RssSourceId { get; set; }
        public RssSource RssSource { get; set; }
        public string Query { get; set; }
        public string? Attribute { get; set; }
        public string? Regex { get; set; }
    }
}
