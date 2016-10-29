using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [LocalDebugOnly]
    public class MBController : BaseController
    {
        // GET: MB
        [Share頁面上常用的ViewBag變數資料]
        public ActionResult Index()
        {
            //ViewData["temp1"] = "Hello";

            var b = new ClientLoginViewModels()
            {
                FirstName = "Kent",
                LastName = "Wu"
            };
            
            ViewData["Temp2"] = b;

            return View();
        }

        public ActionResult MyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModels c)
        {
            if (ModelState.IsValid)
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var data = db.Product.OrderBy(p => p.ProductId).Take(10);
            return View(data);
        }
        
        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {
            foreach(var item in items)
            {
                var data = db.Product.Find(item.ProductId);
                data.ProductName = item.ProductName;
                data.Active = item.Active;
                data.Stock = item.Stock;
                data.Price = item.Price;
            }
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}