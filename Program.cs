using Accounts_api.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using movies_api.Application.Services;
using movies_api.Domain.Entities;
using movies_api.Domain.Interfaces;
using movies_api.Infrastructure.Middlewares;
using movies_api.Infrastructure.Persistence;
using movies_api.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("jwt");
var secretKey = jwtSettings.GetValue<string>("key");
var issuer = jwtSettings.GetValue<string>("issuer");
var audience = jwtSettings.GetValue<string>("audience");

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

//JWT Bearer
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey)),
			ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

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
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

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
