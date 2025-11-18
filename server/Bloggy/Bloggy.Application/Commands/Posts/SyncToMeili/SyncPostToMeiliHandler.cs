using MediatR;
using Bloggy.Application.Persistense;

namespace Bloggy.Application.Commands.Posts.SyncToMeili;

public class SyncPostsToMeiliHandler 
    : IRequestHandler<SyncPostsToMeiliRequest, string>
{
    private readonly IMeiliSyncService _sync;

    public SyncPostsToMeiliHandler(IMeiliSyncService sync)
    {
        _sync = sync;
    }

    public async Task<string> Handle(SyncPostsToMeiliRequest request, CancellationToken cancellationToken)
    {
        // Console.WriteLine("hello, inside sync handler");

        int count = await _sync.SyncAllPostsAsync();

        return $"Uploaded {count} posts to Meilisearch.";
    }
}
