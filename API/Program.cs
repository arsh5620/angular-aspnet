using System.Text;
using API;
using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DefaultDbContext>(param =>
{
    param.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSqlite"));
});
builder.Services.AddScoped<AuthTokenService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddAutoMapper((mapperConfig) =>
{
    mapperConfig.AddProfile<DefaultAutoMapperProfile>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer((options) =>
    {
        var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("TokenKey cannot be null");
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;

try
{
    var dbContext = serviceProvider.GetRequiredService<DefaultDbContext>();
    var seedUsersLogger = serviceProvider.GetRequiredService<ILogger<SeedUsers>>();
    await dbContext.Database.MigrateAsync();

    await SeedUsers.SeedUsersAsync(dbContext, seedUsersLogger);
}
catch (Exception ex)
{
    var programLogger = serviceProvider.GetRequiredService<ILogger<Program>>();
    programLogger.LogError(ex.Message);
}

app.Run();


class DefaultAutoMapperProfile : Profile
{
    public DefaultAutoMapperProfile()
    {
        CreateMap<AppUser, UserResponseDto>()
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
    }
}
