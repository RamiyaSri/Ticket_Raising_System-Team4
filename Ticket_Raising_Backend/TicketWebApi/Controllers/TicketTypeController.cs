using TicketClassLibrary.Models;
using TicketClassLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TicketWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketTypesController : ControllerBase
    {
        ITicketTypeRepository ticketTypeRepository;
 
        public TicketTypesController(ITicketTypeRepository repository)
        {
            ticketTypeRepository = repository;
        }
 
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            List<TicketType> ticketTypeList =
                await ticketTypeRepository.GetAllTicketTypesAsync();
            return Ok(ticketTypeList);
        }
 
        [HttpGet("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string ticketTypeId)
        {
            try
            {
                TicketType ticketType =
                    await ticketTypeRepository.GetTicketTypeAsync(ticketTypeId);
                return Ok(ticketType);
            }
            catch (TicketException exception)
            {
                return NotFound(exception.Message);
            }
        }
 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(TicketType ticketType)
        {
            try
            {
                await ticketTypeRepository.AddTicketTypeAsync(ticketType);
                return Created(
                    $"api/tickettypes/{ticketType.TicketTypeId}",
                    ticketType
                );
            }
            catch (TicketException exception)
            {
                return BadRequest(exception.Message);
            }
        }
 
        [HttpPut("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(
            string ticketTypeId,
            TicketType ticketType)
        {
            try
            {
                await ticketTypeRepository.UpdateTicketTypeAsync(ticketTypeId, ticketType);
                return Ok(ticketType);
            }
            catch (TicketException exception)
            {
                if (exception.Message == "No such Ticket Type Id")
                    return NotFound(exception.Message);
                else
                    return BadRequest(exception.Message);
            }
        }
 
        [HttpDelete("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string ticketTypeId)
        {
            try
            {
                await ticketTypeRepository.DeleteTicketTypeAsync(ticketTypeId);
                return Ok();
            }
            catch (TicketException exception)
            {
                return NotFound(exception.Message);
            }
        }
 
        [HttpGet("priority/{priorityId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByPriority(string priorityId)
        {
            try
            {
                List<TicketType> ticketTypeList =
                    await ticketTypeRepository.GetTicketTypesByPriorityAsync(priorityId);
                return Ok(ticketTypeList);
            }
            catch (TicketException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}