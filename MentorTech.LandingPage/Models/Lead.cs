using System.ComponentModel.DataAnnotations;

namespace MentorTech.LandingPage.Models;

public class Lead
{
    private string? _telefone;

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone e obrigatorio.")]
    [StringLength(20)]
    public string Telefone
    {
        get => _telefone ?? string.Empty;
        set => _telefone = value != null ? new string(value.Where(char.IsDigit).ToArray()) : null;
    }

    public string CargoAtual { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Mensagem { get; set; } = string.Empty;

    public DateTime DataCaptura { get; set; } = DateTime.Now;

    public string Status { get; set; } = "Novo";
}
