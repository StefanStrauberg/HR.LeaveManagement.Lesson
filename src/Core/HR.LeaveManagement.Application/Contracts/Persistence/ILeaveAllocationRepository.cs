using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id, 
                                                              CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetailsByUserId(string userId, 
                                                                                CancellationToken cancellationToken);
    Task<bool> AllocationExistsAsync(string userId, 
                                     int leaveTypeId, 
                                     int period, 
                                     CancellationToken cancellationToken);
    Task<LeaveAllocation?> GetUserAllocationAsync(string userId, 
                                                 int leaveTypeId, 
                                                 CancellationToken cancellationToken);
}