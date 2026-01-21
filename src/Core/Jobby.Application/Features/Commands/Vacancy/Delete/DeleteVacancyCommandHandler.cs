using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Vacancy;
using MediatR;

namespace Jobby.Application.Features.Commands.Vacancy.Delete
{
    public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand, ResponseDto>
    {
        private readonly IUserManager _userManager;
        private readonly IVacancyReadRepository _readRepository;
        private readonly IVacancyWriteRepository _writeRepository;

        public DeleteVacancyCommandHandler(IUserManager userManager, IVacancyReadRepository readRepository,
            IVacancyWriteRepository writeRepository)
        {
            _userManager = userManager;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }
        public async Task<ResponseDto> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();

            var vacancy = await _readRepository.GetByIdAsync(request.Id);

            if (vacancy is null)
                throw new BadRequestException("Vakansiya tapılmadı");

            vacancy.IsDeleted = true;
            vacancy.SetEditFields(userId);

            await _writeRepository.SaveAsync();

            return new ResponseDto { Message = "Vakansiya uğurla silindi" };
        }
    }
}
