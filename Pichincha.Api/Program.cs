using AutoMapper;
using Pichincha.Api.DependencyInjection;
using Pichincha.Api.Middleware;
using Pichincha.Services.Profiles;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Custom dependencies
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddDatabase(builder.Configuration);

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PichinchaMap());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

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
