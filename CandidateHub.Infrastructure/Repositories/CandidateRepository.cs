using CandidateHub.Domain.Entities;
using CandidateHub.Domain.Interfaces;
using CandidateHub.Infrastructure.Data;

namespace CandidateHub.Infrastructure.Repositories;

public class CandidateRepository: Repository<Candidate>, ICandidateRepository
{
    public CandidateRepository(AppDbContext context) : base(context)
    {
    }
}
