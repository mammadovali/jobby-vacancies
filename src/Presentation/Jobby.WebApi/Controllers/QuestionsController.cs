using Jobby.Application.Features.Commands.Question.Create;
using Jobby.Application.Features.Commands.Question.Delete;
using Jobby.Application.Features.Commands.Question.Update;
using Jobby.Application.Features.Queries.Question.GetById;
using Jobby.Application.Features.Queries.Question.GetByVacancyId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.WebApi.Controllers
{
    [Authorize]
    public class QuestionsController : BaseApiController
    {

        [HttpGet("by-vacancy/{vacancyId}")]
        public async Task<IActionResult> GetByVacancy(int vacancyId)
        {
            var result = await Mediator.Send(new GetQuestionsByVacancyIdQuery(vacancyId));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetQuestionByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateQuestionCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteQuestionCommand(id));
            return Ok(response);
        }
    }
}
