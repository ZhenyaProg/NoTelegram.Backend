using NoTelegram.Core.Services;

namespace NoTelegram.API.Filters.Auth
{
    public class AuthWithTime : BaseAuthFilter
    {
        public AuthWithTime(IUsersService usersService) : base(usersService)
        {
            Use(HeaderFilter);
            Use(UserIdFilter);
            Use(UserAuthTimeFilter);
        }
    }
}