using NoTelegram.Core.Models;

namespace NoTelegram.Core.Repositories
{
    public interface IUsersRepository
    {
        Task Add(Users users);
        Task Delete(Guid id);
        Task<Users?> GetById(Guid id);

        Task<Users?> GetByEmail(string email);
        Task<Users?> GetByName(string name);
        Task Update(Guid id, string userName, string email);
    }
}