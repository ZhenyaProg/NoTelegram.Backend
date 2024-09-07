using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NoTelegram.API.Contracts;
using NoTelegram.API.Filters;
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
        private readonly IUsersService _usersService;

        public UsersController(
            IValidator<RegisterUserRequest> registerUserValidator,
            IValidator<LoginUserRequest> loginUserValidator,
            IUsersService usersService)
        {
            _registerUserValidator = registerUserValidator;
            _loginUserValidator = loginUserValidator;
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
            {
                return Results.BadRequest(loginResult.Error);
            }

            Response.Headers.Add("auth-id", loginResult.Value.ToString());

            return Results.Ok();
        }

        [HttpDelete]
        [Route("session")]
        [TypeFilter(typeof(AuthFilter))]
        public async Task<IResult> LogOut()
        {
            return Results.Ok();
        }
    }
}