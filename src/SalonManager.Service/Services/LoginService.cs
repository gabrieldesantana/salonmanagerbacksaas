using SalonManager.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SalonManager.Service.Services
{
    public class LoginService : ILoginService
    {
        //private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        //private TokenConfiguration _configuration;

        //private IUserRepository _repository;
        //private readonly ITokenService _tokenService;

        //public LoginService(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        //{
        //    _configuration = configuration;
        //    _repository = repository;
        //    _tokenService = tokenService;
        //}

        //public TokenVO ValidateCredentials(UserVO userCredentials)
        //{
        //    var user = _repository.ValidateCredential(userCredentials);
        //    if (user == null)
        //        return null;
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        //    };

        //    var accessToken = _tokenService.GenerateAccessToken(claims);
        //    var refreshToken = _tokenService.GenerateRefreshToken();

        //    user.RefreshToken = refreshToken;
        //    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

        //    _repository.RefreshUserInfo(user);

        //    DateTime createDate = DateTime.Now;
        //    DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        //    return new TokenVO(
        //            true,
        //            createDate.ToString(DATE_FORMAT),
        //            expirationDate.ToString(DATE_FORMAT),
        //            accessToken,
        //            refreshToken
        //            );
        //}

        //public TokenVO ValidateCredentials(TokenVO tokenVO)
        //{
        //    var accessToken = tokenVO.AccessToken;
        //    var refreshToken = tokenVO.RefreshToken;

        //    var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        //    var username = principal.Identity.Name;

        //    var user = _repository.ValidateCredential(username);

        //    if (user == null ||
        //        user.RefreshToken != refreshToken ||
        //        user.RefreshTokenExpiryTime <= DateTime.Now) return null;

        //    accessToken = _tokenService.GenerateAccessToken(principal.Claims);
        //    refreshToken = _tokenService.GenerateRefreshToken();

        //    user.RefreshToken = refreshToken;

        //    _repository.RefreshUserInfo(user);

        //    DateTime createDate = DateTime.Now;
        //    DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        //    return new TokenVO(
        //            true,
        //            createDate.ToString(DATE_FORMAT),
        //            expirationDate.ToString(DATE_FORMAT),
        //            accessToken,
        //            refreshToken
        //            );
        //}

        //public bool RevokeToken(string username)
        //{
        //    return _repository.RevokeToken(username);
        //}
    }
}
