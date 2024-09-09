using NoTelegram.Core.Services;

namespace NoTelegram.API.Filters.Auth
{
    public class AuthFilterWithTimeCheck : BaseAuthFilter
    {
        public AuthFilterWithTimeCheck(IUsersService usersService) : base(usersService)
        {
            Use(HeaderFilter);
            Use(UserIdFilter);
            Use(UserAuthTimeFilter);
        }
    }
}