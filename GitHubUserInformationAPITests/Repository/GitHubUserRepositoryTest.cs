
namespace UnitTests
{
    public class GitHubUserRepositoryTest
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
        [Fact]
        public async void Get_Users_No_Params()
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
                var getUserResult = repository.GetUsers();
                int result = getUserResult.Count();
                int expectedResult = 3;
                Assert.Equal(result, expectedResult);
            }
        }
        
        [Fact]
        public async void User_Existing()
        {
            string username = "existingUser";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test_db")
                .Options;

            GitHubUser user = new GitHubUser()
            {
                UserName = "existingUser",
                Login = "existingUser"
            };


            using (var context = new ApplicationDbContext(options))
            {
                context.GitHubUsers.Add(user);
                context.SaveChanges();

                GitHubUserRepository repository = new GitHubUserRepository(context);
                var getUserResult = await repository.GetUser(username);
                Assert.NotNull(getUserResult);
                Assert.Equal(getUserResult.UserName.ToLowerInvariant(), username.ToLowerInvariant());
            }
        }

        [Fact]
        public async void UserNotExisting()
        {
            string username = "notExistingUser";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase("test_db")
                 .Options;

            GitHubUser user = new GitHubUser()
            {
                UserName = "existingUser",
                Login = "existingUser"
            };


            using (var context = new ApplicationDbContext(options))
            {
                context.GitHubUsers.Add(user);
                context.SaveChanges();

                GitHubUserRepository repository = new GitHubUserRepository(context);
                var getUserResult = await repository.GetUser(username);
                Assert.Null(getUserResult);
            }
        }
        [Fact]
        public async void Save_Users()
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
                GitHubUserRepository repository = new GitHubUserRepository(context);
                await repository.SaveUsers(users);
                int result = context.GitHubUsers.Count();
                int expectedResult = 3;
                Assert.Equal(result, expectedResult);
            }
        }
    }
}