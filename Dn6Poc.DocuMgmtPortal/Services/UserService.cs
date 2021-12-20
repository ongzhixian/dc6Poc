using Dn6Poc.DocuMgmtPortal.Api;
using Dn6Poc.DocuMgmtPortal.Api.Requests;
using System.IdentityModel.Tokens.Jwt;

namespace Dn6Poc.DocuMgmtPortal.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    private const string _configurationKey = "Api:CommonApi:ServerUrl";

    public UserService(IConfiguration configuration, HttpClient httpClient)
    {
        string serverUrl = configuration.GetValue<string>(_configurationKey) ?? throw new Exception($"Invalid configuration key: [{_configurationKey}]");

        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri($"{serverUrl}/api/User/");
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        LoginRequest postData = new LoginRequest
        {
            Username = "yayay",
            Password = "yayayPassword"
        };

        //HTTP POST
        var result = await _httpClient.PostAsJsonAsync<LoginRequest>("add", postData);

        if (result.IsSuccessStatusCode)
        {
            JwtSecurityTokenHandler c = new JwtSecurityTokenHandler();

            var res = await result.Content.ReadFromJsonAsync<JwtResponse>();

            JwtSecurityToken token = c.ReadJwtToken(res.Token);
            c.CanReadToken(res.Token);

            return res.Token;
            //HttpContext.Session.SetString("JWT", res.Token);
        }

        return String.Empty;

    }

    //public async Task<IEnumerable<GitHubBranch>?> GetAspNetCoreDocsBranchesAsync() =>
    //    await _httpClient.GetFromJsonAsync<IEnumerable<GitHubBranch>>(
    //        "repos/dotnet/AspNetCore.Docs/branches");
}