using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    internal class LeaveTypeRepository(HrDatabaseContext context) : GenericRepository<LeaveType>(context), ILeaveTypeRepository
    {
        async Task<bool> ILeaveTypeRepository.IsLeaveTypeUnique(string name, 
                                                                CancellationToken cancellationToken)
            => await Context.LeaveTypes
                            .AsNoTracking()
                            .Where(x => x.Name == name)
                            .AnyAsync(cancellationToken);
    }
}
