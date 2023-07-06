namespace GitHubUserInformationAPI.Model
{
    public class GitHubUserDto
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Company { get; set; }
        public int Followers { get; set; }
        public int PublicRepositories { get; set; }
        public double AverageFollowersPerRepository { get; set; }
    }
}
