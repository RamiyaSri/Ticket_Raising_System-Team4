using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketType")]
public class TicketType
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket Type ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Ticket Type ID must be exactly 5 characters.")]
    public string TicketTypeId { get; set; }

    [Column(TypeName = "varchar(50)")]
    [Required(ErrorMessage = "Type name is required.")]
    [StringLength(50, ErrorMessage = "Type name cannot exceed 50 characters.")]
    public string TypeName { get; set; }

    [Column(TypeName = "varchar(100)")]
    [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
    public string Description { get; set; }

    [ForeignKey(nameof(TicketPriority))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Priority ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Priority ID must be exactly 5 characters.")]
    public string PriorityId { get; set; }

    public virtual TicketPriority? TicketPriority { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
