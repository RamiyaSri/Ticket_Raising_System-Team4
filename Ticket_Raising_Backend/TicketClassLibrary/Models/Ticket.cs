using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketClassLibrary.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Ticket ID must be exactly 5 characters.")]
    public string TicketId { get; set; }

    [ForeignKey(nameof(Employee))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Employee ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Employee ID must be exactly 5 characters.")]
    public string EmpId { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "Subject is required.")]
    [StringLength(100, ErrorMessage = "Subject cannot exceed 100 characters.")]
    public string Subject { get; set; }

    [Column(TypeName = "varchar(500)")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; }

    [Column(TypeName = "varchar(20)")]
    [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
    public string Status { get; set; }

    [ForeignKey(nameof(TicketType))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket Type ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Ticket Type ID must be exactly 5 characters.")]
    public string TicketTypeId { get; set; }

    public DateTime? CreationDate { get; set; }
    public DateTime? ResolutionDate { get; set; }

    public virtual Employee Employee { get; set; }
    public virtual TicketType TicketType { get; set; }

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();
}
