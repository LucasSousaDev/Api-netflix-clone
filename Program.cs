using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using movies_api.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "API Documentation - API de Filmes",
        Version = "v1",
        Description = "Uma API para servir um WebApp de Filmes.",
        Contact = new OpenApiContact
        {
            Name = "Wesley T. Benette",
            Email = "wesleyteles.b@gmail.com",
            Url = new Uri("https://github.com/WesleyTelesBenette")
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDatabase")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
