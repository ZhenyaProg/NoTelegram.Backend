using CSharpFunctionalExtensions;
using NoTelegram.Core.Auth;
using NoTelegram.Core.Models;
using NoTelegram.Core.Repositories;
using NoTelegram.Core.Services;

namespace NoTelegram.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> EditUser(Guid id, string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Users>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> LogOut(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Register(string userName, string password, string email)
        {
            {
                Users? user = await _usersRepository.GetByEmail(email);
                if(user is not null)
                    if(user.UserName == userName)
                        return Result.Failure("Такой никнейм занят");
            }
            
            {
                string hashedPassword = _passwordHasher.Generate(password);
                Users user = new Users(Guid.NewGuid(), userName, hashedPassword, email);

                await _usersRepository.Add(user);
            }

            return Result.Success();
        }

        public async Task<Result<Guid>> LogIn(string loginType, string login, string password)
        {
            Users? users = loginType switch
            {
                "email" => await _usersRepository.GetByEmail(login),
                "un" => await _usersRepository.GetByName(login),
                _ => throw new ArgumentException(loginType)
            };

            if(users is null)
                return Result.Failure<Guid>("не найден аккаунт с таким логином");

            bool verify = _passwordHasher.Verify(password, users.PasswordHashed);
            if(verify is false)
                return Result.Failure<Guid>("неверный пароль");

            return users.Id;
        }
    }
}