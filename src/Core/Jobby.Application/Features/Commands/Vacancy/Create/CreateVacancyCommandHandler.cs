using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Category;
using Jobby.Application.Repositories.Vacancy;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Vacancy.Create
{
    public class CreateVacancyCommandHandler : IRequestHandler<CreateVacancyCommand, ResponseDto>
    {
        private readonly IUserManager _userManager;
        private readonly IVacancyWriteRepository _writeRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public CreateVacancyCommandHandler(IUserManager userManager, IVacancyWriteRepository writeRepository,
            ICategoryReadRepository categoryReadRepository)
        {
            _userManager = userManager;
            _writeRepository = writeRepository;
            _categoryReadRepository = categoryReadRepository;
        }
        public async Task<ResponseDto> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();
            var categoryExists = await _categoryReadRepository.Table
                .AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (!categoryExists)
                throw new NotFoundException("Kateqoriya tapılmadı");

            var vacancy = new Domain.Entities.VacancyAggragate.Vacancy(
                request.CategoryId,
                request.Title,
                request.Description,
                userId
            );

            await _writeRepository.AddAsync(vacancy);
            await _writeRepository.SaveAsync();

            return new() { Message = "Vakansiya uğurla yaradıldı"};
        }
    }
}
