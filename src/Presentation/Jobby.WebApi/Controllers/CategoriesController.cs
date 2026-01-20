using Jobby.Application.Features.Commands.Auth.Create;
using Jobby.Application.Features.Commands.Category.Create;
using Jobby.Application.Features.Commands.Category.Delete;
using Jobby.Application.Features.Commands.Category.Update;
using Jobby.Application.Features.Queries.Category.GetAll;
using Jobby.Application.Features.Queries.Category.GetById;
using Jobby.Application.Features.Queries.User.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.WebApi.Controllers
{
    public class CategoriesController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCategoriesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteCategoryCommand(id));
            return Ok(response);
        }
    }
}
