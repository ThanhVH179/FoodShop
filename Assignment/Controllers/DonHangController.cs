using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Interfaces;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class DonHangController : BaseController
    {
        private IDonHangService _donHangService;
        public DonHangController(IDonHangService donHangService)
        {
            _donHangService = donHangService;
        }
        // GET: DonHangController
        public ActionResult Index()
        {
            return View(_donHangService.GetDonHangAll());
        }

        // GET: DonHangController/Details/5
        public ActionResult Details(int id)
        {
            return View(_donHangService.GetDonHang(id));
        }

        // GET: DonHangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonHangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DonHangController/Edit/5
        public ActionResult Edit(int id)
        {
            var donHang = _donHangService.GetDonHang(id);
            return View(donHang);
        }

        // POST: DonHangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DonHang donHang)
        {
            try
            {
                donHang.KhachHang = null;
                _donHangService.EditDonHang(id, donHang);
                return RedirectToAction(nameof(Details), new { id = donHang.DonHangID });
            }
            catch
            {
                return View();
            }
        }

        // GET: DonHangController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DonHangController/Delete/5
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
