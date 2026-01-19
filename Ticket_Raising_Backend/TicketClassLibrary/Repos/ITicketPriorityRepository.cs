using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public interface ITicketPriorityRepository
{
    Task AddPriorityAsync(TicketPriority priority);
    Task UpdatePriorityAsync(string priorityId, TicketPriority priority);
    Task DeletePriorityAsync(string priorityId);
    Task<TicketPriority> GetPriorityAsync(string priorityId);
    Task<List<TicketPriority>> GetAllPrioritiesAsync();
    Task<TicketPriority> GetPriorityByLevelAsync(string priorityLevel);
}
