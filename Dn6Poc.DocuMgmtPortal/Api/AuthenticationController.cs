// app.MapGet("/login", [AllowAnonymous] async (HttpContext http,ITokenService tokenService,IUserRepositoryService userRepositoryService) => {
//     var userModel = await http.Request.ReadFromJsonAsync<UserModel>();
//     var userDto = userRepositoryService.GetUser(userModel);
//     if (userDto == null)
//     {
//         http.Response.StatusCode = 401;
//         return;
//     }

//     var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], userDto);
//     await http.Response.WriteAsJsonAsync(new { token = token });
//     return;
// });

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

//[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{

    private readonly ILogger<AuthenticationController> _logger;
    private readonly IConfiguration _configuration;


    public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // [HttpPost(Name = "login")]
    // public IEnumerable<LoginCredential> Post()
    // {
    //     // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     // {
    //     //     Date = DateTime.Now.AddDays(index),
    //     //     TemperatureC = Random.Shared.Next(-20, 55),
    //     //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     // })
    //     // .ToArray();
    // }

    [HttpGet(Name = "HelloWorld")]
    public string Get()
    {
        return "Helo API OWRLD";
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        _logger.LogInformation("Yep in login API");
            // var authClaims = new List<Claim>
            //     {
            //         new Claim(ClaimTypes.Name, user.UserName),
            //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //     };

            // foreach (var userRole in userRoles)
            // {
            //     authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            // }

            var authClaims = new List<Claim>();

            authClaims.Add(new Claim(ClaimTypes.Name, model.Username));

            string jwtSecret =  _configuration["JWT:Secret"];
            string jwtValidIssuer = _configuration["JWT:ValidIssuer"];
            string jwtValidAudience = _configuration["JWT:ValidAudience"];

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                issuer: jwtValidIssuer,
                audience: jwtValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });

        // var user = await userManager.FindByNameAsync(model.Username);
        // if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        // {
        //     var userRoles = await userManager.GetRolesAsync(user);

        //     var authClaims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name, user.UserName),
        //             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //         };

        //     foreach (var userRole in userRoles)
        //     {
        //         authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //     }

        //     var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //     var token = new JwtSecurityToken(
        //         issuer: _configuration["JWT:ValidIssuer"],
        //         audience: _configuration["JWT:ValidAudience"],
        //         expires: DateTime.Now.AddHours(3),
        //         claims: authClaims,
        //         signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //         );

        //     return Ok(new
        //     {
        //         token = new JwtSecurityTokenHandler().WriteToken(token),
        //         expiration = token.ValidTo
        //     });
        // }

        return Unauthorized();
    }

}
