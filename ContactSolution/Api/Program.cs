using Api.AutoMapperConfigurations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(ContactConfiguration));
builder.Services.AddAutoMapper(typeof(ContactInformationConfiguration));

Service.DI.DependencyLoader.Load(builder.Services);

//builder.Services.AddDbContext<RTDemoContext>(options =>
//{
//    options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("Default"), builder =>
//    {
//        builder.MigrationsAssembly(Assembly.GetAssembly(typeof(RTDemoContext)).GetName().Name);
//    });
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
