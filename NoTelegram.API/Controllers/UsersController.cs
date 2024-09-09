using Azure;
using Azure.Core;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NoTelegram.API.Contracts;
using NoTelegram.API.Filters.Auth;
using NoTelegram.Core.Models;
using NoTelegram.Core.Services;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace NoTelegram.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IValidator<RegisterUserRequest> _registerUserValidator;
        private readonly IValidator<LoginUserRequest> _loginUserValidator;
        private readonly IValidator<EditUserRequest> _editUserValidator;
        private readonly IUsersService _usersService;

        public UsersController(
            IValidator<RegisterUserRequest> registerUserValidator,
            IValidator<LoginUserRequest> loginUserValidator,
            IValidator<EditUserRequest> editUserValidator,
            IUsersService usersService)
        {
            _registerUserValidator = registerUserValidator;
            _loginUserValidator = loginUserValidator;
            _editUserValidator = editUserValidator;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("auth/profile")]
        public async Task<IResult> Register([FromBody] RegisterUserRequest request)
        {
            ValidationResult validationResult = _registerUserValidator.Validate(request);
            if (validationResult.IsValid is not true)
            {
                return Results.BadRequest(validationResult.ToDictionary().Values);
            }

            Result registerResult = await _usersService.Register(request.UserName, request.Password, request.Email);
            if (registerResult.IsFailure)
            {
                return Results.BadRequest(registerResult.Error);
            }

            return Results.Created();
        }

        [HttpPost]
        [Route("auth/session")]
        public async Task<IResult> LogIn([FromBody] LoginUserRequest request)
        {
            ValidationResult validationResult = _loginUserValidator.Validate(request);
            if (validationResult.IsValid is not true)
            {
                var errors = validationResult.ToDictionary();
                return Results.BadRequest(errors.Values);
            }

            Result<Guid> loginResult = await _usersService.LogIn(request.LoginType, request.Login, request.Password);
            if (loginResult.IsFailure)
                return Results.BadRequest(loginResult.Error);

            Response.Cookies.Append("auth-id", loginResult.Value.ToString());

            return Results.Ok();
        }

        [HttpDelete]
        [Route("session")]
        [TypeFilter(typeof(StandartAuthFilter))]
        public async Task<IResult> LogOut([FromHeader] Guid id)
        {
            Result logoutResult = await _usersService.LogOut(id);
            if (logoutResult.IsFailure)
                return Results.BadRequest(logoutResult.Error);

            Response.Cookies.Delete("auth-id");

            return Results.Ok();
        }

        [HttpGet]
        [Route("data/{userId:guid}")]
        public async Task<IResult> GetUserData(Guid userId)
        {
            var getResult = await _usersService.GetByUserId(userId);
            if (getResult.IsFailure)
                return Results.BadRequest(getResult.Error);

            UserDataResponse response = new UserDataResponse()
            {
                UserId = getResult.Value.UserId,
                UserName = getResult.Value.UserName,
                Email = getResult.Value.Email,
                Authenticated = getResult.Value.Authenticated
            };

            return Results.Ok(response);
        }

        [HttpGet]
        [Route("data")]
        [TypeFilter(typeof(AuthFilterWithTimeCheck))]
        public async Task<IResult> GetPersonallyUserData([FromHeader] Guid securityId)
        {
            var getResult = await _usersService.GetBySecurityId(securityId);
            if (getResult.IsFailure)
                return Results.BadRequest(getResult.Error);

            UserDataResponse response = new UserDataResponse()
            {
                UserId = getResult.Value.UserId,
                UserName = getResult.Value.UserName,
                Email = getResult.Value.Email,
                Authenticated = getResult.Value.Authenticated
            };

            return Results.Ok(response);
        }

        [HttpDelete]
        [Route("")]
        [TypeFilter(typeof(AuthFilterWithTimeCheck))]
        public async Task<IResult> DeleteUser([FromHeader] Guid id)
        {
            var getResult = await _usersService.DeleteUser(id);
            if (getResult.IsFailure)
                return Results.BadRequest(getResult.Error);

            return Results.Ok();
        }

        [HttpPut]
        [Route("data")]
        [TypeFilter(typeof(AuthFilterWithTimeCheck))]
        public async Task<IResult> EditUser([FromHeader] Guid id, [FromBody] EditUserRequest request)
        {
            ValidationResult validationResult = _editUserValidator.Validate(request);
            if (validationResult.IsValid is not true)
            {
                var errors = validationResult.ToDictionary();
                return Results.BadRequest(errors.Values);
            }

            Result editResult = await _usersService.EditUser(id, request.UserName, request.Email);
            if(editResult.IsFailure)
                return Results.BadRequest(editResult.Error);

            return Results.Ok();
        }
    }
}