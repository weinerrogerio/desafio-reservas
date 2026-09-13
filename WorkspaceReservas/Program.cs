using AutoMapper;
using Serilog;
using WorkspaceReservas.Configurations;
using WorkspaceReservas.Models;
using WorkspaceReservas.Services;
using WorkspaceReservas.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add seilog.
builder.AddSerilogLogging();

// Add services to the container.
builder.Services.AddScoped<ISalaServices, SalaServicesImpl>();
builder.Services.AddScoped<IReservaServices, ReservaServicesImpl>();

Log.Information("CONECTANDO AO BANCO.........................");

//Conexão com o banco
builder.Services.AddDataBaseConfig(builder.Configuration);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

Log.Information("SUCESSO AO SE CONECTAR AO BANCO.................................");


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
