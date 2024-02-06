using System.ComponentModel.DataAnnotations;

namespace RapidPayApi.Models;

public class AuthenticateModel
{
    [Required]
    public string? Username { get; }

    [Required]
    public string? Password { get; }
}