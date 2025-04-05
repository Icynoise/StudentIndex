using Microsoft.AspNetCore.Identity;
using StudentIndex.Server.Domain;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser
{
    // Foreign key to link to the Studenti table
    public int? StudentId { get; set; }

    // Navigation property to the Studenti table
    public virtual Studenti? Student { get; set; }


}
