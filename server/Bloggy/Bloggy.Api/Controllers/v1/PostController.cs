using Bloggy.Application.Commands.Posts.Create;
using Bloggy.Application.Commands.Posts.GetAll;
using Bloggy.Application.Commands.Posts.GetById;
using Bloggy.Application.Commands.Posts.SemanticSearch;
using Bloggy.Application.Commands.Posts.SyncToMeili;
using Bloggy.Application.Common.Dots;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Api.Controllers.v1;

[ApiController]
[Route("api/v1/posts")]
public class PostController(
    IMediator _mediator
) : BloggyControllerBase
{

    [HttpPost("semantic")]
    public async Task<IActionResult> GetBySemanticSearch(
        // [FromBody] string search = "",
        [FromBody] SemanticSearchRequestDto body,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10
    ){ 
        var result = await _mediator.Send(new GetSemanticSearchRequest(page, limit, body.Search));
        return Ok(result);
    }

    [HttpPost("sync-meili")]
    public async Task<IActionResult> SyncToMeili() {
        var result = await _mediator.Send(new SyncPostsToMeiliRequest());

        return Ok(new { result });
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateRequest request) => Ok(_mediator.Send(request));


    // //_mediator.Send(new GetSemanticSearchRequest(page, limit, body.Search))
    //         [FromQuery] int page = 0,
    //     [FromQuery] int limit = 10

    [HttpGet("{postId:guid}")]
    public IActionResult GetById(Guid postId) => Ok(_mediator.Send(new GetByIdRequest(postId)));

    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int page,
        [FromQuery] int limit = 3,
        [FromQuery] string category = "",
        [FromQuery] string search = ""
    ) => Ok(_mediator.Send(new GetAllRequest(page, limit, category, search)));
}