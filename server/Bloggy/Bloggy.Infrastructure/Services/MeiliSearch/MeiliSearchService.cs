using Meilisearch;
// using Meilisearch.Ex;
// using Bloggy.Domain.Entities;
using Bloggy.Domain.Entites;
using System.Text;
using System.Text.Json;
// using Bloggy.Infrastructure.Services.MeiliSearch;
using Bloggy.Application.Common.MeiliSearch; // Assuming Post is an entity in your project

// namespace Bloggy.Infrastructure.Services.MeiliSearch;

public class MeiliSearchService
{
    private readonly MeilisearchClient _client;
    // private readonly string? _meilisearchUrl;
    private readonly HttpClient _httpClient;


    // Constructor that initializes the client with the MeiliSearch server URL
    public MeiliSearchService(string meiliSearchUrl, string apiKey, string searchAPI)
    {
        _client = new MeilisearchClient(meiliSearchUrl, apiKey);

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(meiliSearchUrl);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {searchAPI}");
    }

    // Getter for the client
    public MeilisearchClient GetClient() => _client;

    public async Task TestConnection()
    {
        try
        {
            // Check the status of MeiliSearch (this assumes MeiliSearch has a "status" endpoint)
            var status = await _client.HealthAsync();

            if (status.Status == "available")
            {
                Console.WriteLine("MeiliSearch is available and connected.");
            }
            else
            {
                throw new Exception("MeiliSearch is not available.");
            }
        }
        catch (Exception ex)
        {
            // Handle any connection errors (e.g., MeiliSearch not running or network issues)
            Console.WriteLine("Error connecting to MeiliSearch: " + ex.Message);
            throw; // Rethrow the exception to be handled in Program.cs
        }
    }

    // Method to perform a semantic search
    public async Task<MeiliSearchResponse> SearchPostsBySemantic(string query, int page, int limit, double ratio)
    {
        try
        {
            var searchRequest = new
            {
                q = query,
                attributesToHighlight= new[] {"title", "description"},
                highlightPreTag = "<mark>",
                highlightPostTag = "</mark>",
                hybrid = new
                {
                    embedder = "blogs-embedder",
                    semanticRatio = ratio
                },
                limit = limit,
                offset = (page - 1) * limit
            };

            var json = JsonSerializer.Serialize(searchRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/indexes/blogs/search", content);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            // return jsonString;

            var result = JsonSerializer.Deserialize<MeiliSearchResponse>(jsonString);

            // if (result?.hits != null)
            // {
            //     foreach (var hit in result.hits)
            //     {
            //         if (hit?.description != null)
            //         {
            //             hit.description = TrimTo500Chars(hit.description);
            //         }
            //         if (hit?._formatted != null && hit._formatted?.description != null)
            //         {
            //             hit._formatted.description = TrimTo500Chars(hit._formatted.description);
            //         }
            //     }
            // }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching for posts: {ex.Message}");
            return new MeiliSearchResponse { hits = new List<dynamic>() };// Return empty list if error occurs
            // throw;
        }
    }

    // private static string? TrimTo500Chars(string? value)
    // {
    //     if (string.IsNullOrEmpty(value))
    //     {
    //         return value;
    //     }

    //     const int maxLength = 500;
    //     return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    // }
}



/*

{
    "result": {
        "hits": [
            {
                "title": "Understanding the Rise of Wearable Technology",
                "description": "Wearable technology has rapidly evolved from devices capable of tracking heart rate variability, sleep cycles, stress "
                "_formatted": {
                    "title": "Understanding the Rise of <mark>Wearable</mark> Technology",
                    "description": "<mark>Wearable</mark> technology has rapidly evolved from basic",
                }
            },
            {...}
        ]
    }
}

*/