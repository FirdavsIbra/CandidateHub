namespace CandidateHub.Application.Constants;

public static class CacheKeys
{
    public const string AllCandidates = "AllCandidates";
    public static string CandidateById(int id) => $"Candidate_{id}";
}