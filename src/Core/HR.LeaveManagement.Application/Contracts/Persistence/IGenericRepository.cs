using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAsync(CancellationToken cancellationToken);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateTask(T entity, CancellationToken cancellationToken);
    Task<T> DeleteTask(T entity, CancellationToken cancellationToken);
}