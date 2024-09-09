using Microsoft.EntityFrameworkCore;
using NoTelegram.Core.Models;
using NoTelegram.Core.Repositories;
using NoTelegram.DataAccess.PostgreSQL.Entities;

namespace NoTelegram.DataAccess.PostgreSQL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataBaseContext _dbContext;

        public UsersRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Users users)
        {
            UsersEntity usersEntity = new UsersEntity()
            {
                SecurityId = users.SecurityId,
                UserId = users.UserId,
                UserName = users.UserName,
                Email = users.Email,
                Password = users.PasswordHashed
            };

            await _dbContext.Users.AddAsync(usersEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _dbContext.Users
            .Where(user => user.SecurityId == id)
            .ExecuteDeleteAsync();
        }

        public async Task<Users?> GetByEmail(string email)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);
            if(userEntity is null) return null;
            return CreateUser(userEntity);
        }

        public async Task<Users?> GetByName(string name)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.UserName == name);
            if (userEntity is null) return null;
            return CreateUser(userEntity);
        }

        public async Task<Users?> GetBySecurityId(Guid id)
        {
            var userEntity = await _dbContext.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(user => user.SecurityId == id);
            if (userEntity is null) return null;
            return CreateUser(userEntity);
        }

        public async Task<Users?> GetByUserId(Guid id)
        {
            var userEntity = await _dbContext.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(user => user.UserId == id);
            if (userEntity is null) return null;
            return CreateUser(userEntity);
        }

        public async Task LogIn(Guid securityId)
        {
            await _dbContext.Users
                .Where(user => user.SecurityId == securityId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(user => user.Authenticated, true)
                    .SetProperty(s => s.AuthDate, DateTime.Now));
        }

        public async Task LogOut(Guid securityId)
        {
            await _dbContext.Users
               .Where(user => user.SecurityId == securityId)
               .ExecuteUpdateAsync(s => s
                   .SetProperty(user => user.Authenticated, false));
        }

        public async Task Update(Guid id, string userName, string email)
        {
            await _dbContext.Users
                .Where(user => user.SecurityId == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(user => user.UserName, userName)
                    .SetProperty(s => s.Email, email));
        }

        private Users CreateUser(UsersEntity usersEntity)
        {
            return new Users
            {
                SecurityId = usersEntity.SecurityId,
                UserId = usersEntity.UserId,
                UserName = usersEntity.UserName,
                PasswordHashed = usersEntity.Password,
                Email = usersEntity.Email,
                Authenticated = usersEntity.Authenticated,
                AuthDate = usersEntity.AuthDate
            };
        }
    }
}