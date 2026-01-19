using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketPriority")]
public class TicketPriority
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Priority ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Priority ID must be exactly 5 characters.")]
    public string PriorityId { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Priority level is required.")]
    [StringLength(20, ErrorMessage = "Priority level cannot exceed 20 characters.")]
    public string PriorityLevel { get; set; }

    [Column(TypeName = "varchar(100)")]
    [StringLength(100, ErrorMessage = "Priority description cannot exceed 100 characters.")]
    public string PriorityDescription { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Response time must be greater than 0.")]
    public int ResponseTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Resolution time must be greater than 0.")]
    public int ResolutionTime { get; set; }

    public virtual ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
}
