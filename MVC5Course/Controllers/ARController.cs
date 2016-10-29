﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContentTest()
        {
            return Content("開發實戰", "text/plan", Encoding.GetEncoding("Big5"));
        }

        public ActionResult FileTest()
        {
            var filePath = Server.MapPath("~/Content/imgres.jpg");
            return File(filePath, "image/jpeg");
        }
        public ActionResult FileTest2()
        {
            var filePath = Server.MapPath("~/Content/imgres.jpg");

            return File(filePath, "image/jpeg", "PPAP.jpg");
        }

        public ActionResult JsonTest()
        {
            //entity framework 才需要加這行
            db.Configuration.LazyLoadingEnabled = false;

            var data = db.Product.OrderBy(p => p.ProductId).Take(10);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}