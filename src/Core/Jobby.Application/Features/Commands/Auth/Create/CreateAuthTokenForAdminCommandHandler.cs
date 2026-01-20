using Jobby.Application.Exceptions;
using Jobby.Application.Features.Commands.Auth.DTOs;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.User;
using Jobby.Infrastructure.Operations;
using MediatR;
using System.Security.Cryptography;

namespace Jobby.Application.Features.Commands.Auth.Create
{
    public class CreateAuthTokenForAdminCommandHandler : IRequestHandler<CreateAuthTokenForAdminCommand, JwtTokenDto>
    {
        private readonly IUserReadRepository _readRepository;
        private readonly IUserWriteRepository _writeRepository;
        private readonly IUserManager _userManager;

        public CreateAuthTokenForAdminCommandHandler(IUserReadRepository readRepository, IUserManager userManager, IUserWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _userManager = userManager;
            _writeRepository = writeRepository;
        }
        public async Task<JwtTokenDto> Handle(CreateAuthTokenForAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _readRepository.GetSingleAsync(x => x.Email.ToLower() == request.Email.ToLower());
            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash) || user.IsDeleted)
                throw new UnAuthorizedException("Email və ya şifrə yanlışdır");


            var randomNum = GenerateRandomNumber();
            var refreshToken = $"{randomNum}_{user.Id}_{DateTime.UtcNow.AddYears(1)}";
            user.UpdateRefreshToken(refreshToken);
            (string token, DateTime expireDate) = await _userManager.GenerateJwtTokenForAdmin(user);
            await _writeRepository.SaveAsync();
            return new() { Token = token, RefreshToken = refreshToken, ExpireAt = expireDate };
        }

        private object GenerateRandomNumber()
        {
            var randomNum = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNum);
                return Convert.ToBase64String(randomNum);
            }
        }
    }
}
