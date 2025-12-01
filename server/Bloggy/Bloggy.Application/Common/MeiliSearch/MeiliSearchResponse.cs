namespace Bloggy.Application.Common.MeiliSearch;

public class Formatted
{
    public string title { get; set; }
    public string description { get; set; }
}

public class Hit
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string author { get; set; }
    public string dateCreated { get; set; }
    public List<string> topics { get; set; }
    public Formatted _formatted { get; set; }
}

public class MeiliSearchResponse
{
    public List<dynamic> hits { get; set; }
    public int processingTimeMs { get; set; }
    public int estimatedTotalHits { get; set; }
}