using Bloggy.Application.Common.Dots;
using Bloggy.Application.Persistense;
using MediatR;


namespace Bloggy.Application.Commands.Topics.GetAll;

public class GetAllHandler(
    ITopicRepository _topicRepository
) : IRequestHandler<GetAllRequest, GetAllResponse>
{
    public Task<GetAllResponse> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var topics = _topicRepository.GetAll()
            .Where(topic => topic.Posts.Any())
            .Select(topic => new TopicDto
            {
                Id = topic.Id,
                Name = topic.Name,
                PostCount = topic.Posts.Count()
            })
            .OrderBy(topic => topic.Name);

        return Task.FromResult(new GetAllResponse(topics));
    }
}
