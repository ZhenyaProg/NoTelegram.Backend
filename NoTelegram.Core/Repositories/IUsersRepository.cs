using NoTelegram.Core.Models;

namespace NoTelegram.Core.Repositories
{
    public interface IUsersRepository
    {
        Task Add(Users users);
        Task<Users?> GetById(Guid id);

        Task<Users?> GetByEmail(string email);
        Task<Users?> GetByName(string name);
    }
}