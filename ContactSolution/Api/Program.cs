using Api.AutoMapperConfigurations;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

const string _defaultConnectionString = "Default";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(ContactConfiguration));
builder.Services.AddAutoMapper(typeof(ContactInformationConfiguration));

Service.DI.DependencyLoader.Load(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RTDemoContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString(_defaultConnectionString)));

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RTDemoContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
