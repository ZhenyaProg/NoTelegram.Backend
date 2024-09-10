using NoTelegram.Core.Services;

namespace NoTelegram.API.Filters.Auth
{
    public class AuthStandart : BaseAuthFilter
    {
        public AuthStandart(IUsersService usersService) : base(usersService) 
        {
            Use(HeaderFilter);
            Use(UserIdFilter);
        }
    }
}