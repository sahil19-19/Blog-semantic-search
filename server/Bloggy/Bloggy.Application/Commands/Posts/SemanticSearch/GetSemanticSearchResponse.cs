using Bloggy.Application.Common.Dots;

namespace Bloggy.Application.Commands.Posts.SemanticSearch;

public record GetSemanticSearchResponse(
        IEnumerable<PostDto> Posts
    );