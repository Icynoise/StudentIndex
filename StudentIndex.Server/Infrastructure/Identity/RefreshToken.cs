namespace StudentIndex.Server.Infrastructure.Identity;

public class RefreshToken
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    /// <summary>SHA-256 heš tokena — sirovi token se nikada ne čuva u bazi.</summary>
    public string TokenHash { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
