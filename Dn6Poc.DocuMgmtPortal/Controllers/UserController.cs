using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dn6Poc.DocuMgmtPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        //private readonly IHttpClientFactory _http;
        private readonly UserService _userService;


        public UserController(ILogger<UserController> logger, IConfiguration configuration, UserService _userService)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = _userService;
        }

        // GET: UserController
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Add
        public ActionResult Add()
        {
            return View(new AddUserViewModel
            {
                Username ="jane-doe",
                Password ="jane-doe-password",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane-doe@anonymous-company.com"
            });
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Make 
                // 
                //TempData[""]
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
