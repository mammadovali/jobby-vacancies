using Jobby.Application.Features.Commands.Applicant.Create;
using Jobby.Application.Features.Commands.Applicant.FinishTest;
using Jobby.Application.Features.Commands.Applicant.StartTest;
using Jobby.Application.Features.Commands.Applicant.SubmitAnswer;
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
    }
}