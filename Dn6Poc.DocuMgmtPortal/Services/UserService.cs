using Dn6Poc.DocuMgmtPortal.Api;
using Dn6Poc.DocuMgmtPortal.Api.Requests;
using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace Dn6Poc.DocuMgmtPortal.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    private const string _configurationKey = "Api:CommonApi:ServerUrl";

    private readonly HttpContext _httpContext;

    public UserService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        string serverUrl = configuration.GetValue<string>(_configurationKey) ?? throw new Exception($"Invalid configuration key: [{_configurationKey}]");

        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri($"{serverUrl}");

        _httpContext = httpContextAccessor.HttpContext ?? throw new Exception("No HttpContext");

        string? jwt = _httpContext.Session.GetString("JWT");

        if (jwt != null)
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

        //HttpContext.Session.SetString("JWT", res.Token);
    }

    public async Task AddUserAsync(AddUserViewModel model)
    {
        var result = await _httpClient.PostAsJsonAsync<AddUserRequest>("/api/User", new AddUserRequest(model));

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Oh no! What now?");
        }
    }

    public async Task<IEnumerable<User>> GetUserListAsync(int pageSize, int page)
    {
        string url = $"/api/User?page={page}&pageSize={pageSize}";

        var result = await _httpClient.GetAsync(url);

        if (result.IsSuccessStatusCode)
        {
            IEnumerable<User>? res = await result.Content.ReadFromJsonAsync<IEnumerable<User>>();

            if (res != null)
                return res;
        }

        return new List<User>();
    }

    //internal async Task SuspendUserAsync(string id)
    //{
    //    var updateRequest = new UpdateUserStatusRequest
    //    {
    //        Id = id,
    //        UserStatus = UserStatus.Suspended
    //    };
        
    //    var result = await _httpClient.PutAsJsonAsync<UpdateUserStatusRequest>("/api/User", updateRequest);

    //    if (!result.IsSuccessStatusCode)
    //    {
    //        throw new Exception("Oh no! What now?");
    //    }
    //}

    //internal void UnlockUser(string id)
    //{
        
    //}

    //internal void ActivateUser(string id)
    //{
    //    throw new NotImplementedException();
    //}

    internal async Task UpdateUserStatusAsync(string id, UserStatus userStatus)
    {
        var updateRequest = new UpdateUserStatusRequest
        {
            Id = id,
            UserStatus = userStatus
        };

        var result = await _httpClient.PutAsJsonAsync<UpdateUserStatusRequest>("/api/User", updateRequest);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Oh no! What now?");
        }
    }



    //public async Task<string> LoginAsync(string username, string password)
    //{
    //    LoginRequest postData = new LoginRequest
    //    {
    //        Username = "yayay",
    //        Password = "yayayPassword"
    //    };

    //    //HTTP POST
    //    var result = await _httpClient.PostAsJsonAsync<LoginRequest>("add", postData);

    //    if (result.IsSuccessStatusCode)
    //    {
    //        JwtSecurityTokenHandler c = new JwtSecurityTokenHandler();

    //        var res = await result.Content.ReadFromJsonAsync<JwtResponse>();

    //        JwtSecurityToken token = c.ReadJwtToken(res.Token);
    //        c.CanReadToken(res.Token);

    //        return res.Token;
    //        //HttpContext.Session.SetString("JWT", res.Token);
    //    }

    //    return String.Empty;

    //}

    //public async Task<IEnumerable<GitHubBranch>?> GetAspNetCoreDocsBranchesAsync() =>
    //    await _httpClient.GetFromJsonAsync<IEnumerable<GitHubBranch>>(
    //        "repos/dotnet/AspNetCore.Docs/branches");
}