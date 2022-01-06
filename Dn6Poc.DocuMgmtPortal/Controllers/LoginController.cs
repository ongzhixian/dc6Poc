using Dn6Poc.DocuMgmtPortal.Api;
using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dn6Poc.DocuMgmtPortal.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly ILoginService _loginService;

    public LoginController(ILogger<AuthenticationController> logger, ILoginService loginService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
    }

    // GET: LoginController
    [AllowAnonymous]
    public ActionResult Index()
    {
        return View(new LoginViewModel()
        {
            Username = "testuser@test.local",
            Password = "testPassword"
        });
    }

    // POST: LoginController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<ActionResult> AuthenticateAsync(LoginViewModel model)
    {
        _logger.LogInformation("Yep in AUthenticate");

        try
        {
            if (!await isValidCredentialsAsync(model))
            {
                return Forbid();
            }

            // TODO Get roles


            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    //new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext?.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    private async Task<bool> isValidCredentialsAsync(LoginViewModel model)
    {
        string token = await _loginService.LoginAsync(model.Username, model.Password);

        if (string.IsNullOrWhiteSpace(token))
        {
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return false;
        }

        HttpContext?.Session.SetString("JWT", token);

        return true;
    }
}
