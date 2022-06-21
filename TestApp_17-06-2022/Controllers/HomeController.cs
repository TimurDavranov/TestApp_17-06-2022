using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestApp_17_06_2022.Classes;
using TestApp_17_06_2022.Data;
using TestApp_17_06_2022.Data.Entities;
using TestApp_17_06_2022.Enums;
using TestApp_17_06_2022.Models;

namespace TestApp_17_06_2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppDbContext _db;
        private readonly IActionClass<User> _actClass;
        private readonly IRepository<ProductFormViewModel> _productRep;
        private readonly IRepository<OrderFormViewModel> _orderRep;

        public HomeController(ILogger<HomeController> logger, IAppDbContext db, IActionClass<User> actClass, IRepository<ProductFormViewModel> productRep)
        {
            _logger = logger;
            _db = db;
            _actClass = actClass;
            _productRep = productRep;
        }

        public IActionResult Index(bool isReg = false)
        {
            var model = new LoginViewModel();
            if (isReg)
            {
                ViewBag.Registration = "SignIn";
                model.IsReg = true;
                return View(model);
            }
            else
            {
                ViewBag.Registration = "LogIn";
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!model.IsReg)
            {
                var result = await _actClass.Login(model);
                if (result != null)
                {
                    return RedirectToAction(nameof(Products), new {isAdmin = result.Role == Roles.Admin});
                }
                else
                {
                    TempData["error"] = "Wrong login/password";
                    return View(model);
                }
            }
            else
            {
                try
                {
                    var result = await _actClass.Signin(model);
                    return RedirectToAction(nameof(Products), new {isAdmin = result.Role == Roles.Admin});
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                    return View(model);
                }
                
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Products(bool isAdmin = false)
        {
            if (isAdmin) ViewBag.IsAdmin = true;

            var products = await _productRep.GetAll();
            var orders = await _orderRep.GetAll();

            var result = new ProductViewModel();
            result.OrderList = orders;
            result.ProductList = products;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> FormProduct(long Id)
        {
            if (Id == 0)
                return View();
            else
            {
                var data = await _productRep.Get(Id);
                return Ok(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> FormProduct(ProductFormViewModel model)
        {
            if (model.Id == 0)
            {
                await _productRep.Create(model);
            }
            else
            {
                await _productRep.Update(model);
            }
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(long Id)
        {
            _productRep.Delete(Id);
            return RedirectToAction(nameof(Products));
        }
        

        [HttpGet]
        public async Task<IActionResult> FormOrder(long Id)
        {
            ViewBag.Product = await _productRep.GetAll();
            
            if (Id == 0)
                return View();
            else
            {
                var data = await _orderRep.Get(Id);
                return Ok(data);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> FormOrder(OrderFormViewModel model)
        {
            if (model.Id == 0)
            {
                await _orderRep.Create(model);
            }
            else
            {
                await _orderRep.Update(model);
            }
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(long Id)
        {
            _orderRep.Delete(Id);
            return RedirectToAction(nameof(Products));
        }
    }
}