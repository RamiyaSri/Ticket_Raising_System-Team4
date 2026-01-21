using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketClassLibrary.Models;
using TicketClassLibrary.Repos;

namespace TicketWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketCommentsController : ControllerBase
    {
        ITicketCommentRepository ticketCommentRepository;
        public TicketCommentsController(ITicketCommentRepository ticketCommentRepo)
        {
            ticketCommentRepository = ticketCommentRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<TicketComment> ticketComments = await ticketCommentRepository.GetAllCommentsAsync();
            return Ok(ticketComments);
        }

        [HttpGet("/GetComments/ByEmpId/{empId}")]        
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByEmpId(string empId)
        {
            try
            {
                List<TicketComment> ticketComments = await ticketCommentRepository.GetCommentsByEmployeeAsync(empId);
                return Ok(ticketComments);
            }
            catch(TicketException e)
            {
                return NotFound(e.Message);
            }
        }   

        [HttpGet("/GetComments/BySupportEmpId/{SupportEmpId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetBySupEmpId(string SupportEmpId)
        {
            try
            {
                List<TicketComment> ticketComments = await ticketCommentRepository.GetCommentsBySupportEmployeeAsync(SupportEmpId);
                return Ok(ticketComments);
            }
            catch(TicketException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("/GetComments/ByTicketId/{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByTicket(string ticketId)
        {
            try
            {
                List<TicketComment> ticketComments = await ticketCommentRepository.GetCommentsByTicketAsync(ticketId);
                return Ok(ticketComments);
            }
            catch(TicketException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string commentId)
        {
            try
            {
                TicketComment ticketComment = await ticketCommentRepository.GetCommentAsync(commentId);
                return Ok(ticketComment);
            }
            catch(TicketException e)
            {
                return NotFound(e.Message);
            }
            
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(TicketComment ticketComment)
        {
            try
            {
                await ticketCommentRepository.AddCommentAsync(ticketComment);
                return Created($"api/ticketComment/{ticketComment.CommentId}",ticketComment);
            }
            catch(TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{commentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Update(string commentId,TicketComment ticketComment)
        {
            try
            {
                await ticketCommentRepository.UpdateCommentAsync(commentId,ticketComment);
                return Ok(ticketComment);
            }
            catch(TicketException e)
            {
                if (e.Message == "No Comment Found")
                    return NotFound(e.Message);
                else 
                    return BadRequest(e.Message);
            }
        }

        [HttpDelete("{commentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Delete(String commentId)
        {
            try
            {
                await ticketCommentRepository.DeleteCommentAsync(commentId);
                return Ok();
            }
            catch(TicketException e)
            {
                if (e.Message == "No Comment Found")
                    return NotFound(e.Message);
                else
                    return BadRequest(e.Message);
            }
        }
    }
}
