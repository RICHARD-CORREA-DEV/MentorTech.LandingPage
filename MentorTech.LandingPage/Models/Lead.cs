using System.ComponentModel.DataAnnotations;

namespace MentorTech.LandingPage.Models;

public class Lead
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Telefone { get; set; } = string.Empty;

    public string CargoAtual { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Mensagem { get; set; } = string.Empty;

    public DateTime DataCaptura { get; set; } = DateTime.Now;

    public string Status { get; set; } = "Novo";
}
