namespace CandidateHub.Application.Dtos;

public class CandidateDto
{
    /// <summary>
    /// Gets or sets the id of candidate.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of candidate.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of candidate.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email of candidate.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of candidate.
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the preferred contact time of candidate.
    /// </summary>
    public string? PreferredContactTime { get; set; }

    /// <summary>
    /// Gets or sets the LinkedIn profile of candidate.
    /// </summary>
    public string? LinkedInProfile { get; set; }

    /// <summary>
    /// Gets or sets GitHub profile of candidate
    /// </summary>
    public string? GitHubProfile { get; set; }

    /// <summary>
    /// Gets or set comment of candidate;
    /// </summary>
    public string Comments { get; set; }
}