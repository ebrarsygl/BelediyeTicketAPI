using System.ComponentModel.DataAnnotations;

namespace BelediyeTicketAPI.DTOs.Category;

public class UpdateCategoryDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = string.Empty;
}