using Jobby.Application.Features.Commands.Applicant.Create;
using Jobby.Application.Features.Commands.Applicant.FinishTest;
using Jobby.Application.Features.Commands.Applicant.StartTest;
using Jobby.Application.Features.Commands.Applicant.SubmitAnswer;
using Jobby.Application.Features.Queries.Applicant.GetAll;
using Jobby.Application.Features.Queries.Applicant.GetTop;
using Jobby.Application.Features.Queries.Vacancy.GetTop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.WebApi.Controllers
{
    public class ApplicantsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateApplicantCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("start-test")]
        public async Task<IActionResult> StartTest([FromBody] StartTestCommand command)
        {
            var question = await Mediator.Send(command);
            return Ok(question);
        }

        [HttpPost("submit-answer")]
        public async Task<IActionResult> SubmitAnswer([FromBody] SubmitAnswerCommand command)
        {
            var question = await Mediator.Send(command);
            return Ok(question);
        }

        [HttpPost("finish-test")]
        public async Task<IActionResult> FinishTest([FromBody] FinishTestCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("top")]
        public async Task<IActionResult> GetTopApplicants(int topCount)
        {
            var result = await Mediator.Send(new GetTopApplicantsQuery(topCount));
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetApplicantsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}