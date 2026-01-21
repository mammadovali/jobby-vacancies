using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Vacancy;
using MediatR;

namespace Jobby.Application.Features.Commands.Vacancy.Update
{
    public class UpdateVacancyCommandHandler : IRequestHandler<UpdateVacancyCommand, ResponseDto>
    {
        private readonly IVacancyReadRepository _readRepository;
        private readonly IVacancyWriteRepository _writeRepository;
        private readonly IUserManager _userManager;

        public UpdateVacancyCommandHandler(IVacancyReadRepository readRepository,
            IVacancyWriteRepository writeRepository,
            IUserManager userManager)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _userManager = userManager;
        }
        public async Task<ResponseDto> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();
            var vacancy = await _readRepository.GetByIdAsync(request.Id);

            if (vacancy is null)
                throw new BadRequestException("Vakansiya tapılmadı");

            vacancy.SetDetails(request.Title, request.Description, request.IsActive, request.CategoryId, userId);

            _writeRepository.Update(vacancy);
            await _writeRepository.SaveAsync();

            return new() { Message = "Vakansiya uğurla yeniləndi" };
        }
    }
}
