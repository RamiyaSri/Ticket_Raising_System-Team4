using TicketClassLibrary.Models;
using TicketClassLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrioritiesController : ControllerBase
    {
        ITicketPriorityRepository priorityRepo;

        public PrioritiesController(ITicketPriorityRepository repository)
        {
            priorityRepo = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            List<TicketPriority> priorities =
                await priorityRepo.GetAllPrioritiesAsync();
            return Ok(priorities);
        }

        [HttpGet("{priorityId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string priorityId)
        {
            try
            {
                TicketPriority priority =
                    await priorityRepo.GetPriorityAsync(priorityId);
                return Ok(priority);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(TicketPriority priority)
        {
            try
            {
                await priorityRepo.AddPriorityAsync(priority);
                return Created(
                    $"api/priorities/{priority.PriorityId}",
                    priority
                );
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{priorityId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(
            string priorityId,
            TicketPriority priority)
        {
            try
            {
                await priorityRepo.UpdatePriorityAsync(priorityId, priority);
                return Ok(priority);
            }
            catch (TicketException ex)
            {
                if (ex.Message == "No such priority ID")
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{priorityId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string priorityId)
        {
            try
            {
                await priorityRepo.DeletePriorityAsync(priorityId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("level/{priorityLevel}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByLevel(string priorityLevel)
        {
            try
            {
                TicketPriority priority =
                    await priorityRepo.GetPriorityByLevelAsync(priorityLevel);
                return Ok(priority);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
