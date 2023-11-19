using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Models;
using _3_Asp.Net_MVC.Services;
using _3_Asp.Net_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace _3_Asp.Net_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IRoleServices roleServices;
        public LoginController()
        {
            userServices = new UserServices();
            roleServices = new RoleServices();
        }
        public IActionResult Login() // Chỉ thực hiện việc hiển thị ra form Create
        {
            var response = new LoginViewModel();
            return View(response);
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if(model.Password != model.ConfirmPassword)
            {
                TempData["Message"] = "Mật khẩu xác nhập không trùng khớp";
            }
            else
            {
                var rolekh = roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == "Khách hàng").ID;
                User us = new User()
                {
                    ID = new Guid(),
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Status = 1,
                    RoleID = rolekh,
                };
                userServices.CreateUser(us);
                TempData["Message"] = "Đăng ký thành công, quay lại trang đăng nhập";
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult UserInfor(Guid id)
        {
            var user = userServices.GetAllUser().FirstOrDefault(c=>c.ID == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = userServices.GetAllUser().Where(c => c.UserName == model.UserName || c.Email == model.UserName).FirstOrDefault();
            var rolekh = roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == "Khách hàng").ID;

            if (user != null)
            {
                foreach (var item in userServices.GetAllUser())
                {
                    if (user.Password.Equals(item.Password))
                    {
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("IdUser", (user.ID).ToString());
                        if (user.RoleID == rolekh) return RedirectToAction("Index", "Home");
                        return RedirectToAction("ListBills", "Bill", new { area = "Admin" });
                    }
                    else
                    {
                        TempData["Message"] = "Tên đăng nhập hoặc mật khẩu không đúng, Hãy nhập lại";
                        if (!ModelState.IsValid) return View(model);
                    }
                }
            }
            TempData["Message"] = "Tên đăng nhập hoặc mật khẩu không đúng, Hãy nhập lại";
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

    }
}
