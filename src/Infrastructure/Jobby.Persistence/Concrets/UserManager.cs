using AutoMapper;
using Jobby.Application.Exceptions;
using Jobby.Application.Features.Queries.User.DTOs;
using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.User;
using Jobby.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jobby.Persistence.Concrets
{
    public class UserManager : IUserManager
    {
        private readonly IClaimManager _claimManager;
        private readonly IConfiguration _configuration;
        private readonly IUserReadRepository _readRepository;
        private readonly IMapper _mapper;

        public UserManager(IClaimManager claimManager, IConfiguration configuration, IUserReadRepository readRepository, IMapper mapper)
        {
            _claimManager = claimManager;
            _configuration = configuration;
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<(string token, DateTime expireAt)> GenerateJwtTokenForAdmin(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(_claimManager.GetUserClaims(user));

            return await GenerateToken(claims);
        }

        private Task<(string token, DateTime expireAt)> GenerateToken(List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = jwtSettings["SecretKey"];
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireAt"]));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                signingCredentials: signingCredentials,
                claims: claims,
                expires: expireDate
            );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            return Task.FromResult<(string token, DateTime expireAt)>((jwtSecurityTokenHandler.WriteToken(jwtSecurityToken), expireDate));
        }

        public int GetCurrentUserId()
        {
            return _claimManager.GetCurrentUserId();
        }

        public string GetCurrentUserType()
        {
            return _claimManager.GetCurrentUserType();
        }

        public async Task<UserDto> GetUserProfileAsync(int userId)
        {
            var user = await _readRepository.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("İstifadəçi tapılmadı");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
