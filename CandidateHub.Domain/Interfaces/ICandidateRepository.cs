using CandidateHub.Domain.Entities;

namespace CandidateHub.Domain.Interfaces;

public interface ICandidateRepository: IRepository<Candidate>
{
    public Task<Candidate?> GetByEmailAsync(string email);
}