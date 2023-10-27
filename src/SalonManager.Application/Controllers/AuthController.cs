using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamlDotNet.Core.Tokens;

namespace SalonManager.Application.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        //private readonly ILoginService _loginService;
        //public AuthController(ILoginService loginService)
        //{
        //    _loginService = loginService;
        //}

        //[HttpPost]
        //[Route("signin")]
        //public IActionResult SignIn(User user)
        //{
        //    if (user == null)
        //        return BadRequest();

        //    var token = _loginService.ValidateCredentials(user);
        //    if (token == null)
        //        return Unauthorized();

        //    return Ok(token);
        //}

        //[HttpPost]
        //[Route("refresh")]
        //public IActionResult Refresh(Token token)
        //{ 
        //    if (token == null) return BadRequest();

        //    var newToken = _loginService.ValidateCredentials(token);
        //    if (newToken == null) return BadRequest();

        //    return Ok(newToken); 
        //}

        //[HttpGet]
        //[Route("revoke")]
        ////[Authorize("Bearer")]
        //public IActionResult Revoke()
        //{
        //    var username = User.Identity;
        //    var result = _loginService.RevokeToken(username);

        //    if (!result) return BadRequest();

        //    return NoContent();
        //}
    }
}
