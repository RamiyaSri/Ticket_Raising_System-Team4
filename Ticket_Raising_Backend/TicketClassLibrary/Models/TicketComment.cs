using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketComment")]
public class TicketComment
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Comment ID is required")]
    [RegularExpression(@"^C\d{4}$", ErrorMessage = "Comment ID must be in format C0001")]
    public string CommentId { get; set; }

    [ForeignKey(nameof(Ticket))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket ID is required")]
    public string TicketId { get; set; }

    [ForeignKey(nameof(Employee))]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmpId { get; set; }

    [ForeignKey(nameof(TicketAssignment))]
    [Column(TypeName = "char(5)")]
    [RegularExpression(@"^E\d{4}$", ErrorMessage = "Support Employee ID must be in format E0001")]
    public string? Support_Emp_Id { get; set; }

    [Column(TypeName = "varchar(500)")]
    [Required(ErrorMessage = "Comment text is required")]
    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
    public string CommentText { get; set; }

    [Required(ErrorMessage = "Comment date is required")]
    public DateTime CommentDate { get; set; }

    public virtual Ticket Ticket { get; set; }
    public virtual Employee Employee { get; set; }
}
