using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public interface ITicketTypeRepository
{
    Task AddTicketTypeAsync(TicketType ticketType);
    Task UpdateTicketTypeAsync(string ticketTypeId, TicketType ticketType);
    Task DeleteTicketTypeAsync(string ticketTypeId);
    Task<TicketType> GetTicketTypeAsync(string ticketTypeId);
    Task<List<TicketType>> GetAllTicketTypesAsync();
    Task<List<TicketType>> GetTicketTypesByPriorityAsync(string priorityId);
}
