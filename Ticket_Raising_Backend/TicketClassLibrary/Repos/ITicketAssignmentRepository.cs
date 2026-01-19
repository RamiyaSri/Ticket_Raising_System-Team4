using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public interface ITicketAssignmentRepository
{
    Task AddAssignmentAsync(TicketAssignment assignment);
    Task UpdateAssignmentAsync(string assignmentId, TicketAssignment assignment);
    Task DeleteAssignmentAsync(string assignmentId);
    Task<TicketAssignment?> GetAssignmentAsync(string assignmentId);
    Task<List<TicketAssignment>> GetAllAssignmentsAsync();
    Task<List<TicketAssignment?>> GetAssignmentsByTicketAsync(string ticketId);
    Task<List<TicketAssignment>> GetAssignmentsBySupportEmployeeAsync(string supportEmpId);
}

