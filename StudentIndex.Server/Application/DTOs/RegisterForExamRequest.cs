using System.ComponentModel.DataAnnotations;

namespace StudentIndex.Server.Application.DTOs;

public class RegisterForExamRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "IspitId mora biti pozitivan broj.")]
    public int IspitId { get; set; }
}
