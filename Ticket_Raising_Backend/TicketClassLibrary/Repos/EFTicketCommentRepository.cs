using Microsoft.EntityFrameworkCore;
using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public class EFTicketCommentRepository : ITicketCommentRepository
{
    TicketDbContext context = new TicketDbContext();
    public async Task AddCommentAsync(TicketComment comment)
    {
        try
        {
            await context.TicketComments.AddAsync(comment);
            await context.SaveChangesAsync();
        }
        catch(Exception e){
            throw new TicketException(e.Message,599);
        }
    }

    public async Task DeleteCommentAsync(string commentId)
    {
        TicketComment ticketComment = await GetCommentAsync(commentId);
        try
        {
            context.TicketComments.Remove(ticketComment);
            await context.SaveChangesAsync();
        }
        catch(Exception e){
            throw new TicketException(e.Message,599);
        }
    }

    public async Task<List<TicketComment>> GetAllCommentsAsync()
    {
        List<TicketComment> ticketComments = await context.TicketComments.ToListAsync();
        return ticketComments;
    }

    public async Task<TicketComment> GetCommentAsync(string commentId)
    {
        try
        {
            TicketComment ticketComment = await (from tc in context.TicketComments where tc.CommentId == commentId select tc).FirstAsync();
            return ticketComment;
        }
        catch(Exception e){
            throw new TicketException(e.Message,599);
        }
        
    }

    public async Task<List<TicketComment>> GetCommentsByEmployeeAsync(string empId)
    {
       
        List<TicketComment> ticketComments = await (from tc in context.TicketComments where tc.EmpId == empId select tc).ToListAsync();
        if (ticketComments.Count() == 0)
        {
            throw new TicketException("No Comments found for given Employee ID",501);
        }
        return ticketComments;

    }

    public async Task<List<TicketComment>> GetCommentsBySupportEmployeeAsync(string supportEmpId)
    {
        List<TicketComment> ticketComments = await (from tc in context.TicketComments where tc.Support_Emp_Id == supportEmpId select tc).ToListAsync();
        if (ticketComments.Count() == 0)
        {
            throw new TicketException("No Comments found for given Support Employee ID",501);
        }
        return ticketComments;
    }

    public async Task<List<TicketComment>> GetCommentsByTicketAsync(string ticketId)
    {
        List<TicketComment> ticketComments = await (from tc in context.TicketComments where tc.TicketId == ticketId select tc).ToListAsync();
        if (ticketComments.Count() == 0)
        {
            throw new TicketException("No Comments found for given Ticket ID",501);
        }
        return ticketComments;
    }

    public async Task UpdateCommentAsync(string commentId, TicketComment comment)
    {
        TicketComment ticketComment2Edit = await GetCommentAsync(commentId);
        try
        {
            ticketComment2Edit.CommentId = comment.CommentId;
            ticketComment2Edit.TicketId = comment.TicketId;
            ticketComment2Edit.EmpId = comment.EmpId;
            ticketComment2Edit.Support_Emp_Id = comment.Support_Emp_Id;
            ticketComment2Edit.CommentText = comment.CommentText;
            ticketComment2Edit.CommentDate = comment.CommentDate;
        }
        catch(Exception e){
            throw new TicketException(e.Message,599);
        }
    }
}
