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
                include => include
                    .Include(q => q.Options)
                    .Include(q => q.ApplicantAnswers)
            );

            if (question == null || question.IsDeleted)
                throw new BusinessException("Sual tapılmadı");

            if (question.HasAnswers())
                throw new BusinessException("Bu suala artıq cavab verilib. Dəyişiklik etmək mümkün deyil");

            var oldOrder = question.Order;
            var newOrder = request.Order;
            var vacancyId = question.VacancyId;

            if (oldOrder != newOrder)
            {
                var totalQuestions = await _questionReadRepository.Table
                    .CountAsync(q => q.VacancyId == vacancyId && !q.IsDeleted, cancellationToken);

                if (newOrder < 1 || newOrder > totalQuestions)
                    throw new BusinessException($"Sıra nömrəsi 1 ilə {totalQuestions} arasında olmalıdır");

                var orderExists = await _questionReadRepository.Table.AnyAsync(
                    q => q.VacancyId == vacancyId
                         && q.Order == newOrder
                         && !q.IsDeleted
                         && q.Id != request.Id,
                    cancellationToken);

                if (orderExists)
                    throw new BusinessException("Bu sıra nömrəsi artıq mövcuddur");
            }

            question.Update(request.Text, newOrder, userId);

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

            if (oldOrder != newOrder)
            {
                var questionsToUpdate = await _questionReadRepository
                    .GetWhere(q => q.VacancyId == vacancyId &&
                                q.Id != request.Id &&
                                !q.IsDeleted)
                    .ToListAsync(cancellationToken);

                if (newOrder > oldOrder)
                {
                    foreach (var q in questionsToUpdate.Where(q => q.Order > oldOrder && q.Order <= newOrder))
                    {
                        q.Reorder(q.Order -1, userId);
                    }
                }
                else
                {
                    foreach (var q in questionsToUpdate.Where(q => q.Order >= newOrder && q.Order < oldOrder))
                    {
                        q.Reorder(q.Order + 1, userId);
                    }
                }
            }

            _questionWriteRepository.Update(question);
            await _questionWriteRepository.SaveAsync();

            return new ResponseDto
            {
                Message = "Sual uğurla yeniləndi"
            };
        }
    }
}
