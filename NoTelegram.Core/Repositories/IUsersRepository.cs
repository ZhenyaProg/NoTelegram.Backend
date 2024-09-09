using NoTelegram.Core.Models;

namespace NoTelegram.Core.Repositories
{
    public interface IUsersRepository
    {
        Task Add(Users users);
        Task Delete(Guid id);
        Task<Users?> GetBySecurityId(Guid securityId);
        Task<Users?> GetByUserId(Guid userId);

        Task<Users?> GetByEmail(string email);
        Task<Users?> GetByName(string name);
        Task Update(Guid securityId, string userName, string email);

        Task LogIn(Guid securityId);
        Task LogOut(Guid securityId);
    }
}