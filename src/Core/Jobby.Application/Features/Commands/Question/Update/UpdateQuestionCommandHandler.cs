using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Question;
using Jobby.Domain.Entities.QuestionAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Question.Update
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, ResponseDto>
    {
        private readonly IQuestionWriteRepository _questionWriteRepository;
        private readonly IQuestionReadRepository _questionReadRepository;
        private readonly IUserManager _userManager;

        public UpdateQuestionCommandHandler(
            IQuestionWriteRepository questionWriteRepository,
            IQuestionReadRepository questionReadRepository,
            IUserManager userManager)
        {
            _questionWriteRepository = questionWriteRepository;
            _questionReadRepository = questionReadRepository;
            _userManager = userManager;
        }

        public async Task<ResponseDto> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();
            var question = await _questionReadRepository.GetByIdAsync(
                request.Id,
                include => include.Include(q => q.Options)
            );

            if (question == null || question.IsDeleted)
                throw new BusinessException("Sual tapılmadı");

            question.Update(request.Text, request.Order, userId);

            question.Options.ToList().ForEach(o => question.RemoveOption(o));

            foreach (var option in request.Options)
            {
                question.AddOption(
                    new QuestionOption(
                        option.Text,
                        option.IsCorrect, 
                        userId
                    )
                );
            }

            if (!question.IsValid())
                throw new BusinessException("Invalid question configuration");

            await _questionWriteRepository.SaveAsync();

            return new ResponseDto
            {
                Message = "Sual uğurla yeniləndi"
            };
        }
    }
}
