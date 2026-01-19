using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using TicketClassLibrary.Models;
 
namespace TicketClassLibrary.Repos
{
    
    public class TicketException : Exception
    {
        public int ErrorCode { get; set; }
 
        public TicketException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
 
    public class EFTicketRepository : ITicketRepository
    {
        TicketDbContext context = new TicketDbContext();
 
        public async Task AddTicketAsync(Ticket ticket)
        {
            try
            {
                await context.Tickets.AddAsync(ticket);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlException = ex.InnerException as SqlException;
                int errorNumber = sqlException?.Number ?? 0;
 
                switch (errorNumber)
                {
                    case 2627: 
                        throw new TicketException("Ticket ID already exists", 501);
                    default:
                        throw new TicketException(sqlException?.Message ?? ex.Message, 599);
                }
            }
        }
 
        public async Task UpdateTicketAsync(string ticketId, Ticket ticket)
        {
            Ticket ticket2edit = await GetTicketAsync(ticketId);
 
            try
            {
                ticket2edit.Subject = ticket.Subject;
                ticket2edit.Description = ticket.Description;
                ticket2edit.Status = ticket.Status;
                ticket2edit.TicketTypeId = ticket.TicketTypeId;
                ticket2edit.EmpId = ticket.EmpId;
                ticket2edit.ResolutionDate = ticket.ResolutionDate;
 
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlException = ex.InnerException as SqlException;
                throw new TicketException(sqlException?.Message ?? ex.Message, 599);
            }
        }
 
        public async Task DeleteTicketAsync(string ticketId)
        {
            Ticket ticket2del = await context.Tickets
                                             .Include(t => t.TicketComments)
                                             .FirstOrDefaultAsync(t => t.TicketId == ticketId);
 
            if (ticket2del == null)
                throw new TicketException("No such ticket to delete", 502);
 
            if (ticket2del.TicketComments.Count == 0)
            {
                context.Tickets.Remove(ticket2del);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new TicketException("Cannot delete ticket with comments", 503);
            }
        }
 
        public async Task<Ticket> GetTicketAsync(string ticketId)
        {
            try
            {
                Ticket ticket = await context.Tickets.FirstAsync(t => t.TicketId == ticketId);
                return ticket;
            }
            catch
            {
                throw new TicketException("No such ticket ID", 502);
            }
        }
 
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await context.Tickets.ToListAsync();
        }
 
        public async Task<List<Ticket>> GetTicketsByEmployeeAsync(string empId)
        {
            return await context.Tickets
                                .Where(t => t.EmpId == empId)
                                .ToListAsync();
        }
 
        public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
        {
            return await context.Tickets
                                .Where(t => t.Status == status)
                                .ToListAsync();
        }
 
        public async Task<List<Ticket>> GetTicketsByTypeAsync(string ticketTypeId)
        {
            return await context.Tickets
                                .Where(t => t.TicketTypeId == ticketTypeId)
                                .ToListAsync();
        }
    }
}