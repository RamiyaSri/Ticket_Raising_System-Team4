using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketClassLibrary.Models;
using TicketClassLibrary.Repos;

namespace TicketWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TicketAssignmentController : ControllerBase
    {
        ITicketAssignmentRepository assignmentRepo;
        public TicketAssignmentController(ITicketAssignmentRepository repository)
        {
            assignmentRepo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<TicketAssignment> assignAll = await assignmentRepo.GetAllAssignmentsAsync();
            return Ok(assignAll);
        }
        [HttpGet("{assignmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string assignmentId)
        {
            try
            {
            TicketAssignment assign2one = await assignmentRepo.GetAssignmentAsync(assignmentId);
            return Ok(assign2one);
            }
            
            catch(TicketException ex)
            {
                return NotFound(ex.Message);               
            }
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(TicketAssignment assignment)
        {
            try
            {
                await assignmentRepo.AddAssignmentAsync(assignment);
                return Created(
                    $"api/assignments/{assignment.AssignmentId}",
                    assignment
                    );
            }
            catch(TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{assignmentId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(TicketAssignment assignment,string assignmentId)
        {
            try
            {
                await assignmentRepo.UpdateAssignmentAsync(assignmentId,assignment);
                return Ok(assignment);

            }
            catch(TicketException ex)
            {
                if(ex.Message == "No such assignment ID")
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
                    
            }
        }
        [HttpDelete("{assignmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string assignmentId){
            try
            {
                await assignmentRepo.DeleteAssignmentAsync(assignmentId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
        [HttpGet("/ByTicketId/{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByTicketId(string ticketId)
        {
            try
            {
                List<TicketAssignment> assignByTicketId = await 
                    assignmentRepo.GetAssignmentsByTicketAsync(ticketId);
                return Ok(assignByTicketId);
            }
            catch(TicketException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet("/GetByEmpId/{supportEmpId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByCustomerId(string supportEmpId)
        {
            try
            {
                List<TicketAssignment> assignByCustomerId = await
                    assignmentRepo.GetAssignmentsBySupportEmployeeAsync(supportEmpId);
                return Ok(assignByCustomerId);   
            }
            catch(TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
