using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NoTelegram.Core.Services;

namespace NoTelegram.API.Filters
{
    public class AuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IUsersService _usersService;

        public AuthFilter(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("auth-id"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            Guid id = Guid.Parse(context.HttpContext.Request.Cookies["auth-id"]);

            var getResult = await _usersService.GetById(id);
            if(getResult.IsFailure)
                context.Result = new UnauthorizedResult();
        }
    }
}