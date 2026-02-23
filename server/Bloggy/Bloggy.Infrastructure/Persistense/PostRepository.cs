using System.Reflection.Metadata;
using System.Threading.Tasks;
using Bloggy.Application.Common.MeiliSearch;
using Bloggy.Application.Persistense;
using Bloggy.Domain.Entites;
using Bloggy.Infrastructure.Services;
using Bloggy.Infrastructure.Services.MeiliSearch;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.Infrastructure.Persistense;

public class PostRepository(
    AppDbContext _appDbContext,
    MeiliSearchService _meiliSearchService
) : IPostRepository
{
    public int GetCount()
    {
        return _appDbContext.Posts
            .Count();
    }
    public void Add(Post post)
    {
        _appDbContext.Posts.Add(post);
        _appDbContext.SaveChanges();
    }

    public IEnumerable<Post> GetAll(int page, int limit)
    {
        return _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .OrderByDescending(p => p.DateCreated)
            .Skip(page * limit)
            .Take(limit)
            .ToList();
    }

    public IEnumerable<Post> GetAllWithoutPaging()
    {
        return _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .ToList();
    }

    public IEnumerable<Post> GetByTopicId(int page, int limit, int topicId)
    {
        return _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .Where(p => p.Topics.Any(t => t.Id == topicId))
            .OrderByDescending(p => p.DateCreated)
            .Skip(page * limit)
            .Take(limit)
            .ToList();
    }

    public IEnumerable<Post> GetByTopic(int page, int limit, string topic)
    {
        return _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .Where(p => p.Topics.Any(t => t.Name == topic))
            .OrderByDescending(p => p.DateCreated)
            .Skip(page * limit)
            .Take(limit)
            .ToList();
    }

    public Post? GetById(Guid postId)
    {
        // Console.WriteLine("### total posts: "+ _appDbContext.Posts.Count());
        
        // var post =  _appDbContext.Posts
        //     .Include(p => p.Author)
        //     .Include(p => p.Topics)
        //     .FirstOrDefault(p => p.Id == postId);
        
        // Console.WriteLine("### post: ");
        // Console.WriteLine(post);
        Console.WriteLine("### total posts: " + _appDbContext.Posts.Count());
        Console.WriteLine("### Request ID: " + postId);
    
        foreach (var p in _appDbContext.Posts)
        {
            Console.WriteLine($"DB ID: {p.Id} | Equal? {p.Id == postId}");
        }
    
        var post = _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .FirstOrDefault(p => p.Id == postId);
    
        Console.WriteLine(post == null ? "### NOT FOUND" : "### FOUND");

        return post;
    }

    public void Remove(Post post)
    {
        _appDbContext.Posts.Remove(post);
        _appDbContext.SaveChanges();
    }

    public void Update(Post post)
    {
        _appDbContext.SaveChanges();
    }

    public IEnumerable<Post> Search(int page, int limit, string searchString)
    {
        return _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .Where(p => p.Title.ToUpper().Contains(searchString.ToUpper()) ||
                        p.Description.ToUpper().Contains(searchString.ToUpper()))
            .OrderByDescending(p => p.Title.ToUpper().Contains(searchString.ToUpper()))
            .ThenByDescending(p => p.DateCreated)
            .ThenByDescending(p => p.Description.ToUpper().Contains(searchString.ToUpper()))
            .ThenByDescending(p => p.DateCreated)
            .Skip(page * limit)
            .Take(limit)
            .ToList();
    }

    public async Task<MeiliSearchResponse> GetBySemanticSearch(int page, int limit, string searchString, string filterTopic, double ratio)
    {
        var meiliResult = await _meiliSearchService.SearchPostsBySemantic(searchString, filterTopic, page, limit, ratio);
        // if (meiliResult == null || meiliResult.hits == null)
        //     return Enumerable.Empty<MeiliSearchResponse>();
        return meiliResult;
    }

    public IEnumerable<Post> GetPopular(int page, int limit)
    {
        return _appDbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Topics)
            .OrderByDescending(p => p.Views)
            .ThenByDescending(p => p.DateCreated)
            .Skip(page * limit)
            .Take(limit)
            .ToList();
    }
}
