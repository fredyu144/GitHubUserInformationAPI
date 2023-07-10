namespace UnitTests.Manager
{
    public class GitHubUserManagerTest
    {
        [Theory]
        [InlineData(new string[] { "existingUser1" }, 1)]
        [InlineData(new string[] { "existingUser1", "existingUser2" }, 2)]
        [InlineData(new string[] { "existingUser1", "existingUser2", "existingUser3" }, 3)]
        [InlineData(new string[] { "existingUser1", "existingUser4" }, 1)]
        public async void Get_Users(string[] expectedUsernames, int expectedResult)
        {
            List<GitHubUser> users = new List<GitHubUser>(){
                new GitHubUser()
                {
                    UserName = "existingUser1",
                    Login = "existingUser1"
                },
                new GitHubUser()
                {
                    UserName = "existingUser2",
                    Login = "existingUser2"
                },
                new GitHubUser()
                {
                    UserName = "existingUser3",
                    Login = "existingUser3"
                }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"ApplicationDatabase{Guid.NewGuid()}")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.GitHubUsers.AddRange(users);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                GitHubUserRepository repository = new GitHubUserRepository(context);
                var getUserResult = repository.GetUsers(expectedUsernames);
                int result = getUserResult.Count();
                Assert.Equal(result, expectedResult);
            }
        }
    }
}