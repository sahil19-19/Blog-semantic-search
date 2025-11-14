using Meilisearch;
// using Meilisearch.Exceptions;
// using Bloggy.Domain.Entities;
using Bloggy.Domain.Entites; // Assuming Post is an entity in your project

namespace Bloggy.Infrastructure.Services.MeiliSearch;

public class MeiliSearchService
{
    private readonly MeilisearchClient _client;

    // Constructor that initializes the client with the MeiliSearch server URL
    public MeiliSearchService(string meiliSearchUrl, string apiKey)
    {
        _client = new MeilisearchClient(meiliSearchUrl, apiKey);
    }

    // Getter for the client
    public MeilisearchClient GetClient() => _client;

    public async Task TestConnection()
    {
        try
        {
            // Check the status of MeiliSearch (this assumes MeiliSearch has a "status" endpoint)
            var status = await _client.HealthAsync();
            Console.WriteLine(status);
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

    // Method to index posts
    // public async Task IndexPosts(IEnumerable<Post> posts)
    // {
    //     var index = _client.Index("posts"); // Create or access the 'posts' index

    //     // Convert posts to a structure MeiliSearch can index (custom DTO or model)
    //     var postsForSearch = posts.Select(post => new
    //     {
    //         Id = post.Id.ToString(),
    //         Title = post.Title,
    //         Content = post.Content,
    //         Author = post.Author.Name,  // Assuming Author is an object
    //         Category = post.Category,
    //         Description = post.Description,
    //         DateCreated = post.DateCreated.ToString("yyyy-MM-dd HH:mm:ss")
    //     });

    //     // try
    //     // {
    //         // Add documents to the 'posts' index
    //         await index.AddDocumentsAsync(postsForSearch);
    //         Console.WriteLine("Documents indexed successfully.");
    //     // }
    //     // catch (MeiliSearchException ex)
    //     // {
    //     //     Console.WriteLine($"Error indexing documents: {ex.Message}");
    //     // }
    // }

    // Method to perform a semantic search
    // public async Task<IEnumerable<dynamic>> SearchPostsBySemantic(string query)
    // {
    //     var index = _client.Index("posts");

    //     // try
    //     // {
    //         // Search the index with the provided query
    //         var result = await index.SearchAsync<Post>(query);
    //         return result.Hits; // Return the search results (posts)
    //     // }
    //     // catch (MeiliSearchException ex)
    //     // {
    //     //     Console.WriteLine($"Error searching for posts: {ex.Message}");
    //     //     return new List<dynamic>(); // Return empty list if error occurs
    //     // }
    // }
}
