using Jobby.Application.Constants;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.Category;
using MediatR;

namespace Jobby.Application.Features.Commands.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseDto>
    {
        private readonly IUserManager _userManager;
        private readonly ICategoryWriteRepository _writeRepository;

        public CreateCategoryCommandHandler(IUserManager userManager, ICategoryWriteRepository writeRepository)
        {
            _userManager = userManager;
            _writeRepository = writeRepository;
        }
        public async Task<ResponseDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            int userId = _userManager.GetCurrentUserId();

            var category = new Domain.Entities.CategoryAggregate.Category(request.Name, userId);

            await _writeRepository.AddAsync(category);
            await _writeRepository.SaveAsync();

            return new ResponseDto
            {
                Message = "Kateqoriya əlavə edildi"
            };
        }
    }
}
