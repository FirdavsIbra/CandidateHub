using CandidateHub.Domain.Entities;
using CandidateHub.Domain.Interfaces;
using CandidateHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Infrastructure.Repositories;

public class CandidateRepository: Repository<Candidate>, ICandidateRepository
{
    public CandidateRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Candidate?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}