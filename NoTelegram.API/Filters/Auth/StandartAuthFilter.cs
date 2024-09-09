using NoTelegram.Core.Services;

namespace NoTelegram.API.Filters.Auth
{
    public class StandartAuthFilter : BaseAuthFilter
    {
        public StandartAuthFilter(IUsersService usersService) : base(usersService) 
        {
            Use(HeaderFilter);
            Use(UserIdFilter);
        }
    }
}