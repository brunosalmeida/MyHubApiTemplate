using Domain.Interfaces;

namespace Data.Repository;

public abstract class CommandRepository<T> : ICommandRepository<T> where T : class
{
    public Task<bool> Create(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(T entity)
    {
        throw new NotImplementedException();
    }
}