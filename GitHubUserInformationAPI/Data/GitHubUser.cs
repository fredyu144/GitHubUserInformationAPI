using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitHubUserInformationAPI.Model
{
    public class GitHubUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public int Followers { get; set; }
        public int PublicRepositories { get; set; }
        public double AverageFollowersPerRepository { get; set; }
        public DateTime AccountCreateDate { get; set; }
    }
}
