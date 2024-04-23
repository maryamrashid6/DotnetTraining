using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using TodoApi.Entities;
using TodoApi.Controllers;
using TodoApi.Services;
using TodoApi.Services.Contracts;
using TodoApi.Services.Core;

var builder = WebApplication.CreateBuilder(args);

// Register services with dependency injection
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

builder.Services.AddDbContext<ToDoContext>(options =>
{
    options.UseSqlServer("Server=MARYUMRASHID-LT\\SQLEXPRESS;Database=ToDoDb;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
