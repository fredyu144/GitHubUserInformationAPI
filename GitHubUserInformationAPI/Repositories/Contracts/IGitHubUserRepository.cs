namespace GitHubUserInformationAPI.Repositories
{
    public interface IGitHubUserRepository
    {
        Task<GitHubUser> GetUser(string username);
        IQueryable<GitHubUser> GetUsers();
        IQueryable<GitHubUser> GetUsers(IEnumerable<string> usernames);
        Task<IEnumerable<GitHubUser>> SaveUsers(IEnumerable<GitHubUser> users);
    }
}
