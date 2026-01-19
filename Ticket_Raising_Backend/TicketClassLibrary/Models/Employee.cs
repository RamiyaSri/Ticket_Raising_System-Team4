using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 
namespace Helpdesk.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
 
        [Required]
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
 
        [Required]
        public string Email { get; set; }
 
        [Required]
        public string Password { get; set; }
 
        public string Role { get; set; }   
 
        public string PhoneNumber { get; set; }
 
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
        public ICollection<TicketAssignment> TicketAssignments { get; set; }
    }
}