﻿using CSharpFunctionalExtensions;
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
            Result<Users> getResult = await GetBySecurityId(id);
            if(getResult.IsFailure)
                return Result.Failure(getResult.Error);

            await _usersRepository.Delete(id);

            return Result.Success();
        }

        public async Task<Result> EditUser(Guid id, string userName, string email)
        {
            Result<Users> getResult = await GetBySecurityId(id);
            if (getResult.IsFailure)
                return Result.Failure(getResult.Error);

            await _usersRepository.Update(id, userName, email);
            return Result.Success();
        }

        public async Task<Result<Users>> GetBySecurityId(Guid id)
        {
            Users? user = await _usersRepository.GetBySecurityId(id);
            return user == null ? Result.Failure<Users>("Нет пользователя с таким id")
                                : Result.Success(user);
        }

        public async Task<Result<Users>> GetByUserId(Guid id)
        {
            Users? user = await _usersRepository.GetByUserId(id);
            return user == null ? Result.Failure<Users>("Нет пользователя с таким id")
                                : Result.Success(user);
        }

        public async Task<Result> LogOut(Guid id)
        {
            var getResult = await GetBySecurityId(id);
            if(getResult.IsFailure)
                return Result.Failure(getResult.Error);

            await _usersRepository.LogOut(id);

            return Result.Success();
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
                Users user = new Users
                {
                    SecurityId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    UserName = userName,
                    PasswordHashed = hashedPassword,
                    Email = email
                };

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

            await _usersRepository.LogIn(users.SecurityId);

            return users.SecurityId;
        }
    }
}