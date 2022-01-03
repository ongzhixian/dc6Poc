using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
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
        private readonly RoleService _roleService;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, UserService userService, RoleService roleService)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
        }

        // GET: UserController
        public async Task<ActionResult> IndexAsync()
        {
            IEnumerable<User>? userList = await _userService.GetUserListAsync(10, 1);

            return View(userList);
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
        public async Task<ActionResult> AddAsync(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Make 
                // 
                //TempData[""]
                
                await _userService.AddUserAsync(model);

                return View(model);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: UserController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(IndexAsync));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(IndexAsync));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SuspendAsync(string id)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            try
            {
                await _userService.UpdateUserStatusAsync(id, UserStatus.Suspended);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActivateAsync(string id)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            try
            {
                await _userService.UpdateUserStatusAsync(id, UserStatus.Active);

                //return View(model);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAdminRoleAsync(string id)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            try
            {
                await _roleService.AddUserRoleAsync(id, "Form-designer");

                //return View(model);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveAdminRoleAsync(string id)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            try
            {
                await _roleService.RemoveUserRoleAsync(id, "Form-designer");

                //return View(model);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }


    }
}
