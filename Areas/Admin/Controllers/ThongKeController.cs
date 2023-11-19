using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3_Asp.Net_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ThongKeController : Controller
    {

        private readonly IUserServices userServices;
        private readonly IProductServices productServices;
        private readonly IBillServices billServices;
        private readonly IRoleServices roleServices;

        public ThongKeController()
        {
            userServices = new UserServices();
            productServices = new ProductServices();
            billServices = new BillServices();
            roleServices = new RoleServices();
        }
        public IActionResult ThongKe()
        {
            var cusid = roleServices.GetAllRole().FirstOrDefault(c => c.RoleName == "Khách vãng lai").ID;
            ViewBag.us = userServices.GetAllUser().Where(c=>c.RoleID == cusid).Count();
            ViewBag.pd = productServices.GetAllProduct().Count();
            ViewBag.bll = billServices.GetAllBill().Count();
            return View("ThongKe");
        }
        [HttpGet("/Admin/ThongKe/UserInfor/{id}")]
        public IActionResult UserInfor(Guid id)
        {
            var user = userServices.GetAllUser().FirstOrDefault(c => c.ID == id);
            return View(user);
        }
    }
}
