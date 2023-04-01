using Domain.Interfaces;
using Shared;

namespace Data.Repository;

public abstract class QueryRepository<T> : IQueryRepository<T> where T : class
{
    public Task<Optional<T>> GetById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<Optional<IList<T>>> GetAll(int page = 1, int take = 50)
    {
        throw new NotImplementedException();
    }
}