using System.ComponentModel.DataAnnotations;

namespace Dourfor.Core.Requests.Profiles;

public class UpdateProfileRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição inválida")]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public int Age { get; set; }
}