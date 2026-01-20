using TicketClassLibrary.Models;

namespace TicketClassLibrary.Repos;

public interface IEmployeeRepository
{
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(string empId, Employee employee);
    Task DeleteEmployeeAsync(string empId);
    Task<Employee> GetEmployeeAsync(string empId);
    Task<List<Employee>> GetAllEmployeesAsync();
 

}
