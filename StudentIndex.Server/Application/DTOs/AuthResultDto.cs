namespace StudentIndex.Server.Application.DTOs
{
    public class AuthResultDto
    {
        public bool Succeeded { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
