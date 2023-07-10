using System.Linq.Expressions;

namespace GitHubUserInformationAPI.MappingProfiles
{
    public static class GitHubUserMappings
    {
        public static readonly Expression<Func<GitHubUser, GitHubUserDto>> GitHubUserToDto = x => new GitHubUserDto
        {
            Name = x.Name,
            Company = x.Company,
            Followers = x.Followers,
            Login = x.Login,
            PublicRepositories = x.PublicRepositories,
            AverageFollowersPerRepository = x.AverageFollowersPerRepository
        };
    }
}
