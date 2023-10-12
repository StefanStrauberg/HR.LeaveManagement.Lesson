using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

internal class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    async Task<LeaveAllocation?> ILeaveAllocationRepository.GetLeaveAllocationWithDetailsAsync(int id,
                                                                                               CancellationToken cancellationToken)
        => await Context.LeaveAllocations
            .AsNoTracking()
            .Include(x => x.LeaveType)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    async Task<IReadOnlyList<LeaveAllocation>> ILeaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync(CancellationToken cancellationToken)
        => await Context.LeaveAllocations
            .AsNoTracking()
            .Include(x => x.LeaveType)
            .ToListAsync(cancellationToken);

    async Task<IReadOnlyList<LeaveAllocation>> ILeaveAllocationRepository.GetLeaveAllocationsWithDetailsByUserId(string userId, 
                                                                                                                 CancellationToken cancellationToken)
        => await Context.LeaveAllocations
                        .AsNoTracking()
                        .Include(x => x.LeaveType)
                        .Where(x => x.EmployeeId == userId)
                        .ToListAsync(cancellationToken);


    async Task<bool> ILeaveAllocationRepository.AllocationExistsAsync(string userId,
                                                                      int leaveTypeId,
                                                                      int period,
                                                                      CancellationToken cancellationToken)
        => await Context.LeaveAllocations
            .AsNoTracking()
            .AnyAsync(x => x.EmployeeId == userId && 
                      x.LeaveTypeId == leaveTypeId && 
                      x.Period == period, 
                      cancellationToken);

    async Task<LeaveAllocation?> ILeaveAllocationRepository.GetUserAllocationAsync(string userId,
                                                                                  int leaveTypeId,
                                                                                  CancellationToken cancellationToken)
        => await Context.LeaveAllocations
            .AsNoTracking()
            .Where(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId)
            .FirstOrDefaultAsync(cancellationToken);
}