using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketAssignment")]
public class TicketAssignment
{
    [Key]
    [Column(TypeName = "char(3)")]
    [Required(ErrorMessage = "Assignment ID is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Assignment ID must be exactly 3 characters.")]
    public string AssignmentId { get; set; }

    [ForeignKey(nameof(Ticket))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Ticket ID must be exactly 5 characters.")]
    public string TicketId { get; set; }

    [ForeignKey(nameof(Employee))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Support Employee ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Support Employee ID must be exactly 5 characters.")]
    public string Support_Emp_Id { get; set; }

    [Required(ErrorMessage = "Assignment date is required.")]
    public DateTime AssignmentDate { get; set; }

    public virtual Ticket Ticket { get; set; }
    public virtual Employee Employee { get; set; }

    
}
