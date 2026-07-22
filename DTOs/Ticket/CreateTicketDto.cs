namespace BelediyeTicketAPI.DTOs.Ticket;

public class CreateTicketDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }
}