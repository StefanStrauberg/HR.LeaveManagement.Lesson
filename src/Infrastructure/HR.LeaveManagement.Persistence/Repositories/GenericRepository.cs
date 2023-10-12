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

    async Task<IReadOnlyList<T>> IGenericRepository<T>.GetAsync(CancellationToken cancellationToken)
        => await Context.Set<T>()
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

    async Task<T?> IGenericRepository<T>.GetByIdAsync(int id, 
                                                      CancellationToken cancellationToken)
        => await Context.Set<T>()
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);

    async Task IGenericRepository<T>.CreateAsync(T entity, 
                                                 CancellationToken cancellationToken)
    {
        await Context.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    async Task IGenericRepository<T>.CreateManyAsync(IReadOnlyList<T> entities, 
                                                     CancellationToken cancellationToken)
    {
        await Context.Set<T>()
            .AddRangeAsync(entities, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    async Task IGenericRepository<T>.UpdateTask(T entity, 
                                                CancellationToken cancellationToken)
    {
        Context.Update(entity);
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    async Task IGenericRepository<T>.DeleteTask(T entity, 
                                                CancellationToken cancellationToken)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    async Task<bool> IGenericRepository<T>.CheckEntityExistsByIdAsync(int id,
                                                                      CancellationToken cancellationToken)
        => await Context.Set<T>()
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .AnyAsync(cancellationToken);
}