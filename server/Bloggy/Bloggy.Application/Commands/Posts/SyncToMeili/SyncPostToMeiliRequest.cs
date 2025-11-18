using MediatR;

namespace Bloggy.Application.Commands.Posts.SyncToMeili;

public record SyncPostsToMeiliRequest() : IRequest<string>;