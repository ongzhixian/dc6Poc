using Dn6Poc.DocuMgmtPortal.Api;
using Dn6Poc.DocuMgmtPortal.Api.Requests;
using System.IdentityModel.Tokens.Jwt;

namespace Dn6Poc.DocuMgmtPortal.Services;

public class LoginService
{
    private readonly HttpClient _httpClient;

    private const string _configurationKey = "Api:CommonApi:ServerUrl";

    public LoginService(IConfiguration configuration, HttpClient httpClient)
    {
        string serverUrl = configuration.GetValue<string>(_configurationKey) ?? throw new Exception($"Invalid configuration key: [{_configurationKey}]");

        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri($"{serverUrl}/api/Authentication/");
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        LoginRequest postData = new LoginRequest
        {
            Username = "yayay",
            Password = "yayayPassword"
        };

        //HTTP POST
        var result = await _httpClient.PostAsJsonAsync<LoginRequest>("login", postData);

        if (result.IsSuccessStatusCode)
        {
            JwtSecurityTokenHandler c = new JwtSecurityTokenHandler();

            JwtResponse? res = await result.Content.ReadFromJsonAsync<JwtResponse>();

            if ((res != null) && c.CanReadToken(res.Token))
            {
                // JwtSecurityToken token = c.ReadJwtToken(res.Token);

                return res.Token;
            }
        }

        return String.Empty;

    }

    //public async Task<IEnumerable<GitHubBranch>?> GetAspNetCoreDocsBranchesAsync() =>
    //    await _httpClient.GetFromJsonAsync<IEnumerable<GitHubBranch>>(
    //        "repos/dotnet/AspNetCore.Docs/branches");
}