using MVC5Course.Models;
using MVC5Course.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            db.OrderLine.RemoveRange(data.OrderLine);
            db.Product.Remove(data);
            db.SaveChanges();
            /*錯誤示範
            foreach(var item in data.OrderLine.ToList())
            {
                db.OrderLine.Remove(item);
                db.SaveChanges();
            }
            */
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
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach(var entityErrors in ex.EntityValidationErrors)
                {
                    foreach(var vErrors in entityErrors.ValidationErrors)
                    {
                        throw new DbEntityValidationException(vErrors.PropertyName + " 發生錯誤 " + vErrors.ErrorMessage);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //public ActionResult Add20Percent()
        //{
        //    var data = db.Product.Where(p => p.ProductName.Contains("White"));
        //    foreach(var item in data)
        //    {
        //        if (item.Price.HasValue)
        //        {
        //            item.Price *= 1.2m;
        //        }
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Add20Percent()
        {
            var str = "%White%";
            db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price = Price * 1.2 WHERE ProductName LIKE @p0", str);

            return RedirectToAction("Index");
        }

        public ActionResult ClientContribution()
        {
            var data = db.vw_ClientOrderTotal.Take(100);
            return View(data);
        }

        public ActionResult ClientContribution2(string str = "Mary")
        {
            var data = db.Database.SqlQuery<ClientContributionViewModel>(@"
                SELECT
                    c.FirstName,
                    c.LastName,
                    (SELECT SUM(o.OrderTotal) FROM[dbo].[Order] o WHERE o.ClientId = c.ClientId) AS OrderTotal
                FROM[Client] AS c
                WHERE c.FirstName Like @p0", "%" + str + "%"
            );
            return View(data);
        }

        public ActionResult ClientContribution3(string keyword)
        {
            var data = db.usp_GetClientContribution(keyword);
            return View(data);
        }
    }
}