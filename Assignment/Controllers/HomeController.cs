using Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Interfaces;
using Microsoft.AspNetCore.Http;
using Assignment.Constants;
using Newtonsoft.Json;
using Assignment.Filters;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _webHostEnvironment;
        IMonAnService _monAnService;
        IKhachHangService _KhachHangService;
        IDonHangService _donHangService;
        IDonHangChiTietService _donHangChiTietService;

        public HomeController(IWebHostEnvironment webHostEnvironment, IMonAnService monAnService, IKhachHangService khachHangService, IDonHangService donHangService, IDonHangChiTietService donHangChiTietService)
        {
            _webHostEnvironment = webHostEnvironment;
            _monAnService = monAnService;
            _KhachHangService = khachHangService;
            _donHangChiTietService = donHangChiTietService;
            _donHangService = donHangService;
        }
        public IActionResult Login(string returnUrl)
        {
            string Kh_email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_Email);
            if (Kh_email != null && Kh_email != "")
            {
                return RedirectToAction("Index", "Home");
            }
            #region Hiển thị login
            ViewWebLogin login = new ViewWebLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewWebLogin viewWebLogin)
        {
            if (ModelState.IsValid)
            {
                KhachHang khachHang = _KhachHangService.Login(viewWebLogin);
                if (khachHang != null)
                {
                    HttpContext.Session.SetString(SessionKey.KhachHang.KH_Email, khachHang.EmailKH);
                    HttpContext.Session.SetString(SessionKey.KhachHang.KH_FullName, khachHang.FullName);
                    HttpContext.Session.SetString(SessionKey.KhachHang.KhachHangContext,
                        JsonConvert.SerializeObject(khachHang));

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(viewWebLogin);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.KhachHang.KH_Email);
            HttpContext.Session.Remove(SessionKey.KhachHang.KH_FullName);
            HttpContext.Session.Remove(SessionKey.KhachHang.KhachHangContext);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang khachHang)
        {
            try
            {
                _KhachHangService.AddKhachHang(khachHang);
                return RedirectToAction(nameof(Login), new { id = khachHang.KhachHangID });
            }
            catch 
            {
                return View();
            }
        }

        [AuthenticationFilterAttribute_KH]
        public ActionResult Info()
        {
            string Kh_email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_Email);
            if (Kh_email == null && Kh_email == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var khContext = HttpContext.Session.GetString(SessionKey.KhachHang.KhachHangContext);
            var khId = JsonConvert.DeserializeObject<KhachHang>(khContext).KhachHangID;
            var khachhang = _KhachHangService.GetKhachHang(khId);
            return View(khachhang);
        }

        [AuthenticationFilterAttribute_KH]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(int khachHangID, KhachHang khachHang)
        {
            try
            {
                _KhachHangService.EditKhachHang(khachHangID, khachHang);
                return RedirectToAction(nameof(Index), new { id = khachHang.KhachHangID });
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View(_monAnService.GetMonAnAll());
        }

        public IActionResult AddCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                var monAn = _monAnService.GetMonAn(id);
                List<ViewCart> listCart = new List<ViewCart>()
                {
                    new ViewCart
                    {
                        MonAn = monAn,
                        Quantity = 1
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    var monAn = _monAnService.GetMonAn(id);
                    dataCart.Add(new ViewCart
                    {
                        MonAn = monAn,
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return Ok();
        }

        public IActionResult Cart()
        {
            List<ViewCart> dataCart = new List<ViewCart>();
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
            }
            return View(dataCart);
        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int soLuong)
        {
            var cart = HttpContext.Session.GetString("cart");
            double total = 0;
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart[i].Quantity = soLuong;
                        break;
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }

        public IActionResult DeleteCart(int id)
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnID == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }

        [AuthenticationFilterAttribute_KH]
        public IActionResult OrderCart()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_Email);
            if (kH_Email == null || kH_Email == "")  // đã có session
            {
                return BadRequest();
            }
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null && cart.Count() > 0)
            {
                #region DonHang
                var khachhangContext = HttpContext.Session.GetString(SessionKey.KhachHang.KhachHangContext);
                var khachhangId = JsonConvert.DeserializeObject<KhachHang>(khachhangContext).KhachHangID;

                double total = Tongtien();

                var donhang = new DonHang()
                {
                    Status = TrangThaiDonHang.Moidat,
                    KhachHangID = khachhangId,
                    Total = total,
                    OrderDay = DateTime.Now,
                    Note = "",
                };

                _donHangService.AddDonHang(donhang);
                int donhangId = donhang.DonHangID;

                #region Chitiet
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    DonHangChiTiet chitiet = new DonHangChiTiet()
                    {
                        DonHangID = donhangId,
                        MonAnID = dataCart[i].MonAn.MonAnID,
                        Quantity = dataCart[i].Quantity,
                        Price = dataCart[i].MonAn.Price * dataCart[i].Quantity,
                        Note = "",
                    };
                    //donhang.DonhangChitiets.Add(chitiet);
                    _donHangChiTietService.AddDonHangChiTiet(chitiet);
                }

                #endregion
                #endregion

                HttpContext.Session.Remove("cart");

                return Ok();
            }
            return BadRequest();
        }


        [NonAction]
        private double Tongtien()
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    total += (dataCart[i].MonAn.Price * dataCart[i].Quantity);
                }
            }
            return total;
        }

        [AuthenticationFilterAttribute_KH]
        public ActionResult Details(int id)
        {
            return View(_donHangService.GetDonHang(id));
        }

        [AuthenticationFilterAttribute_KH]
        public IActionResult History(int khachID)
        {
            string Kh_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_Email);
            var khachContext = HttpContext.Session.GetString(SessionKey.KhachHang.KhachHangContext);
            khachID = JsonConvert.DeserializeObject<KhachHang>(khachContext).KhachHangID;
            if (Kh_Email == null || Kh_Email == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_donHangService.GetDonHangByKhach(khachID));
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View(_monAnService.GetMonAnAll());
        }
        public IActionResult One()
        {
            List<MonAn> list = _monAnService.GetMonAnAll();
            var one = list.Where(x => x.Type == PhanLoai.MonAn || x.Type == PhanLoai.Nuoc).ToList();
            return View(one);
        }
        public IActionResult Combo()
        {
            List<MonAn> list = _monAnService.GetMonAnAll();
            var one = list.Where(x => x.Type == PhanLoai.MonAn).ToList();
            var combo = list.Except(one).ToList();
            return View(combo);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
