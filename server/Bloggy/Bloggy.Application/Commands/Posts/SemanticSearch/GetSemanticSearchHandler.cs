using Bloggy.Application.Common.Dots;
using Bloggy.Application.Persistense;
using MediatR;

namespace Bloggy.Application.Commands.Posts.SemanticSearch;

public class GetSemanticSearchHandler(
    IPostRepository _postRepository
) : IRequestHandler<GetSemanticSearchRequest, GetSemanticSearchResponse>
{
    public async Task<GetSemanticSearchResponse> Handle(GetSemanticSearchRequest request, CancellationToken cancellationToken)
    {
        var posts = await _postRepository.GetBySemanticSearch(request.Page, request.Limit, request.Search, request.FilterTopic, request.Ratio);
        return new GetSemanticSearchResponse(posts);

        // var formatted = posts.Select(p => new PostDto
        // {
        //     Id = p.Id.ToString(),
        //     Author = new UserWithoutPasswordDto
        //     {
        //         Id = p.Author.Id.ToString(),
        //         ImageUri = p.Author.ImageUri,
        //         Name = p.Author.Name,
        //         Email = p.Author.Email,
        //     },
        //     ImageUri = p.ImageUri,
        //     Title = p.Title,
        //     Description = p.Description,
        //     DateCreated = p.DateCreated.ToString("dd/MM/yyyy HH:mm"),
        //     Views = p.Views,
        //     Topics = p.Topics.Select(t => new TopicDto
        //     {
        //         Id = t.Id,
        //         Name = t.Name
        //     })
        // });

    }
}