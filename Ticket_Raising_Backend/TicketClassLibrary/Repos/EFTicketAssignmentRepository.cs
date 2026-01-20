using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        List<TicketAssignment> ticketAssignments =  await context.TicketAssignments.ToListAsync();
        return ticketAssignments;
    }

    public async Task<TicketAssignment?> GetAssignmentAsync(string assignmentId)
    {
        try
        {
            TicketAssignment assignment2get= await (from a in context.TicketAssignments 
                                                where a.AssignmentId == assignmentId
                                                select a).FirstAsync();
            return assignment2get;
        }
        catch (Exception)
        {
            throw new TicketException("Assignment ID does not exsist",502);
        }
    }

    public async Task<List<TicketAssignment>> GetAssignmentsBySupportEmployeeAsync(string supportEmpId)
    {
        List<TicketAssignment> ticketAssignments =  await (from a in context.TicketAssignments
                    where a.Support_Emp_Id == supportEmpId
                    select a).ToListAsync();
        if(ticketAssignments.Count == 0)
            throw new TicketException("No new Assignments found for Support Employee ID",501);
        return ticketAssignments;
    }

    public async Task<List<TicketAssignment?>> GetAssignmentsByTicketAsync(string ticketId)
    {
        List<TicketAssignment> ticketAssignments =  await (from a in context.TicketAssignments
                    where a.TicketId == ticketId
                    select a).ToListAsync();
        if(ticketAssignments.Count == 0)
            throw new TicketException("No new assignments found for Ticket ID",501);

        return ticketAssignments;
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
            catch
            {
                throw new TicketException("ex.message", 599);
            }
    }
}
