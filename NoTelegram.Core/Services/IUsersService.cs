﻿using CSharpFunctionalExtensions;
using NoTelegram.Core.Models;

namespace NoTelegram.Core.Services
{
    public interface IUsersService
    {
        Task<Result> Register(string userName, string password, string email);
        Task<Result<Guid>> LogIn(string loginType, string login, string password);
        Task<Result> LogOut(Guid id);
        Task<Result<Users>> GetBySecurityId(Guid id);
        Task<Result<Users>> GetByUserId(Guid id);
        Task<Result> EditUser(Guid id, string userName, string email);
        Task<Result> DeleteUser(Guid id);
    }
}