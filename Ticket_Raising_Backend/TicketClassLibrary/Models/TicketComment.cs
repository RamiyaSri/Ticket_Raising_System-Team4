using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("TicketComment")]
public class TicketComment
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Comment ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Comment ID must be exactly 5 characters.")]
    public string CommentId { get; set; }

    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Ticket ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Ticket ID must be exactly 5 characters.")]
    public string TicketId { get; set; }

    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Employee ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Employee ID must be exactly 5 characters.")]
    public string EmpId { get; set; }

    
    [Column("Support_Emp_Id", TypeName = "char(5)")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Support Employee ID must be exactly 5 characters.")]
    public string? Support_Emp_Id { get; set; }

    [Column(TypeName = "varchar(500)")]
    [Required(ErrorMessage = "Comment text is required.")]
    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
    public string CommentText { get; set; }

    [Required(ErrorMessage = "Comment date is required.")]
    public DateTime CommentDate { get; set; }



    [ForeignKey(nameof(TicketId))]
    public virtual Ticket Ticket { get; set; }

    [ForeignKey(nameof(EmpId))]
    public virtual Employee Employee { get; set; }


    [ForeignKey(nameof(Support_Emp_Id))]
    public virtual Employee SupportEmployee { get; set; }
}
