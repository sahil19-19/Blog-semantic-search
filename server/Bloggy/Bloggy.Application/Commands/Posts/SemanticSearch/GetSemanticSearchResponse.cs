using Bloggy.Application.Common.Dots;
using Bloggy.Application.Common.MeiliSearch;

namespace Bloggy.Application.Commands.Posts.SemanticSearch;

public record GetSemanticSearchResponse(
        MeiliSearchResponse result
    );