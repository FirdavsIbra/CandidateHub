using CandidateHub.Domain.Interfaces;
using CandidateHub.Infrastructure.Repositories;

namespace CandidateHub.Infrastructure.Data;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ICandidateRepository _candidateRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _candidateRepository = new CandidateRepository(_context);
    }

    public ICandidateRepository Candidates => _candidateRepository;

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
