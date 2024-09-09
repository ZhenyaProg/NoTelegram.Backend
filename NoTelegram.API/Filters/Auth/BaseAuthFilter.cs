using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NoTelegram.Core.Models;
using NoTelegram.Core.Services;

namespace NoTelegram.API.Filters.Auth
{
    public abstract class BaseAuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        protected readonly IUsersService UsersService;
        private List<Func<AuthorizationFilterContext, Task>> _filters;

        protected BaseAuthFilter(IUsersService usersService)
        {
            UsersService = usersService;
            _filters = new List<Func<AuthorizationFilterContext, Task>>();
        }

        protected void Use(Func<AuthorizationFilterContext, Task> filter)
        {
            if (_filters.Contains(filter)) return;
            _filters.Add(filter);
        }

        //TODO: придумать архитектуру, при которой не будет дубляжа в фильтрах
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            foreach (var filter in _filters)
            {
                if (context.Result is UnauthorizedResult)
                    break;
                else
                    await filter(context);
            }
        }

        protected async Task HeaderFilter(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("auth-id"))
                context.Result = new UnauthorizedResult();

            await Task.CompletedTask;
        }

        protected async Task UserIdFilter(AuthorizationFilterContext context)
        {
            var getResult = await GetUser(context);
            if (getResult.IsFailure)
                context.Result = new UnauthorizedResult();
        }

        protected async Task UserAuthTimeFilter(AuthorizationFilterContext context)
        {
            var getResult = await GetUser(context);
            if (!getResult.Value.Authenticated || DateTime.Now - getResult.Value.AuthDate > TimeSpan.FromMinutes(1))
                context.Result = new UnauthorizedResult();
        }

        private async Task<Result<Users>> GetUser(AuthorizationFilterContext context)
        {
            Guid id = Guid.Parse(context.HttpContext.Request.Cookies["auth-id"]);
            var getResult = await UsersService.GetBySecurityId(id);
            return getResult;
        }
    }
}