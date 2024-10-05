namespace DTOs.UrlToImageConfig
{
    public class UrlToImageConfigDto
    {
        public int RssFeedId { get; set; }
        public string Query { get; set; }
        public string? Attribute { get; set; }
        public string? Regex { get; set; }
    }
}
