using WorkspaceReservas.Configurations;
using WorkspaceReservas.Models;
using WorkspaceReservas.Services;
using WorkspaceReservas.Services.Implementations;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISalaServices, SalaServicesImpl>();
builder.Services.AddScoped<IReservaServices, ReservaServicesImpl>();

//Conexão com o banco
builder.Services.AddDataBaseConfig(builder.Configuration);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
