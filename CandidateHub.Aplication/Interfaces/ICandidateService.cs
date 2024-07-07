using CandidateHub.Application.Dtos;

namespace CandidateHub.Application.Interfaces;

public interface ICandidateService
{
    public Task<CandidateDto[]> GetAllAsync();
    public Task<CandidateDto> GetByIdAsync(int id);
    public Task AddOrUpdateAsync(CandidateDto candidateDto);
    public Task DeleteAsync(int id);
}
