using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
//MediatoR Configuration
builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(GetProductByIdQuery))));
//Repositories Configuration
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();

//Api Versions Configuration
builder.Services.AddApiVersioning(option =>
{
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "This is API For Catalog Microservice in E_Commerce Application",
        Contact = new OpenApiContact
        {
            Name = "NWF Hefni",
            Email = "eng.nwaf@gmail.com",
            Url = new Uri("http://myWebsite.com")
        }
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
