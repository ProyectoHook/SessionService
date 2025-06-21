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
using Template.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


//JWT KEY
var keyString = builder.Configuration["JwtSettings:SecretKey"];
var key = Encoding.ASCII.GetBytes(keyString);

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

//JWT Config
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // pon false si usas HTTP local
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

builder.Services.AddAuthorization();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(
                              "http://127.0.0.1:5500",
                              "http://localhost:5500"
                          )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});


//SignalR
builder.Services.AddSignalR();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// routing antes de auth
app.UseRouting();

// CORS antes de auth si se usa cookies
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

// sesiones primero
app.UseSession();

// luego auth
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SessionHub>("/sessionHub");
});

app.Run();
