using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketAssignment")]
public class TicketAssignment
{
    [Key]
    [Column(TypeName = "char(3)")]
    [Required(ErrorMessage = "Assignment ID is required")]
    [RegularExpression(@"^A\d{2}$", ErrorMessage = "Assignment ID must be in format A01")]
    public string AssignmentId { get; set; }

    [ForeignKey(nameof(Ticket))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket ID is required")]
    public string TicketId { get; set; }

    [ForeignKey(nameof(Employee))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Support Employee ID is required")]
    [RegularExpression(@"^E\d{4}$", ErrorMessage = "Support Employee ID must be in format E0001")]
    public string Support_Emp_Id { get; set; }

    [Required(ErrorMessage = "Assignment Date is required")]
    public DateTime AssignmentDate { get; set; }

    // Navigation Properties
    public virtual Ticket Ticket { get; set; }
    public virtual Employee Employee { get; set; }
}
