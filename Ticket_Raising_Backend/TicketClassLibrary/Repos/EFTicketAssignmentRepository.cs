using System;
using Microsoft.EntityFrameworkCore;
using TicketClassLibrary.Exceptions;
using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public class EFTicketAssignmentRepository : ITicketAssignmentRepository
{
    TicketDbContext context = new TicketDbContext();
    public async Task AddAssignmentAsync(TicketAssignment assignment)
    {
        try
        {
            await context.TicketAssignments.AddAsync(assignment);
            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new TicketException("Unexpected error while assigning ticket",599);
        }
    }

    public async Task DeleteAssignmentAsync(string assignmentId)
    {
        
        TicketAssignment assignment2del = await GetAssignmentAsync(assignmentId);
        try
        {
            context.TicketAssignments.Remove(assignment2del);
            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new TicketException("Assignment ID does not exsist for delete",599);
        }
        
    }

    public async Task<List<TicketAssignment>> GetAllAssignmentsAsync()
    {
        return await context.TicketAssignments.ToListAsync();
    }

    public async Task<TicketAssignment?> GetAssignmentAsync(string assignmentId)
    {
        try
        {
            TicketAssignment assignment2getall = await (from a in context.TicketAssignments 
                                                where a.AssignmentId == assignmentId
                                                select a).FirstAsync();
            return assignment2getall;
        }
        catch (Exception)
        {
            throw new TicketException("Assignment ID does not exsist",502);
        }
    }

    public async Task<List<TicketAssignment>> GetAssignmentsBySupportEmployeeAsync(string supportEmpId)
    {
        return await (from a in context.TicketAssignments
                    where a.Support_Emp_Id == supportEmpId
                    select a).ToListAsync();
    }

    public async Task<List<TicketAssignment?>> GetAssignmentsByTicketAsync(string ticketId)
    {
        return await (from a in context.TicketAssignments
                    where a.TicketId == ticketId
                    select a).ToListAsync();
    }

    public async Task UpdateAssignmentAsync(string assignmentId, TicketAssignment updatedAssignment)
    {
        TicketAssignment assignment2update = await GetAssignmentAsync(assignmentId);
            try
            {
                assignment2update.TicketId = updatedAssignment.TicketId;
                assignment2update.Support_Emp_Id = updatedAssignment.Support_Emp_Id;
                assignment2update.AssignmentDate = updatedAssignment.AssignmentDate;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException("ex.message", 599);
            }
    }
}
