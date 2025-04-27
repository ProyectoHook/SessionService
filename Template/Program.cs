using Application.Interfaces.InterfaceSession;
using Application.UseCases;
using Infrastructrure.Command;
using Infrastructrure.Persistence;
using Infrastructrure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<ISessionCommand,SessionCommand>();
builder.Services.AddScoped<ISessionQuery, SessionQuery>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped<IParticipantCommand, ParticipantCommand>();
builder.Services.AddScoped<IParticipantQuery, ParticipantQuery>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
