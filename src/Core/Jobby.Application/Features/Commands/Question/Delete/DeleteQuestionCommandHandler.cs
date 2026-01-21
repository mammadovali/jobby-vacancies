using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Question;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Question.Delete
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ResponseDto>
    {
        private readonly IQuestionWriteRepository _writeRepository;
        private readonly IQuestionReadRepository _readRepository;
        private readonly IUserManager _userManager;
        public DeleteQuestionCommandHandler(IQuestionWriteRepository writeRepository,
            IQuestionReadRepository readRepository, IUserManager userManager)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _userManager = userManager;
        }

        public async Task<ResponseDto> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();
            var question = await _readRepository.GetByIdAsync(request.Id,
                include => include.Include(q => q.Options));

            if (question is null)
                throw new NotFoundException("Sual tapılmadı");

            question.IsDeleted = true;
            question.SetEditFields(userId);

            foreach (var option in question.Options)
            {
                option.IsDeleted = true;
                option.SetEditFields(userId);
            }

            await _writeRepository.SaveAsync();

            return new() { Message = "Sual uğurla silindi" };
        }
    }

}
