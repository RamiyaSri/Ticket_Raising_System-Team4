using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketClassLibrary.Models;

[Table("Employee")]
public class Employee
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "Employee ID is required.")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Employee ID must be exactly 5 characters.")]
    public string EmpId { get; set; }

    [Column(TypeName = "varchar(50)")]
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string FirstName { get; set; }

    [Column(TypeName = "varchar(50)")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string LastName { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string Email { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Role is required.")]
    [RegularExpression("Admin|User", ErrorMessage = "Role must be either 'Admin' or 'User'.")]
    public string Role { get; set; }

    [Column(TypeName = "char(10)")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits.")]
    public string PhoneNumber { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
