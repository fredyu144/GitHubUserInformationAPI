using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GitHubUsersController : ControllerBase
{
    private readonly IGitHubUserManager _gitHubUserManager;

    public GitHubUsersController(IGitHubUserManager gitHubUserManager)
    {
        _gitHubUserManager = gitHubUserManager;
    }

    [HttpPost()]
    public async Task<IActionResult> RetrieveUsers([FromBody]List<string> usernames)
    {
        if (!usernames.Any())
        {
            return BadRequest("please provide at least 1 username");
        }
        try
        {
            List<GitHubUserDto>? users = await _gitHubUserManager.GetUsers(usernames);

            return Ok(users.OrderBy(u => u.Name).ToList());
        }
        catch()
        {
            return BadRequest("something went wrong");
        }
    }
}
