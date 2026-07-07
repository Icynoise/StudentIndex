using Microsoft.AspNetCore.Identity;
using StudentIndex.Server.Domain.Constants;

namespace StudentIndex.Server.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Kreira sistemske role pri startu — role se više ne kreiraju iz register endpointa.
    /// </summary>
    public static async Task SeedIdentityAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Roles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
