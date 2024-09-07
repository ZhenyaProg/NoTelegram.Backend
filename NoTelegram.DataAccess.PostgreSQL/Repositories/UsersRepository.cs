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
                Id = users.Id,
                UserName = users.UserName,
                Email = users.Email,
                Password = users.PasswordHashed
            };

            await _dbContext.Users.AddAsync(usersEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Users?> GetByEmail(string email)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);
            if(userEntity is null) return null;
            return new Users(userEntity.Id, userEntity.UserName, userEntity.Password, userEntity.Email);
        }

        public async Task<Users?> GetByName(string name)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.UserName == name);
            if (userEntity is null) return null;
            return new Users(userEntity.Id, userEntity.UserName, userEntity.Password, userEntity.Email);
        }

        public Task<Users?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}