using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Question;
using Jobby.Application.Repositories.Vacancy;
using Jobby.Domain.Entities.QuestionAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Question.Create
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ResponseDto>
    {
        private readonly IQuestionWriteRepository _questionWriteRepository;
        private readonly IVacancyReadRepository _vacancyReadRepository;
        private readonly IUserManager _userManager;

        public CreateQuestionCommandHandler(
            IQuestionWriteRepository questionWriteRepository,
            IVacancyReadRepository vacancyReadRepository,
            IUserManager userManager)
        {
            _questionWriteRepository = questionWriteRepository;
            _vacancyReadRepository = vacancyReadRepository;
            _userManager = userManager;
        }

        public async Task<ResponseDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();

            var vacancyExists = await _vacancyReadRepository.Table.AnyAsync(
                    v => v.Id == request.VacancyId && !v.IsDeleted);

            if (!vacancyExists)
                throw new NotFoundException("Vacancy tapılmadı");

            var question = new Domain.Entities.QuestionAggregate.Question(
                request.VacancyId,
                request.Text,
                request.Order,
                userId
            );

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

            question.IsValid();

            if (!question.IsValid())
                throw new BusinessException("Invalid question configuration");

            await _questionWriteRepository.AddAsync(question);
            await _questionWriteRepository.SaveAsync();

            return new() { Message = "Sual uğurla yaradıldı" };
        }
    }

}
