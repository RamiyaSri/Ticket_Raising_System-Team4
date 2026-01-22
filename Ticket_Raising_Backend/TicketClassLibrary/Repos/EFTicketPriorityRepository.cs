using TicketClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TicketClassLibrary.Repos;

public class EFTicketPriorityRepository : ITicketPriorityRepository
{
    private readonly TicketDbContext context = new TicketDbContext();

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
            int errorNumber = sqlException?.Number ?? 0;

            switch (errorNumber)
            {
                case 2627:
                    throw new TicketException("Details already exists", 501);
                default:
                    throw new TicketException(sqlException?.Message ?? "Database error", 599);
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
            throw new TicketException(sqlException?.Message ?? "Database error", 599);
        }
    }

    public async Task DeletePriorityAsync(string priorityId)
    {
        TicketPriority priority2del =
            await context.TicketPriorities
                         .Include(p => p.TicketTypes)
                         .FirstOrDefaultAsync(p => p.PriorityId == priorityId);

        if (priority2del == null)
            throw new TicketException("No such priority ID", 502);

        if (priority2del.TicketTypes != null && priority2del.TicketTypes.Count > 0)
            throw new TicketException("Cannot delete priority with existing ticket types", 503);

        context.TicketPriorities.Remove(priority2del);
        await context.SaveChangesAsync();
    }

    public async Task<TicketPriority> GetPriorityAsync(string priorityId)
    {
        try
        {
            return await context.TicketPriorities
                .FirstAsync(p => p.PriorityId == priorityId);
        }
        catch
        {
            throw new TicketException("No such priority ID", 502);
        }
    }

    public async Task<List<TicketPriority>> GetAllPrioritiesAsync()
    {
        return await context.TicketPriorities.ToListAsync();
    }

    public async Task<List<TicketPriority>> GetPriorityByLevelAsync(string priorityLevel)
    {
    if (string.IsNullOrWhiteSpace(priorityLevel))
        throw new TicketException("Priority level cannot be empty", 400);

    List<TicketPriority> priorities =
        await context.TicketPriorities
            .Where(p => EF.Functions.Like(p.PriorityLevel, priorityLevel))
            .ToListAsync();

    if (priorities.Count == 0)
        throw new TicketException("No such priority level", 404);

    return priorities;
    }
   
}
