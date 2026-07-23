using System.ComponentModel.DataAnnotations;

namespace BelediyeTicketAPI.DTOs.Ticket;

public class UpdateTicketDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık zorunludur.")]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Durum zorunludur.")]
    public string Status { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kategori seçilmelidir.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Kullanıcı seçilmelidir.")]
    public int UserId { get; set; }      // EKLE
}