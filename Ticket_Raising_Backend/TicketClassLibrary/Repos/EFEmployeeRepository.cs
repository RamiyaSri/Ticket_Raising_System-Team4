using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketClassLibrary.Models;
 
namespace TicketClassLibrary.Repos;
 
public class EFEmployeeRepository : IEmployeeRepository
{
    TicketDbContext context = new TicketDbContext();
 
    public async Task AddEmployeeAsync(Employee employee)
    {
        try
        {
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;

            if (sqlException.Number == 2601)
            {
                throw new TicketException("Employee email or phone already exists", 501);
            }
            else if(sqlException.Number == 2627)
            {
                throw new TicketException("Employee already exists", 502);
            }
            else
            {
                throw new TicketException("Unable to add employee details", 599);
            }
        }
        catch (Exception e)
        {
            throw new TicketException(e.Message, 599);
        }
    }
 
    public async Task DeleteEmployeeAsync(string empId)
    {
        Employee employeeToDelete =
            await context.Employees
                .Include("Tickets")
                .Include("TicketAssignments")
                .FirstOrDefaultAsync(e => e.EmpId == empId);

        if (employeeToDelete == null)
        {
            throw new TicketException("No such Employee Id", 502);
        }

        if (employeeToDelete.Tickets.Count == 0 &&
            employeeToDelete.TicketAssignments.Count == 0)
        {
            try
            {
                context.Employees.Remove(employeeToDelete);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new TicketException(e.Message, 599);
            }
        }
        else
        {
            throw new TicketException(
                "Cannot delete Employee with existing Tickets or Assignments",
                503
            );
        }
    }

 
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        List<Employee> employees = await context.Employees.ToListAsync();
        return employees;
    }
 
    public async Task<Employee> GetEmployeeAsync(string empId)
    {
        try
        {
            Employee employee =
                await (from e in context.Employees
                       where e.EmpId == empId
                       select e).FirstAsync();
            return employee;
        }
        catch (Exception e)
        {
            throw new TicketException("No employee found", 599);
        }
    }
 
    public async Task UpdateEmployeeAsync(string empId, Employee employee)
    {
        Employee employee2Edit = await GetEmployeeAsync(empId);
        try
        {
            employee2Edit.FirstName = employee.FirstName;
            employee2Edit.LastName = employee.LastName;
            employee2Edit.Email = employee.Email;
            employee2Edit.PhoneNumber = employee.PhoneNumber;
            employee2Edit.Role = employee.Role;
 
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;

            if (sqlException.Number == 2627 || sqlException.Number == 2601)
            {
                throw new TicketException("Employee email or phone already exists", 501);
            }
            else
            {
                throw new TicketException("Unable to update employee details", 599);
            }
        }
        catch (Exception e)
        {
            throw new TicketException(e.Message, 599);
        }
    }

    public async Task<Employee> LoginAsync(string empId, string password)
    {
        try
        {
            return await context.Employees
                .FirstAsync(e => e.EmpId == empId && e.Password == password);
        }
        catch
        {
            throw new TicketException("Invalid Employee Id or Password", 401);
        }
    }

}
 