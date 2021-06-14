using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    public class NguoiDungController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoiDungService _nguoiDungService;
        public NguoiDungController(IWebHostEnvironment webHostEnvironment, INguoiDungService nguoiDungService)
        {
            _webHostEnvironment = webHostEnvironment;
            _nguoiDungService = nguoiDungService;
        }
        // GET: NguoiDungController
        public ActionResult Index()
        {
            return View(_nguoiDungService.GetNguoiDungAll());
        }

        // GET: NguoiDungController/Details/5
        public ActionResult Details(int id)
        {
            var nguoiDung = _nguoiDungService.GetNguoiDung(id);
            return View(nguoiDung);
        }

        // GET: NguoiDungController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NguoiDung nguoiDung)
        {
            try
            {
                _nguoiDungService.AddNguoiDung(nguoiDung);
                
                return RedirectToAction(nameof(Index), new { id = nguoiDung.NguoiDungID });
            }
            catch
            {
                return View();
            }
        }

        // GET: NguoiDungController/Edit/5
        public ActionResult Edit(int id)
        {
            var nguoiDung = _nguoiDungService.GetNguoiDung(id);
            return View(nguoiDung);
        }

        // POST: NguoiDungController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NguoiDung nguoiDung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _nguoiDungService.EditNguoiDung(id, nguoiDung);
                    return RedirectToAction(nameof(Details), new { id = nguoiDung.NguoiDungID });
                }               
            }
            catch
            {
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: NguoiDungController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NguoiDungController/Delete/5
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
