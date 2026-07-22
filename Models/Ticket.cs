namespace BelediyeTicketAPI.Models;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Beklemede";

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}