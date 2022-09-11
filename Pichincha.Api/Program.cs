using Microsoft.EntityFrameworkCore;
using Pichincha.Infrastructure.Database;
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

#endregion

#region Injection Repository


#endregion

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
