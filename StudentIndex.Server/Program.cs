using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity; // For Identity
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentIndex.Server.Infrastructure.Data;
using StudentIndex.Server.Infrastructure.Repositories;
using StudentIndex.Application.Interfaces; // Note: You had Server.Application.Interfaces (typo?)
using StudentIndex.Server.Infrastructure.Services;
using System.Text;
using StudentIndex.Application.Services;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<StudentAplikacijaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<StudentAplikacijaContext>()
    .AddDefaultTokenProviders();

// Add JWT Authentication
var jwtSecret = builder.Configuration["JwtSettings:Secret"];
var key = Encoding.UTF8.GetBytes(jwtSecret);
builder.Services.AddAuthentication(options =>
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
        ValidateIssuer = false, // Simplify for now
        ValidateAudience = false // Simplify for now
    };
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application and infrastructure services
builder.Services.AddScoped<IPredmetService, PredmetService>();
builder.Services.AddScoped<IPredmetRepository, PredmetRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IIspitRepository, IspitiRepository>();
builder.Services.AddScoped<IPrijavaIspitaRepository, PrijavaIspitaRepository>();
builder.Services.AddScoped<IPrijavaIspitaService, PrijavaIspitaService>();


builder.Services.AddScoped<IAuthService, AuthService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthentication(); // Add this before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();