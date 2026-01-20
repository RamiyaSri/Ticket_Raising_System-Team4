using TicketClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace TicketClassLibrary.Repos;

public class EFTicketPriorityRepository : ITicketPriorityRepository
{
    TicketDbContext context = new TicketDbContext();

    public async Task AddPriorityAsync(TicketPriority priority)
    {
        try
        {
            await context.TicketPriorities.AddAsync(priority);
            await context.SaveChangesAsync();
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

    public async Task UpdatePriorityAsync(string priorityId, TicketPriority priority)
    {
        TicketPriority priority2edit = await GetPriorityAsync(priorityId);

        try
        {
            priority2edit.PriorityLevel = priority.PriorityLevel;
            priority2edit.PriorityDescription = priority.PriorityDescription;
            priority2edit.ResponseTime = priority.ResponseTime;
            priority2edit.ResolutionTime = priority.ResolutionTime;

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599);
        }
    }

    public async Task DeletePriorityAsync(string priorityId)
    {
        TicketPriority priority2del =
            await context.TicketPriorities
                         .Include("TicketTypes")
                         .FirstOrDefaultAsync(p => p.PriorityId == priorityId);

        if (priority2del.TicketTypes.Count == 0)
        {
            context.TicketPriorities.Remove(priority2del);
            await context.SaveChangesAsync();
        }
        
        if (priority2del == null)
        {
            throw new TicketException("No such priority ID", 502);
        }
        else
        {
            throw new TicketException("Cannot delete priority with existing ticket types", 503);
        }
    }

    public async Task<TicketPriority> GetPriorityAsync(string priorityId)
    {
        try
        {
            TicketPriority priority =
                await context.TicketPriorities.FirstAsync(p => p.PriorityId == priorityId);
            return priority;
        }
        catch
        {
            throw new TicketException("No such priority ID", 502);
        }
    }

    public async Task<List<TicketPriority>> GetAllPrioritiesAsync()
    {
        List<TicketPriority> priorities =
            await context.TicketPriorities.ToListAsync();
        return priorities;
    }

    public async Task<TicketPriority> GetPriorityByLevelAsync(string priorityLevel)
    {
        try
        {
            TicketPriority priority =
                await context.TicketPriorities.FirstAsync(p => p.PriorityLevel == priorityLevel);
            return priority;
        }
        catch
        {
            throw new TicketException("No such priority level", 502);
        }
    }
}
