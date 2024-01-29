using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SkyLux.Models;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SkyLux.Controllers
{
    public class UserController : Controller
    {

        private readonly AirlineContext _sql;
        public UserController(AirlineContext sql)
        {
            _sql = sql;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin u)
        {
            var Md5Pass = CreateMD5Hash(u.UserPassword);
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _sql.Users.SingleOrDefault(x => x.UserName == u.UserName && x.UserPassword == Md5Pass);

            if (user != null)
            {
                List<Claim> claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Sid,user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,user.UserName),
                };

                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var prims = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, prims, props).Wait();

                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View();
            }

        }
        public static string CreateMD5Hash(string input)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }


        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(User u)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            u.UserPassword = CreateMD5Hash(u.UserPassword);
            u.UserBlock = "blocked";
            _sql.Users.Add(u);
            _sql.SaveChanges();
            return RedirectToAction("Index", "Home");


        }

        //public IActionResult Profil(int id)
        //{
        //    var element = _sql.Users.Where(x => x.UserId == id).ToList();
        //    return View(element);
        //}


        //public IActionResult UserActive(int id)
        //{
        //    if (_sql.Users.SingleOrDefault(x => x.UserId == id).UserBlock == "blocked")
        //    {
        //        _sql.Users.SingleOrDefault(x => x.UserId == id).UserBlock = "unblocked";

        //    }
        //    else
        //    {
        //        _sql.Users.SingleOrDefault(x => x.UserId == id).UserBlock = "blocked";

        //    }
        //    _sql.SaveChanges();
        //    return RedirectToAction("UserPanel", "User");
        //}

        //public IActionResult UserPanel(string? search="")
        //{
        //    var a = _sql.Users.Where(x=>x.UserName.Contains(search)).ToList();
        //    return View(a);
        //}

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
