using Jobby.Application.Features.Commands.Vacancy.Create;
using Jobby.Application.Features.Commands.Vacancy.Delete;
using Jobby.Application.Features.Commands.Vacancy.Update;
using Jobby.Application.Features.Queries.Vacancy.GetAll;
using Jobby.Application.Features.Queries.Vacancy.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.WebApi.Controllers
{
    public class VacanciesController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetVacanciesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetVacancyByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateVacancyCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVacancyCommand command)
        {
            command.Id = id;
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteVacancyCommand(id));
            return Ok(response);
        }
    }
}
