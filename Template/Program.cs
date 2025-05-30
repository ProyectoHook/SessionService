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
using Infrastructrure.HttpClients;
using Template.Hubs;

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

builder.Services.AddScoped<IParticipantCommand, ParticipantCommand>();
builder.Services.AddScoped<IParticipantQuery, ParticipantQuery>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();

builder.Services.AddHttpClient<IPresentationServiceClient, PresentationServiceClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7112/"); // O desde config
});

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

// política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostPorts", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:3000",
                "http://127.0.0.1:8080",
                "http://127.0.0.1:433",
                "https://127.0.0.1:3000",
                "https://127.0.0.1:8080",
                "https://127.0.0.1:433"
            ) // lo que esta en el browser cuando levanta el server
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR().AddJsonProtocol(options => {
    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhostPorts");

app.UseAuthentication();
app.UseAuthorization();

//ativa sesiones
app.UseSession();

app.UseRouting();

// Hubs de SignalR
app.MapHub<PresentationHub>("/presentationHub"); 
app.MapHub<ChatHub>("/chatHub");

app.MapControllers();

app.Run();
