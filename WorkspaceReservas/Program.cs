using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Serilog;
using WorkspaceReservas.Configurations;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Services;
using WorkspaceReservas.Services.Implementations;
using WorkspaceReservas.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add seilog.
builder.AddSerilogLogging();

// Add services to the container.
builder.Services.AddScoped<ISalaServices, SalaServicesImpl>();
builder.Services.AddScoped<IReservaServices, ReservaServicesImpl>();
builder.Services.AddScoped<IMedicosServices, MedicosServicesImpl>();
builder.Services.AddScoped<IConsultaServices, ConsultaServicesImpl>();


//Conexão com o banco
builder.Services.AddDataBaseConfig(builder.Configuration);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//Adicionando o FluentValidation e registrando os validadores
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<SalaRequestValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<SalaUpdateValidator>();// não é necessário, pois o FluentValidation já valida os campos não nulos do DTO de atualização
builder.Services.AddValidatorsFromAssemblyContaining<ReservaRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ConsultaRequestValidator>();

builder.Services.AddControllers();

// Mapeamento de objetos usando AutoMapper - geral
TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
