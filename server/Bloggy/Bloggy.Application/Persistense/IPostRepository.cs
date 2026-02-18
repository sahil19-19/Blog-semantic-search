using Bloggy.Application.Common.MeiliSearch;
using Bloggy.Domain.Entites;

namespace Bloggy.Application.Persistense;

public interface IPostRepository
{
    void Add(Post post);
    void Remove(Post post);
    void Update(Post post);
    Post? GetById(Guid postId);
    IEnumerable<Post> GetAll(int page, int limit);
    int GetCount();
    IEnumerable<Post> GetAllWithoutPaging();
    IEnumerable<Post> GetPopular(int page, int limit);
    IEnumerable<Post> Search(int page, int limit, string searchString);
    IEnumerable<Post> GetByTopicId(int page, int limit, int topicId);
    IEnumerable<Post> GetByTopic(int page, int limit, string topic);
    Task<MeiliSearchResponse> GetBySemanticSearch(int page, int limit, string searchString, string filterTopic, double ratio);
}