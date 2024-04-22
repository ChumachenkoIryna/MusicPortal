using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.Controllers
{
    public class AccountController : Controller
    { 
       IUserRepository repo;

    public AccountController(IUserRepository r)
    {
        repo = r;
    }
    public ActionResult Login()
    {
        return View();
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var users =  repo.GetUsersList();
                if ( users == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var userList = repo.CheckUser(logon);
                if (userList.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var user = userList.First();
               
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("Level", user.Level);
                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                var result = repo.CheckRegisterUser(reg);
                if (result.ToList().Count > 0)
                {
                    ModelState.AddModelError("", "Such username already exists, try another one!");
                    return View(reg);
                }
                user.Username = reg.Username;
                byte[] saltbuf = new byte[16];
                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);
                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);
                byte[] byteHash = SHA256.HashData(password);
                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));
                user.Password = hash.ToString();
                user.Salt = salt;
                if (reg.Username == "admin" || reg.Username == "Admin")
                    user.Level = 2;
                else
                    user.Level = 0;
                repo.Create(user);
                return RedirectToAction("Login");
            }

            return View(reg);
        }
    }
}

