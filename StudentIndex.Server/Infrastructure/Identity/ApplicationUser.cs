using Microsoft.AspNetCore.Identity;
using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public int? StudentId { get; set; }
    public virtual Studenti? Student { get; set; }
}
