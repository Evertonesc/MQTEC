using MQTEC.Models;
using MQTEC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MQTEC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserRepository _repository;

        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Save([Bind("Id,Name,Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(user);
                TempData["value"] = "Success";
                return RedirectToAction(nameof(Register));
            }
            else
            {
                return View();
            }           
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Login([Bind("Login,Password")] User user)
        {
            bool isValid = _repository.ValidateLogin(user);
            if (isValid)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["value"] = "isInvalid";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}