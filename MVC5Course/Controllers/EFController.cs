﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));
            return View(data);
        }

        public ActionResult Create()
        {
            var product = new Product()
            {
                ProductName = "White Cars",
                Active = true,
                Price = 199,
                Stock = 5
            };

            db.Product.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var data = db.Product.Find(id);
            db.Product.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        public ActionResult Update(int id)
        {
            var data = db.Product.Find(id);
            data.ProductName += "!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Add20Percent()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));
            foreach(var item in data)
            {
                if (item.Price.HasValue)
                {
                    item.Price *= 1.2m;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}