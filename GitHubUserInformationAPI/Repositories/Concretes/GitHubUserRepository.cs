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
            return await _db.GitHubUsers.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }
        public IQueryable<GitHubUser> GetUsers()
        {
            return _db.GitHubUsers.AsQueryable();
        }
        public IQueryable<GitHubUser> GetUsers(IEnumerable<string> usernames)
        {
            return _db.GitHubUsers.Where(x => usernames.Contains(x.UserName)).AsQueryable();
        }
        public async Task<IEnumerable<GitHubUser>> SaveUsers(IEnumerable<GitHubUser> users)
        {
            _db.GitHubUsers.AddRange(users);
            await _db.SaveChangesAsync();
            return users;
        }
    }
}
