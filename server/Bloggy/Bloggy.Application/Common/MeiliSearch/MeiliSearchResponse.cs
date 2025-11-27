namespace Bloggy.Application.Common.MeiliSearch;

public class MeiliSearchResponse
{
    public List<dynamic> hits { get; set; }
    public int processingTimeMs { get; set; }
    public int estimatedTotalHits { get; set; }
    // public HighlightFields _formatted { get; set; }
}

// public class HighlightFields
// {
//     public string title { get; set; }
//     public string description { get; set; }
// }