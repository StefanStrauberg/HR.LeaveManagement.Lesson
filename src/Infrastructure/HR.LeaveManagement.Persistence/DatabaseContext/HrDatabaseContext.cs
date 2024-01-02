using System.Reflection;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

internal sealed class HrDatabaseContext(DbContextOptions<HrDatabaseContext> options) : DbContext(options)
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(x => x.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.DateUpdated = DateTime.Now;
            if (entry.State == EntityState.Added)
                entry.Entity.DateCreated = DateTime.Now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}