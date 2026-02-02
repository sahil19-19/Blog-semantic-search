using MediatR;

namespace Bloggy.Application.Commands.Posts.SemanticSearch;

public record GetSemanticSearchRequest(
    int Page,
    int Limit,
    string Search,
    string? FilterTopic,
    double Ratio
) : IRequest<GetSemanticSearchResponse>;

