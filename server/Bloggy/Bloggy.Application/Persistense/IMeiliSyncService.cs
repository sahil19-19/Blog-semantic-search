namespace Bloggy.Application.Persistense;

public interface IMeiliSyncService
{
    Task<int> SyncAllPostsAsync();
}
