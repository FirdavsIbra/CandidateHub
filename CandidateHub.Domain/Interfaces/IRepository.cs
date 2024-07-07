namespace CandidateHub.Domain.Interfaces;

public interface IRepository<T> where T: class
{
    public Task<T?> GetByIdAsync(int id);
    public Task<T[]> GetAllAsync();

    public Task AddAsync(T entity);
    public Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);
    void Remove(T entity);
}