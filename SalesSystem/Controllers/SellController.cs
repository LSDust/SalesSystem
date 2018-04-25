using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesSystem.Controllers
{
    public class SellController : Controller
    {
        // GET: Sell
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddCommdity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCommdity(string cname, float cost, float price, string unit)
        {
            string pic = Request.Form["pic"];
            var a = Session["tel"].ToString();
            RetailerManage db = new RetailerManage();
            db.InertCommodity(cname, pic, cost, price, unit);
            db.Closedb();
            return View();
        }
    }
}