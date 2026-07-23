using System.ComponentModel.DataAnnotations;

namespace BelediyeTicketAPI.DTOs.User;

public class CreateUserDto
{
    [Required(ErrorMessage = "Ad zorunludur.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad zorunludur.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rol zorunludur.")]
    public string Role { get; set; } = string.Empty;

    [Required(ErrorMessage = "Departman seçilmelidir.")]
    public int DepartmentId { get; set; }
}