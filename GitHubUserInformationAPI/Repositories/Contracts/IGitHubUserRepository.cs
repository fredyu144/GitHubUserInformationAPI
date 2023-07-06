namespace GitHubUserInformationAPI.Repositories
{
    public interface IGitHubUserRepository
    {
        Task<GitHubUser> GetUser(string username);
        Task<IEnumerable<GitHubUser>> GetUsers();
        Task<IEnumerable<GitHubUser>> GetUsers(IEnumerable<string> usernames);
        Task<IEnumerable<GitHubUser>> SaveUsers(IEnumerable<GitHubUser> users);
    }
}
