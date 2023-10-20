using System.Collections;
using Core;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext context;
    private Hashtable repositories;

    public UnitOfWork(StoreContext context)
    {
        this.context = context;
    }

    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (repositories == null) repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!repositories.ContainsKey(type))
        {
            var repoType = typeof(GenericRepository<>);
            var repostoryInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), context);

            repositories.Add(type, repostoryInstance);
        }

        return (IGenericRepository<TEntity>)repositories[type];
    }
}
