using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using movies_api.Application.Services;
using movies_api.Domain.Interfaces;
using movies_api.Infrastructure.Middlewares;
using movies_api.Infrastructure.Persistence;
using movies_api.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//GENERAL
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//DOCS
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

//DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDatabase")));

//INJECTIONS
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Development");

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.Run();
