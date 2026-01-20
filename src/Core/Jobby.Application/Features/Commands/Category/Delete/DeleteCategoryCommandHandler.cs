using Jobby.Application.Constants;
using Jobby.Application.Exceptions;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Category;
using MediatR;

namespace Jobby.Application.Features.Commands.Category.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseDto>
    {
        private readonly IUserManager _userManager;
        private readonly ICategoryWriteRepository _writeRepository;
        private readonly ICategoryReadRepository _readRepository;

        public DeleteCategoryCommandHandler(IUserManager userManager,
            ICategoryWriteRepository writeRepository,
            ICategoryReadRepository readRepository)
        {
            _userManager = userManager;
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }
        public async Task<ResponseDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();

            var category = await _readRepository.GetByIdAsync(request.Id);

            if (category is null)
                throw new BadRequestException("Kateqoriya tapılmadı");

            category.IsDeleted = true;
            category.SetEditFields(userId);

            await _writeRepository.SaveAsync();

            return new ResponseDto { Message = "Kateqoriya uğurla silindi" };
        }
    }
}
