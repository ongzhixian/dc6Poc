using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dn6Poc.DocuMgmtPortal.Controllers
{
    public class RoleController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly RoleService _roleService;


        public RoleController(ILogger<UserController> logger, RoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        // GET: RoleController
        public ActionResult Index()
        {

            return View();
        }

        // GET: RoleController/Details/5
        public ActionResult Details(string id)
        {
            return View(new RoleDetailsViewModel(id));
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddRoleFormModel collection)
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

        // GET: RoleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleController/Edit/5
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

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleController/Delete/5
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
