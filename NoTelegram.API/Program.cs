using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NoTelegram.API.Contracts;
using NoTelegram.API.Validators;
using NoTelegram.Application.Services;
using NoTelegram.Core.Auth;
using NoTelegram.Core.Repositories;
using NoTelegram.Core.Services;
using NoTelegram.DataAccess.PostgreSQL;
using NoTelegram.DataAccess.PostgreSQL.Repositories;
using NoTelegram.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddTransient<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
        builder.Services.AddTransient<IValidator<LoginUserRequest>, LoginUserRequestValidator>();
        builder.Services.AddTransient<IValidator<EditUserRequest>, EditUserRequestValidator>();

        builder.Services.AddTransient<IUsersService, UsersService>();
        builder.Services.AddTransient<IUsersRepository, UsersRepository>();

        builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        builder.Services.AddDbContext<DataBaseContext>(
            options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DataBaseContext)));
            });

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}