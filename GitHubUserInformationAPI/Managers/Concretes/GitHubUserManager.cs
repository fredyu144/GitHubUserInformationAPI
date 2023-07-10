using Microsoft.EntityFrameworkCore;

namespace GitHubUserInformationAPI.Managers
{
    public class GitHubUserManager : IGitHubUserManager
    {
        private readonly IGitHubUserRepository _gitHubUserRepository;
        private readonly IGitHubService _gitHubUserService;

        public GitHubUserManager(
            IGitHubUserRepository gitHubUserRepository,
            IGitHubService gitHubUserService
            )
        {
            _gitHubUserRepository = gitHubUserRepository;
            _gitHubUserService = gitHubUserService;
        }

        public async Task<List<GitHubUserDto>> GetUsers(IEnumerable<string> usernames)
        {
            if (!usernames.Any())
            {
                return new List<GitHubUserDto>();
            }

            usernames = usernames.Select(x => x.ToLowerInvariant()).Distinct().ToList();
            IQueryable<GitHubUser>? existingUsers = _gitHubUserRepository.GetUsers(usernames);
            List<GitHubUserDto> user = existingUsers.Select(GitHubUserMappings.GitHubUserToDto).ToList();

            List<GitHubUser> gitHubUsersToAdd = new List<GitHubUser>();
            foreach (string? newUser in usernames.Where(x => !existingUsers.Select(x => x.Login).Contains(x)))
            {
                GitHubUserDto? newGitHubUserData = await _gitHubUserService.GetUser(newUser);
                if (newGitHubUserData is not null)
                {
                    int averageFollowersPerRepository = 0;
                    if (newGitHubUserData.Followers != 0 && newGitHubUserData.PublicRepositories != 0) {
                        averageFollowersPerRepository = newGitHubUserData.Followers / newGitHubUserData.PublicRepositories;
                    }
                    gitHubUsersToAdd.Add(new GitHubUser
                    {
                        UserName = newGitHubUserData.Login.ToLowerInvariant(),
                        Login = newGitHubUserData.Login,
                        Name = newGitHubUserData.Name,
                        Company = newGitHubUserData.Company,
                        Followers = newGitHubUserData.Followers,
                        PublicRepositories = newGitHubUserData.PublicRepositories,
                        AverageFollowersPerRepository = averageFollowersPerRepository
                    });
                    user.Add(newGitHubUserData);
                }
            }

            if (gitHubUsersToAdd.Count > 0)
            {
                await _gitHubUserRepository.SaveUsers(gitHubUsersToAdd);
            }

            return user;
        }
    }
}
