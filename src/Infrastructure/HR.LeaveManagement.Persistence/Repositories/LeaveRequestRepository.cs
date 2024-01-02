using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

internal class LeaveRequestRepository(HrDatabaseContext context) : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    async Task<LeaveRequest?> ILeaveRequestRepository.GetLeaveRequestWithDetailsAsync(int id, 
                                                                                      CancellationToken cancellationToken)
        => await Context.LeaveRequests
                        .AsNoTracking()
                        .Include(x => x.LeaveType)
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);

    async Task<IReadOnlyList<LeaveRequest>> ILeaveRequestRepository.GetLeaveRequestsWithDetailsAsync(CancellationToken cancellationToken)
        => await Context.LeaveRequests
                        .AsNoTracking()
                        .Include(x => x.LeaveType)
                        .ToListAsync(cancellationToken);

    async Task<IReadOnlyList<LeaveRequest>> ILeaveRequestRepository.GetLeaveRequestsWithDetailsByUserIdAsync(string userId, 
                                                                                                             CancellationToken cancellationToken)
        => await Context.LeaveRequests
                        .AsNoTracking()
                        .Include(x => x.LeaveType)
                        .Where(x => x.RequestingEmployeeId == userId)
                        .ToListAsync(cancellationToken);
}