using Common.Dto;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace Sub.Api.Controllers
{
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly IHttpContextAccessor _haccess;

        public BaseApiController(ILogger<T> logger, IHttpContextAccessor haccess)
        {
            this._logger = logger;
            this._haccess = haccess;
        }

        protected CurrentUserDto _currentUser
        {
            get { return _createCurrentUser(_haccess.HttpContext.User); }
        }

        private CurrentUserDto _createCurrentUser(ClaimsPrincipal principal)
        {
            if (principal != null && principal.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)principal.Identity;
                return new CurrentUserDto(
                    int.Parse(identity.FindFirst(ClaimTypes.Name).Value),
                    identity.FindFirst(ClaimTypes.Email).Value,
                    (UserRole)Enum.Parse(typeof(UserRole), identity.FindFirst(ClaimTypes.Role).Value));
            }

            return null;
        }
    }
}