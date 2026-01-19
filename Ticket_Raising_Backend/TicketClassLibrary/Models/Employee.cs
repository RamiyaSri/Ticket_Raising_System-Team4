using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketClassLibrary.Models;

[Table("Employee")]
public class Employee
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Employee ID is required")]
    [RegularExpression(@"^E\d{4}$", ErrorMessage = "Employee ID must be in format E0001")]
    public string EmpId { get; set; }

    [Column(TypeName = "varchar(50)")]
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
    [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "First Name can contain only letters")]
    public string FirstName { get; set; }

    [Column(TypeName = "varchar(50)")]
    [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
    [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Last Name can contain only letters")]
    public string? LastName { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Role is required")]
    [RegularExpression(@"^(ADMIN|USER|SUPPORT)$", ErrorMessage = "Role must be ADMIN, USER, or SUPPORT")]
    public string Role { get; set; }

    [Column(TypeName = "char(10)")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phone number must be a valid 10-digit Indian number")]
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
