using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketPriority")]
public class TicketPriority
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Priority ID is required")]
    [RegularExpression(@"^P\d{4}$", ErrorMessage = "Priority ID must be in format P0001")]
    public string PriorityId { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Priority level is required")]
    [StringLength(20, ErrorMessage = "Priority level cannot exceed 20 characters")]
    public string PriorityLevel { get; set; }

    [Column(TypeName = "varchar(100)")]
    [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string PriorityDescription { get; set; }

    [Required(ErrorMessage = "Response time is required")]
    [Range(1, 1000, ErrorMessage = "Response time must be greater than 0")]
    public int ResponseTime { get; set; }

    [Required(ErrorMessage = "Resolution time is required")]
    [Range(1, 5000, ErrorMessage = "Resolution time must be greater than 0")]
    public int ResolutionTime { get; set; }

    public virtual ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
}
