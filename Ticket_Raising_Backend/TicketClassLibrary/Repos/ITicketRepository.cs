using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public interface ITicketRepository
{
    Task AddTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(string ticketId, Ticket ticket);
    Task DeleteTicketAsync(string ticketId);
    Task<Ticket> GetTicketAsync(string ticketId);
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<List<Ticket>> GetTicketsByEmployeeAsync(string empId);
    Task<List<Ticket>> GetTicketsByStatusAsync(string status);
    Task<List<Ticket>> GetTicketsByTypeAsync(string ticketTypeId);
}
