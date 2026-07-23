using System.ComponentModel.DataAnnotations;

namespace BelediyeTicketAPI.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "Personel";

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}