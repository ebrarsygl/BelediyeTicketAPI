using System.ComponentModel.DataAnnotations;

namespace BelediyeTicketAPI.DTOs.Category;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}