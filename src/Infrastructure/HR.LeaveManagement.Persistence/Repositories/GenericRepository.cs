using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly HrDatabaseContext Context;

    public GenericRepository(HrDatabaseContext context)
        => Context = context;

    public async Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken)
        => await Context.Set<T>()
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<T>()
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await Context.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTask(T entity, CancellationToken cancellationToken)
    {
        Context.Update(entity);
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTask(T entity, CancellationToken cancellationToken)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}