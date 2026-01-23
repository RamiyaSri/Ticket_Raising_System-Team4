using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos
{
    public class EFTicketRepository : ITicketRepository
    {
        private readonly TicketDbContext context;

        public EFTicketRepository()
        {
            context = new TicketDbContext();
        }



        public async Task AddTicketAsync(Ticket ticket)
        {
            try
            {
                ticket.CreationDate ??= DateTime.Now;

                await context.Tickets.AddAsync(ticket);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlEx = ex.InnerException as SqlException;

                if (sqlEx?.Number == 2627)
                    throw new TicketException("Ticket ID already exists", 501);

                throw new TicketException(sqlEx?.Message ?? ex.Message, 599);
            }
        }

 

        public async Task UpdateTicketAsync(string ticketId, Ticket ticket)
        {
            Ticket existingTicket = await GetTicketAsync(ticketId);

            try
            {
                existingTicket.Subject = ticket.Subject;
                existingTicket.Description = ticket.Description;
                existingTicket.Status = ticket.Status;
                existingTicket.TicketTypeId = ticket.TicketTypeId;
                existingTicket.ResolutionDate = ticket.ResolutionDate;

                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new TicketException(ex.Message, 599);
            }
        }

   

        public async Task DeleteTicketAsync(string ticketId)
        {
            Ticket ticket = await context.Tickets
                .Include(t => t.TicketComments)
                .Include(t => t.TicketAssignments)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                throw new TicketException("No such ticket to delete", 502);

            if (ticket.TicketComments.Count > 0)
                throw new TicketException("Cannot delete ticket with comments", 503);

            if (ticket.TicketAssignments.Count > 0)
                throw new TicketException("Cannot delete ticket with assignments", 504);

            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
        }

   

       public async Task<Ticket> GetTicketAsync(string ticketId)
        {
            try
            {
                Ticket ticket = await context.Tickets
                                            .FirstAsync(t => t.TicketId == ticketId);
                return ticket;
            }
            catch
            {
                throw new TicketException("No such ticket ID", 502);
            }
        }
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            List<Ticket> tickets = await context.Tickets.ToListAsync();
            return tickets;
        }
        public async Task<List<Ticket>> GetTicketsByEmployeeAsync(string empId)
            {
            List<Ticket> tickets =
                await (from t in context.Tickets
                    where t.EmpId == empId
                    select t).ToListAsync();
        
            if (tickets.Count() == 0)
            {
                throw new TicketException("No Tickets found for given Employee", 501);
            }
        
            return tickets;
        }

       public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)

{

    List<Ticket> tickets =

        await (from t in context.Tickets

               where t.Status == status

               select t).ToListAsync();
 
    if (tickets.Count() == 0)

    {

        throw new TicketException("No Tickets found for given Status", 501);

    }
 
    return tickets;

}
 

        public async Task<List<Ticket>> GetTicketsByTypeAsync(string ticketTypeId)
{
    List<Ticket> tickets =
        await (from t in context.Tickets
               where t.TicketTypeId == ticketTypeId
               select t).ToListAsync();
 
    if (tickets.Count() == 0)
    {
        throw new TicketException("No Tickets found for given Ticket Type", 501);
    }
 
    return tickets;
}
 
    }
}
