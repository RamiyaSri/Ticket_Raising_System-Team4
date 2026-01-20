using TicketClassLibrary.Models;
using TicketClassLibrary.Repos;
using Microsoft.AspNetCore.Mvc;

namespace TicketWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeRepository employeeRepo;

        public EmployeesController(IEmployeeRepository emprepository)
        {
            employeeRepo = emprepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            List<Employee> employees = await employeeRepo.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{empId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string empId)
        {
            try
            {
                Employee employee = await employeeRepo.GetEmployeeAsync(empId);
                return Ok(employee);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(Employee employee)
        {
            try
            {
                await employeeRepo.AddEmployeeAsync(employee);
                return Created(
                    $"api/employees/{employee.EmpId}",
                    employee
                );
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{empId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(
            string empId,
            Employee employee)
        {
            try
            {
                await employeeRepo.UpdateEmployeeAsync(empId, employee);
                return Ok(employee);
            }
            catch (TicketException ex)
            {
                if (ex.Message == "No employee found")
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{empId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string empId)
        {
            try
            {
                await employeeRepo.DeleteEmployeeAsync(empId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
