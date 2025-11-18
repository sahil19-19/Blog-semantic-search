using Meilisearch;
using Microsoft.EntityFrameworkCore;
using Bloggy.Infrastructure.Persistense;
using Bloggy.Infrastructure.Services.MeiliSearch;
using Bloggy.Application.Persistense;

namespace Bloggy.Infrastructure.Services.MeiliSearch;

public class MeiliSyncService : IMeiliSyncService
{
    private readonly AppDbContext _db;
    private readonly MeilisearchClient _client;

    public MeiliSyncService(AppDbContext db, MeiliSearchService meili)
    {
        _db = db;
        _client = meili.GetClient();
    }

    public async Task<int> SyncAllPostsAsync()
    {
        var posts = await _db.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .ToListAsync();

        if (!posts.Any()) return 0;

        var docs = posts.Select(p => new
        {
            id = p.Id.ToString(),
            title = p.Title,
            description = p.Description,
            author = p.Author.Name,
            topics = p.Topics.Select(t => t.Name).ToList(),
            dateCreated = p.DateCreated
        });

        // Console.WriteLine(docs);
        var index = _client.Index("blogs");

        var task = await index.AddDocumentsAsync(docs);
        await _client.WaitForTaskAsync(task.TaskUid);

        return posts.Count;
    }
}
