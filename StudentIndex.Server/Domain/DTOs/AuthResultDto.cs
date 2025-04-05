namespace StudentIndex.Server.Domain.DTOs
{
    public class AuthResultDto
    {
        public bool Succeeded { get; set; }
        public string? Token { get; set; }
        public int StudentId { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
