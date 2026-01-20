using TicketClassLibrary.Models;
using TicketClassLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        ITicketRepository ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            List<Ticket> tickets = await ticketRepository.GetAllTicketsAsync();
            return Ok(tickets);
        }


        [HttpGet("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string ticketId)
        {
            try
            {
                Ticket ticket = await ticketRepository.GetTicketAsync(ticketId);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(Ticket ticket)
        {
            try
            {
                await ticketRepository.AddTicketAsync(ticket);
                return Created($"api/Ticket/{ticket.TicketId}", ticket);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string ticketId, Ticket ticket)
        {
            try
            {
                await ticketRepository.UpdateTicketAsync(ticketId, ticket);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                if (ex.ErrorCode == 502)
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string ticketId)
        {
            try
            {
                await ticketRepository.DeleteTicketAsync(ticketId);
                return Ok();
            }
            catch (TicketException ex)
            {
                if (ex.ErrorCode == 502)
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }

   
        [HttpGet("ByEmployee/{empId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetByEmployee(string empId)
        {
            List<Ticket> tickets = await ticketRepository.GetTicketsByEmployeeAsync(empId);
            return Ok(tickets);
        }

        [HttpGet("ByStatus/{status}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetByStatus(string status)
        {
            List<Ticket> tickets = await ticketRepository.GetTicketsByStatusAsync(status);
            return Ok(tickets);
        }

 
        [HttpGet("ByType/{ticketTypeId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetByType(string ticketTypeId)
        {
            List<Ticket> tickets = await ticketRepository.GetTicketsByTypeAsync(ticketTypeId);
            return Ok(tickets);
        }
    }
}
