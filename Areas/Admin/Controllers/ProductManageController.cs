﻿using _3_Asp.Net_MVC.Controllers;
using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace _3_Asp.Net_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProductManageController : Controller
    {
        private readonly ILogger<ProductManageController> _logger;
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        public ProductManageController(ILogger<ProductManageController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices();
            _categoryServices = new CategoryServices();
        }
        public void LoadCategory(long? selectedID = null)
        {
            var loai = _categoryServices.GetAllCategory();
            ViewData["TypeName"] = loai;
        }
        // ROLL BACK KHI XÓA
        public IActionResult RollBackDelete()
        {
            var products = _productServices.GetAllProduct();
            if (SessionServices.GetProductFromSession(HttpContext.Session, "RollBackDelete") == null)
            {
                return View("Product", products);
            }
            else
            {
                var sps = SessionServices.GetProductFromSession(HttpContext.Session, "RollBackDelete");
                foreach (var product in sps)
                {
                    _productServices.CreateProduct(product);
                }
                sps.Clear();
                SessionServices.SetProductToJson(HttpContext.Session, "RollBackDelete", sps);
            }
            products = _productServices.GetAllProduct();
            return View("Product", products);
        }
        // TÌM KIẾM SẢN PHẨM THEO MÔ TẢ
        [HttpGet]
        public IActionResult FilterPrices(int min, int max)
        {
            var sanpham = _productServices.GetAllProduct();
            var products = sanpham.Where(x => x.Price >= min && x.Price <= max);
            return View("Product", products);

        }
        //LỌC SẢN PHẨM
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
                    return View("Product", products);
                }
                return View("Product", products);
            }
            return View("Product", products);
        }
        //LỌC Trạng thái
        [HttpGet]
        public IActionResult FilterStatus(int status)
        {
            List<Product> products = _productServices.GetAllProduct();
            if (status != null)
            {
                products = _productServices.GetAllProduct().Where(c => c.Status == status).ToList();
                return View("Product", products);
            }
            return View("Product", products);
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
                return View("Product", sanpham);
            }
            return View("Product", sanpham);
        }
        // HIỂN THỊ DANH SÁCH SẢN PHẨM
        public IActionResult Redirect()
        {
            LoadCategory();
            var Products = _productServices.GetAllProduct();
            return View("Product", Products); // Trả về một View cụ thể đi kèm với Model
        }

        // TẠO SẢN PHẨM MỚI
        public IActionResult Create() // Chỉ thực hiện việc hiển thị ra form Create
        {
            LoadCategory();
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile imageUploader) // Thực hiện chức năng thêm
        {
            LoadCategory();
            try
            {
                if (imageUploader != null && imageUploader.Length > 0) // Không null và không trống
                {
                    string _FileName = Path.GetFileName(imageUploader.FileName);
                    string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productimages", _FileName);
                    using (var fileStream = new FileStream(_filePath, FileMode.Create))
                    {
                        imageUploader.CopyToAsync(fileStream);
                    }
                    product.Image = _FileName;
                    product.ProductName = product.ProductName.TrimEnd().TrimStart();
                    _productServices.CreateProduct(product);
                }
                else
                {
                    product.ProductName = product.ProductName.TrimEnd();
                    _productServices.CreateProduct(product);
                }
                //return View();
                return RedirectToAction("Redirect", "ProductManage", new { area = "Admin" });
            }
            catch (Exception)
            {
                return View();
            }
        }
        // CHI TIẾT SẢN PHẨM
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Detail(Guid id) // Chỉ thực hiện việc hiển thị ra form Detail
        {
            var p = _productServices.GetProductById(id);
            return View(p);
        }

        // SỬA SẢN PHẨM
        [HttpPost]
        public IActionResult EditProduct(Product product, IFormFile imageUploader) // Thực hiện việc sửa 
        {
            LoadCategory();
            try
            {
                if (imageUploader != null && imageUploader.Length > 0)
                {
                    //Xóa hình cũ
                    //var p = _productServices.GetProductById(product.ID);
                    //string _filecu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productimages", p.Image);
                    //System.IO.File.Delete(_filecu);

                    // Thay thế bằng ảnh mới
                    string _FileName = Path.GetFileName(imageUploader.FileName);
                    string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productimages", _FileName);
                    product.Image = _FileName;
                    // Sửa và lưu ảnh

                    product.ProductName = product.ProductName.TrimEnd().TrimStart();
                    _productServices.UpdateProduct(product);
                    using (var fileStream = new FileStream(_filePath, FileMode.Create))
                    {
                        imageUploader.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    var p = _productServices.GetProductById(product.ID);
                    product.Image = p.Image;
                    product.ProductName = product.ProductName.TrimEnd().TrimStart();
                    _productServices.UpdateProduct(product);
                }
                //return View(product);
                return RedirectToAction("Redirect", "ProductManage", new { area = "Admin" });

            }
            catch (Exception)
            {
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id) // Chỉ thực hiện hiển thị ra form Edit
        {
            LoadCategory();
            var product = _productServices.GetProductById(id);
            return View(product);
        }
        // XÓA SẢN PHẨM
        public IActionResult Delete(Guid id)
        {
            var p = _productServices.GetProductById(id);
            var rollbackdelete = SessionServices.GetProductFromSession(HttpContext.Session, "RollBackDelete");
            rollbackdelete.Add(p);
            SessionServices.SetProductToJson(HttpContext.Session, "RollBackDelete", rollbackdelete);

            //string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productimages", p.Image);
            //if (System.IO.File.Exists(_filePath))
            //{
            //    System.IO.File.Delete(_filePath);
            //}

            if (_productServices.DeleteProduct(id))
            {
                return RedirectToAction("Redirect", "ProductManage", new { area = "Admin" });
            }
            return View("Index");
        }
    }

}
