using Shared;

namespace Domain.Interfaces;

public interface IQueryRepository<T> where T : class
{
    Task<Optional<T>> GetById(Guid Id);
    
    Task<Optional<IList<T>>> GetAll(int page=1, int take=50);
}