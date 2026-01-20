using TicketClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace TicketClassLibrary.Repos;

public class EFTicketTypeRepository : ITicketTypeRepository
{
    TicketDbContext ticketDbContext = new TicketDbContext();

    public async Task AddTicketTypeAsync(TicketType ticketType)
    {
        try
        {
            await ticketDbContext.TicketTypes.AddAsync(ticketType);
            await ticketDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;

            switch (errorNumber)
            {
                case 2627:
                    throw new TicketException("Details already exists", 501);
                default:
                    throw new TicketException(sqlException.Message, 599);
            }
        }
    }

    public async Task UpdateTicketTypeAsync(string ticketTypeId, TicketType ticketType)
    {
        TicketType ticketTypeToEdit = await GetTicketTypeAsync(ticketTypeId);

        try
        {
            ticketTypeToEdit.TypeName = ticketType.TypeName;
            ticketTypeToEdit.PriorityId = ticketType.PriorityId;           
            ticketTypeToEdit.Description = ticketType.Description;

            await ticketDbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599);
        }
    }

    public async Task DeleteTicketTypeAsync(string ticketTypeId)
    {
        TicketType ticketTypeToDelete = await ticketDbContext.TicketTypes
                .Include("Tickets")
                .FirstOrDefaultAsync(ticketTypeEntity => ticketTypeEntity.TicketTypeId == ticketTypeId);

        if (ticketTypeToDelete.Tickets.Count == 0)
        {
            ticketDbContext.TicketTypes.Remove(ticketTypeToDelete);
            await ticketDbContext.SaveChangesAsync();
        }
        else
        {
            throw new TicketException("Cannot delete Ticket Type with existing Tickets", 503);
        }
    }

    public async Task<TicketType> GetTicketTypeAsync(string ticketTypeId)
    {
        try
        {
            TicketType ticketType =
                await ticketDbContext.TicketTypes
                    .FirstAsync(ticketTypeEntity => ticketTypeEntity.TicketTypeId == ticketTypeId);
            return ticketType;
        }
        catch
        {
            throw new TicketException("No such Ticket Type Id", 502);
        }
    }

    public async Task<List<TicketType>> GetAllTicketTypesAsync()
    {
        List<TicketType> ticketTypeList =
            await ticketDbContext.TicketTypes.ToListAsync();
        return ticketTypeList;
    }

    public async Task<List<TicketType>> GetTicketTypesByPriorityAsync(string priorityId)
    {
        try
        {
            List<TicketType> ticketTypeList =
                await ticketDbContext.TicketTypes
                    .Where(ticketTypeEntity => ticketTypeEntity.PriorityId == priorityId)
                    .ToListAsync();
            return ticketTypeList;
        }
        catch
        {
            throw new TicketException("No Ticket Types found for given Priority", 504);
        }
    }
}

