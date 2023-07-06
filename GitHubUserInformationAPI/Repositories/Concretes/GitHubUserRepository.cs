using Microsoft.EntityFrameworkCore;

namespace GitHubUserInformationAPI.Repositories
{
    public class GitHubUserRepository : IGitHubUserRepository
    {
        private readonly ApplicationDbContext _db;

        public GitHubUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GitHubUser> GetUser(string username)
        {
            return await _db.GitHubUsers.Where(x => x.UserName == username).FirstAsync();
        }
        public async Task<IEnumerable<GitHubUser>> GetUsers()
        {
            return await _db.GitHubUsers.ToListAsync();
        }
        public async Task<IEnumerable<GitHubUser>> GetUsers(IEnumerable<string> usernames)
        {
            return await _db.GitHubUsers.Where(x => usernames.Contains(x.UserName)).ToListAsync();
        }
        public async Task<IEnumerable<GitHubUser>> SaveUsers(IEnumerable<GitHubUser> users)
        {
            _db.GitHubUsers.AddRange(users);
            await _db.SaveChangesAsync();
            return users;
        }
    }
}
