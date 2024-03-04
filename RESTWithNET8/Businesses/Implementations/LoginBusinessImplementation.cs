using Microsoft.IdentityModel.JsonWebTokens;
using RESTWithNET8.Configurations;
using RESTWithNET8.Data.ValueObjects;
using RESTWithNET8.Repositories;
using RESTWithNET8.Services;
using System.Security.Claims;

namespace RESTWithNET8.Businesses.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private TokenConfiguration _configuration;

        private IUserRepository _repository;

        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO user)
        {
            var validatedUser = _repository.ValidateCredentials(user);

            if (validatedUser == null)
            {
                return null;
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                validatedUser.RefreshToken = refreshToken;
                validatedUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

                _repository.RefreshUserInfo(validatedUser);

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

                return new TokenVO(
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                    );
            }
        }
    }
}
