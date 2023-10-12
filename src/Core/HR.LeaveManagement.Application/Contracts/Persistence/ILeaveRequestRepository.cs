using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id, 
                                                        CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetailsByUserIdAsync(string userId, 
                                                                               CancellationToken cancellationToken);
}