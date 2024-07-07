namespace CandidateHub.Domain.Interfaces;

public interface IUnitOfWork: IDisposable
{
    public ICandidateRepository Candidates { get; }
    public Task<int> CommitAsync();
}
