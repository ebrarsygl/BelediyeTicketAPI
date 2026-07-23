using System.ComponentModel.DataAnnotations;

namespace BelediyeTicketAPI.DTOs.Ticket;

public class CreateTicketDto
{
    [Required(ErrorMessage = "Başlık zorunludur.")]
    [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Durum zorunludur.")]
    public string Status { get; set; } = "Beklemede";

    [Required(ErrorMessage = "Kategori seçilmelidir.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Kullanıcı seçilmelidir.")]
    public int UserId { get; set; }
}