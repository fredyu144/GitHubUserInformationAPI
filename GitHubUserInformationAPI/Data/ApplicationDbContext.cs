using Microsoft.EntityFrameworkCore;

namespace GitHubUserInformationAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GitHubUser> GitHubUsers { get; set; }
    }
}