using Microsoft.Data.SqlClient;
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
        catch (DbUpdateException ex)
        {
            SqlException? sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            if (ex.InnerException.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_TicketComment_Ticket_TicketId\""))
                errorNumber = 700;
            else if (ex.InnerException.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_TicketComment_Employee_EmpId\""))
                errorNumber = 701;
            else if (ex.InnerException.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_TicketComment_Employee_Support_Emp_Id\""))
                errorNumber = 702;
            switch (errorNumber)
            {
                case 2627:
                    throw new TicketException("Details already exists", 501);
                case 700:
                    throw new TicketException("Given ticket Id Not Found",502);
                case 701:
                    throw new TicketException("Given Employee Id Not Found",502);
                case 702:
                    throw new TicketException("Given Supplier Empoyee Id Not Found",502);
                default:
                    throw new TicketException(sqlException.Message, 599);
            }
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
        catch{
            throw new TicketException("No Comment Found",599);
        }
        
    }

    public async Task<List<TicketComment>> GetCommentsByEmployeeAsync(string empId)
    {
       
        List<TicketComment> ticketComments = await (from tc in context.TicketComments where tc.EmpId == empId select tc).ToListAsync();
        if (ticketComments.Count() == 0)
        {
            throw new TicketException("No Comments found for given Employee",501);
        }
        return ticketComments;

    }

    public async Task<List<TicketComment>> GetCommentsBySupportEmployeeAsync(string supportEmpId)
    {
        List<TicketComment> ticketComments = await (from tc in context.TicketComments where tc.Support_Emp_Id == supportEmpId select tc).ToListAsync();
        if (ticketComments.Count() == 0)
        {
            throw new TicketException("No Comments found for given Support Employee",501);
        }
        return ticketComments;
    }

    public async Task<List<TicketComment>> GetCommentsByTicketAsync(string ticketId)
    {
        List<TicketComment> ticketComments = await (from tc in context.TicketComments where tc.TicketId == ticketId select tc).ToListAsync();
        if (ticketComments.Count() == 0)
        {
            throw new TicketException("No Comments found for given Ticket",501);
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
            await context.SaveChangesAsync();
        }
        catch(Exception e){
            throw new TicketException($"Error updating comment with ID '{commentId}': {e.Message}",599);
        }
    }
}
