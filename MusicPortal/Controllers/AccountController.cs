using Microsoft.AspNetCore.Mvc;

namespace MusicPortal.Controllers
{
    using System.Text;
    using MusicPortal.Models;
    using System.Security.Cryptography;
    using MusicPortal.Filters;
    using MusicPortal.BLL.Interfaces;
    using MusicPortal.BLL.DTO;

    [Culture]
    public class AccountController(IUserService userService, IPasswordHashing hashing) : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            ViewData["Menu"] = "none";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UserLogin User)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (!ModelState.IsValid) return View(User);
            var user = userService.GetUserFirstOrDefault(User.Login!);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, Resources.Resource.IncorrectUsernameOrPassword);
                return View(User);
            }
            if (!hashing.PassowordEquals(user.Salt!, User.Password!, user.Password!))
            {
                ModelState.AddModelError(String.Empty, Resources.Resource.IncorrectUsernameOrPassword);
                return View(User);
            }
            if (user.IsAdmin == true) HttpContext.Session.SetString("IsAdmin", "true");
            if (user.IsActivated == true) HttpContext.Session.SetString("IsActivated", "true");
            return RedirectToAction("Index", "Music");
        }

        public IActionResult Registration()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationModel User)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (!ModelState.IsValid) return View(User);
            PasswordAndSaltDTO password = hashing.Hashing(User.Password!);
            bool IsAdmin = false;
            if (HttpContext.Session.GetString("Admin") != null)
                IsAdmin = true;
            var user = new UserDTO { Name = User.Name, Surname = User.Surname, Login = User.Login, Password = password.Password, Salt = password.Salt, Year = User.Year, IsActivated = false, IsAdmin = IsAdmin };
            if (IsAdmin == true) HttpContext.Session.SetString("IsAdmin", "true");
            userService.AddUser(user);
            return RedirectToAction("Index", "Music");
        }

        public IActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Music");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeLanguage(string langCode)
        {
            IRead readLanguage = HttpContext.RequestServices.GetService<IRead>()!;
            IEnumerable<string> codes = readLanguage.GetList().Select(l => l.Code);
            if(!codes.Contains(langCode))
            {
                langCode = "uk";
            }
            Response.Cookies.Append("lang", langCode, new CookieOptions { Expires = DateTime.Now.AddDays(10) });
            return Redirect(HttpContext.Session.GetString("path")!);
        }
    }
}
