using Microsoft.EntityFrameworkCore;
using Pichincha.Api.Middleware;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Database;
using Pichincha.Infrastructure.Repositories;
using Pichincha.Services.Implementations;
using Pichincha.Services.Intefaces;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//if (environment.IsProduction())
//{
Console.WriteLine("--> Using SqlServer DB");
builder.Services.AddDbContext<AppDbContext>(opt =>
                 opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
Console.WriteLine($"Server DB {configuration.GetConnectionString("DefaultConnection")}");
//}
//else
//{

//    Console.WriteLine("--> Using InMem DB");
//    builder.Services.AddDbContext<AppDbContext>(opt =>
//                     opt.UseInMemoryDatabase("InMem"));
//}

#region Injection Services

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();

#endregion

#region Injection Repository

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
