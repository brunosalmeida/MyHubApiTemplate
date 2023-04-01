namespace Domain.Interfaces;

public interface ICommandRepository<in T> where T : class
{
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
}