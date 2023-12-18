using Business.Services;
using Common.Dto;
using Common.Singletons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sub.Api.ActionFilters;
using System.Threading.Tasks;

namespace Sub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : BaseApiController<AuthorizeController>
    {
        private readonly IAppCache _appCache;
        private readonly IAuthorizeService _authorizeServiceBll;

        public AuthorizeController(
            ILogger<AuthorizeController> logger,
            IHttpContextAccessor haccess,
            IAuthorizeService authorizeServiceBll,
            IAppCache appCache
            ) : base(logger, haccess)
        {
            _authorizeServiceBll = authorizeServiceBll;
            _appCache = appCache;
        }

        [HttpPost("dontTouch")]
        [ProducesResponseType(typeof(ErrorResponseDto), 400)]
        public IActionResult DontTouch()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("checkToken")]
        public async Task<ActionResult<CheckTokenResponseDto>> CheckTokenAsync()
        {
            var response = new CheckTokenResponseDto
            {
                user = _currentUser,
                appBusinessConfig = _appCache.ConfigCache
            };
            return response;
        }

        [HttpPost("login")]        
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto req)
        {
            return await _authorizeServiceBll.Login(req);
        }

        [HttpPost("forgetPassword")]
        [TypeFilter(typeof(LogActionFilter))]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordRequestDto req)
        {
            _logger.LogInformation("Forget Password on email: {email}", req.Email);
            await _authorizeServiceBll.ForgetPassword(req);
            return Ok();
        }

        [HttpPost("changePassword")]
        [TypeFilter(typeof(LogActionFilter))]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestDto req)
        {
            _logger.LogInformation("Change Password");
            await _authorizeServiceBll.ChangePassword(req);
            return Ok();
        }
    }
}
