using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Application.Services;
using StudentIndex.Server.Infrastructure.Data;
using StudentIndex.Server.Infrastructure.Identity;
using StudentIndex.Server.Infrastructure.Repositories;
using System.Text;

namespace StudentIndex.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<StudentAplikacijaContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<StudentAplikacijaContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? throw new InvalidOperationException("JwtSettings sekcija nedostaje u konfiguraciji.");

        if (string.IsNullOrWhiteSpace(jwtSettings.Secret))
            throw new InvalidOperationException(
                "JwtSettings:Secret nije postavljen. U developmentu ga postavi kroz user-secrets: " +
                "dotnet user-secrets set \"JwtSettings:Secret\" \"<vrijednost>\"");

        services.Configure<JwtSettings>(config.GetSection(JwtSettings.SectionName));

        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPredmetService, PredmetService>();
        services.AddScoped<IPredmetRepository, PredmetRepository>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IIspitRepository, IspitiRepository>();
        services.AddScoped<IPrijavaIspitaRepository, PrijavaIspitaRepository>();
        services.AddScoped<IPrijavaIspitaService, PrijavaIspitaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var allowedOrigins = config.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });

        return services;
    }

    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Unesi JWT token (bez 'Bearer ' prefiksa)."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
