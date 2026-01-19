using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket ID is required")]
    [RegularExpression(@"^T\d{4}$", ErrorMessage = "Ticket ID must be in format T0001")]
    public string TicketId { get; set; }

    [ForeignKey(nameof(Employee))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmpId { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "Subject is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Subject must be between 5 and 100 characters")]
    public string Subject { get; set; }

    [Column(TypeName = "varchar(500)")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Status is required")]
    [RegularExpression(@"^(OPEN|IN_PROGRESS|RESOLVED|CLOSED)$",
        ErrorMessage = "Status must be OPEN, IN_PROGRESS, RESOLVED, or CLOSED")]
    public string Status { get; set; }

    [ForeignKey(nameof(TicketType))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket Type ID is required")]
    public string TicketTypeId { get; set; }

    [Required(ErrorMessage = "Creation Date is required")]
    public DateTime CreationDate { get; set; }

    public DateTime? ResolutionDate { get; set; }

    // Navigation Properties
    public virtual Employee Employee { get; set; }
    public virtual TicketType TicketType { get; set; }

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();
}
