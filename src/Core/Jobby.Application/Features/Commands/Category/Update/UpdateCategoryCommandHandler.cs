using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Category;
using MediatR;

namespace Jobby.Application.Features.Commands.Category.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseDto>
    {
        private readonly IUserManager _userManager;
        private readonly ICategoryWriteRepository _writeRepository;
        private readonly ICategoryReadRepository _readRepository;

        public UpdateCategoryCommandHandler(IUserManager userManager,
            ICategoryWriteRepository writeRepository,
            ICategoryReadRepository readRepository)
        {
            _userManager = userManager;
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }
        public async Task<ResponseDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();

            var category = await _readRepository.GetByIdAsync(request.Id);

            if(category == null)
                throw new BadRequestException("Kateqoriya tapılmadı");

            category.SetDetails(request.Name, userId);

            _writeRepository.Update(category);
            await _writeRepository.SaveAsync();

            return new() { Message = "Kateqoriya uğurla yeniləndi" };
        }
    }
}