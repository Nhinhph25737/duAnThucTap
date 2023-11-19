using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;

namespace _3_Asp.Net_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // _logger ghi lại tất cả hành động   
        private readonly IProductServices _productServices; // Interface
        private readonly ICategoryServices _categoryServices;
        private readonly IBillServices billServices;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices(); // new class (truyen vao 1 su phu thuoc) 
            _categoryServices = new CategoryServices();
            billServices = new BillServices();
        }
        public void LoadCategory()
        {
            var loai = _categoryServices.GetAllCategory();
            ViewBag.TypeName = loai;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Redirect()
        {
            LoadCategory();
            var Products = _productServices.GetAllProduct();
            if (Products.Count == 0)
            {
                TempData["Message"] = "Không có sản phẩm nào !";
            }
            return View("Product", Products); // Trả về một View cụ thể đi kèm với Model
        }
        [HttpGet]
        public IActionResult AllProduct()
        {
            var Products = _productServices.GetAllProduct().Where(c => c.AvailableQuantity > 0).ToList();
            return View("AllProduct", Products); // Trả về một View cụ thể đi kèm với Model
        }
        //TÌM KIẾM SẢN PHẨM
        [HttpGet]
        public IActionResult Search(string tim)
        {
            var sanpham = _productServices.GetAllProduct();
            if (!String.IsNullOrEmpty(tim))
            {
                tim = tim.ToLower();
                sanpham = _productServices.GetProductByName(tim).ToList();
                return View("AllProduct", sanpham);
            }
            return View("AllProduct", sanpham);
        }
        [HttpGet]
        public IActionResult FilterCategory(string category)
        {
            List<Product> products = _productServices.GetAllProduct();
            if (category != null)
            {
                Category categoryID = _categoryServices.GetCategoryByName(category);
                if (categoryID != null)
                {
                    products = _productServices.GetAllProduct().Where(c => c.TypeName == categoryID.ID).ToList();
                    return View("AllProduct", products);
                }
                return View("AllProduct", products);
            }
            return View("AllProduct", products);
        }
        [HttpGet]
        public IActionResult FilterPrice(int value)
        {
            List<Product> products = _productServices.GetAllProduct();
            if (value == 1)
            {
                products = products.OrderByDescending(c => c.Price).ToList();
                return View("AllProduct", products);
            }
            else if (value == 0)
            {
                products = products.OrderBy(c => c.Price).ToList();
                return View("AllProduct", products);
            }
            return View("AllProduct", products);
        }
        public IActionResult UserBills(Guid id) // Hiển thị tất cả hóa đơn của User hiện tại // Phải đăng nhập mới xem được
        {
            var Userbills = billServices.GetAllBill().Where(c=>c.UserID == id).ToList();
            if (Userbills.Count == 0)
            {
                TempData["Message"] = "Không có hóa đơn nào!";
            }
            return View(Userbills);
        }
        // Chi tiết sản phẩm
        public IActionResult Detail(Guid id) // Chỉ thực hiện việc hiển thị ra form Detail
        {
            var p = _productServices.GetProductById(id);
            return View(p);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}