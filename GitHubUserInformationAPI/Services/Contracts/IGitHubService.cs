namespace GitHubUserInformationAPI.Services
{
    public interface IGitHubService
    {
        Task<GitHubUserDto> GetUser(string username);
    }
}
