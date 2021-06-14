using Assignment.Constants;
using Assignment.Interfaces;
using Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoiDungService _nguoiDungService;
        public AdminController(IWebHostEnvironment webHostEnvironment, INguoiDungService nguoiDungService)
        {
            _webHostEnvironment = webHostEnvironment;
            _nguoiDungService = nguoiDungService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            string username = HttpContext.Session.GetString(SessionKey.NguoiDung.UserName);
            if (username != null && username != "")
            {
                return RedirectToAction("Index", "Home");
            }
            #region Hiển thị login
            ViewLogin login = new ViewLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                NguoiDung nguoidung = _nguoiDungService.Login(viewLogin);
                if (nguoidung != null)
                {
                    HttpContext.Session.SetString(SessionKey.NguoiDung.UserName, nguoidung.UserName);
                    HttpContext.Session.SetString(SessionKey.NguoiDung.FullName, nguoidung.FullName);
                    HttpContext.Session.SetString(SessionKey.NguoiDung.NguoiDungContext,
                        JsonConvert.SerializeObject(nguoidung));

                    return RedirectToAction(nameof(Index), "Admin");
                }
            }
            return View(viewLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.NguoiDung.UserName);
            HttpContext.Session.Remove(SessionKey.NguoiDung.FullName);
            HttpContext.Session.Remove(SessionKey.NguoiDung.NguoiDungContext);
            return RedirectToAction(nameof(Index), "Admin");
        }
    }
}
