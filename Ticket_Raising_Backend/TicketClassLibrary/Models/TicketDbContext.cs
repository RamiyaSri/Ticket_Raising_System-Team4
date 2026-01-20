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

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }
    public DbSet<TicketPriority> TicketPriorities { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }
    public DbSet<TicketAssignment> TicketAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "data source=localhost\\SQLEXPRESS; database=TicketTeam4DB; integrated security=true; Trust Server Certificate=true"
            );
        }
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Employee)
        .WithMany(e => e.Tickets)
        .HasForeignKey(t => t.EmpId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.TicketType)
        .WithMany(tt => tt.Tickets)
        .HasForeignKey(t => t.TicketTypeId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<TicketComment>()
        .HasOne(tc => tc.Ticket)
        .WithMany(t => t.TicketComments)
        .HasForeignKey(tc => tc.TicketId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<TicketComment>()
        .HasOne(tc => tc.Employee)
        .WithMany()
        .HasForeignKey(tc => tc.EmpId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<TicketComment>()
        .HasOne(tc => tc.SupportEmployee)
        .WithMany()
        .HasForeignKey(tc => tc.Support_Emp_Id)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<TicketAssignment>()
        .HasOne(ta => ta.Ticket)
        .WithMany(t => t.TicketAssignments)
        .HasForeignKey(ta => ta.TicketId)
        .OnDelete(DeleteBehavior.Cascade);

    // ✅ FIXED mapping
    modelBuilder.Entity<TicketAssignment>()
        .HasOne(ta => ta.Employee)
        .WithMany(e => e.TicketAssignments)
        .HasForeignKey(ta => ta.Support_Emp_Id)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<TicketType>()
        .HasOne(tt => tt.TicketPriority)
        .WithMany(tp => tp.TicketTypes)
        .HasForeignKey(tt => tt.PriorityId)
        .OnDelete(DeleteBehavior.Restrict);
}
}