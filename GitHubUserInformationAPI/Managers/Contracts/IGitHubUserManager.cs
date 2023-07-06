namespace GitHubUserInformationAPI.Managers
{
    public interface IGitHubUserManager
    {
        Task<List<GitHubUserDto>> GetUsers(IEnumerable<string> username);
    }
}
