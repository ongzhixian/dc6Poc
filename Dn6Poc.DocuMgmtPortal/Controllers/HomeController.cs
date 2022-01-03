using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dn6Poc.DocuMgmtPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;

namespace Dn6Poc.DocuMgmtPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDataProtector _dataProtector;

    public HomeController(ILogger<HomeController> logger, IDataProtectionProvider dataProtectionProvider)
    {
        _logger = logger;

        var protectorPurpose = "whatever purpose you want";

        _dataProtector = dataProtectionProvider.CreateProtector(protectorPurpose);
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {

        return View();
    }


    //private string Pad(string text)
    //{
    //    var padding = 3 - ((text.Length + 3) % 4);
    //    if (padding == 0)
    //    {
    //        return text;
    //    }
    //    return text + new string('=', padding);
    //}

    //[Authorize(Roles ="Administrator")]
    [Authorize("RequireAdministratorRole")]
    public IActionResult Dashboard()
    {
        HttpContext.Session.SetString("HasDashBoard", "yes");
        
        //Session["LastPage"] = "someting"
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
