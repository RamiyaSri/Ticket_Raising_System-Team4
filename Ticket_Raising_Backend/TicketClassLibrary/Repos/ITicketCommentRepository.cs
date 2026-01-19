using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public interface ITicketCommentRepository
{
    Task AddCommentAsync(TicketComment comment);
    Task UpdateCommentAsync(string commentId, TicketComment comment);
    Task DeleteCommentAsync(string commentId);
    Task<TicketComment> GetCommentAsync(string commentId);
    Task<List<TicketComment>> GetAllCommentsAsync();
    Task<List<TicketComment>> GetCommentsByTicketAsync(string ticketId);
    Task<List<TicketComment>> GetCommentsByEmployeeAsync(string empId);
    Task<List<TicketComment>> GetCommentsBySupportEmployeeAsync(string supportEmpId);


}
