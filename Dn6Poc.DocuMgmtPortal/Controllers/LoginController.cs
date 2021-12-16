﻿using Dn6Poc.DocuMgmtPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dn6Poc.DocuMgmtPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;


        public LoginController(ILogger<AuthenticationController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _http = httpClientFactory;
        }

        // GET: LoginController
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(new LoginViewModel());
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
                if (!isValidCredentials())
                {
                    return Forbid();
                }


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

                await HttpContext.SignInAsync(
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

        private bool isValidCredentials()
        {
            // using (var client = new HttpClient())
            // {
            //     client.BaseAddress = new Uri("http://localhost:5001/api/login");
            //     //HTTP GET
            //     var responseTask = client.GetAsync("student");
            //     responseTask.Wait();

            //     var result = responseTask.Result;
            //     if (result.IsSuccessStatusCode)
            //     {
            //         var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
            //         readTask.Wait();

            //         students = readTask.Result;
            //     }
            //     else //web api sent error response 
            //     {
            //         //log response status here..

            //         students = Enumerable.Empty<StudentViewModel>();

            //         ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //     }
            // }

            //new HttpClient()
            

            using (var client = _http.CreateClient())
            {
                client.BaseAddress = new Uri("https://localhost:7241/api/Authentication/");

                LoginModel postData = new LoginModel
                {
                    Username = "yayay",
                    Password = "yayayPassword"
                };

                //HTTP POST
                var postTask = client.PostAsJsonAsync<LoginModel>("login", postData);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return true;
        }


        //// GET: LoginController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LoginController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: LoginController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LoginController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LoginController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
