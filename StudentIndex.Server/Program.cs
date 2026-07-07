using Microsoft.Extensions.FileProviders;
using StudentIndex.Server.Extensions;
using StudentIndex.Server.WebApi;
using StudentIndex.Server.WebApi.ModelBinding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.ModelBinderProviders.Insert(0, new QueryParametersModelBinderProvider()));
builder.Services.AddSwaggerWithJwt();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddCorsPolicy(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

await app.SeedIdentityAsync();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// In production, serve the pre-built Angular files from the dist folder
if (!app.Environment.IsDevelopment())
{
    var spaPath = Path.Combine(app.Environment.ContentRootPath, "..", "ClientApp", "dist", "client-app", "browser");
    var spaFileOptions = new StaticFileOptions { FileProvider = new PhysicalFileProvider(spaPath) };
    app.UseStaticFiles(spaFileOptions);
    app.MapFallbackToFile("index.html", spaFileOptions);
}

app.Run();
