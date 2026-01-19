using Microsoft.EntityFrameworkCore;

namespace TicketClassLibrary.Models;

public class TicketDbContext : DbContext
{
    public TicketDbContext()
    {
        
    }

    public TicketDbContext(DbContextOptions<TicketDbContext> options)
        : base(options)
    {
        
    }

    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<TicketType> TicketTypes { get; set; }
    public virtual DbSet<TicketPriority> TicketPriorities { get; set; }
    public virtual DbSet<TicketComment> TicketComments { get; set; }
    public virtual DbSet<TicketAssignment> TicketAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "data source=localhost\\SQLEXPRESS; database=TicketDB; integrated security=true; Trust Server Certificate=true"
        );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<TicketComment>()
        .HasOne(tc => tc.Ticket)
        .WithMany(t => t.TicketComments)
        .HasForeignKey(tc => tc.TicketId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<TicketComment>()
        .HasOne(tc => tc.Employee)
        .WithMany()
        .HasForeignKey(tc => tc.EmpId)
        .OnDelete(DeleteBehavior.Cascade);
}

}
