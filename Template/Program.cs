using Application.UseCases;
using Infrastructrure.Command;
using Infrastructrure.Persistence;
using Infrastructrure.Query;
using JWT.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Interfaces.Services;
using Application.Interfaces.Queries;
using Application.Interfaces.Commands;
using Application.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var key = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyz123456");

builder.Services.AddScoped<ISessionCommand, SessionCommand>();
builder.Services.AddScoped<ISessionQuery, SessionQuery>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped<IAccesCodeCommand, AccesCodeCommand>();
builder.Services.AddScoped<IAccesCodeQuery, AccesCodeQuery>();

builder.Services.AddScoped<IParticipantCommand, ParticipantCommand>();
builder.Services.AddScoped<IParticipantQuery, ParticipantQuery>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();

//Mapper
builder.Services.AddAutoMapper(typeof(Mapping));

//habilita sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//ativa sesiones
app.UseSession();

app.MapControllers();

app.Run();
