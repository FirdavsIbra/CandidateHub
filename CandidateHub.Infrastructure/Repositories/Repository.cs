using CandidateHub.Domain.Interfaces;
using CandidateHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task<T[]> GetAllAsync()
    {
        return await _dbSet.ToArrayAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}