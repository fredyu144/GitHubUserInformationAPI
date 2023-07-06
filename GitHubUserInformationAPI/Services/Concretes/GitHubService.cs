using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GitHubUserInformationAPI.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GitHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetValue<string>("GitHubApi:ApiKey"));
            _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", _configuration.GetValue<string>("GitHubApi:ApiVersion"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET/6.0 GitHubUserInformationAPI/1.0");
        }

        public async Task<GitHubUserDto> GetUser(string username)
        {
            var response = await _httpClient.GetAsync(string.Format("https://api.github.com/users/{0}", username));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GitHubUserDto>();
            }
            else
            {
                return null;
            }
        }
    }

}
