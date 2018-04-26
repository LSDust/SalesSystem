using SalesSystem.Models;
using System;
using System.Collections;
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
        public ActionResult CommodityInfo()
        {
            return View();
        }

        public String InitCommodity()
        {
            RetailerManage db = new RetailerManage();
            string a = db.SelectCommodity(Session["id"].ToString());
            return a;
        }
        [HttpPost]
        public ActionResult AddCommodity(string cname, float cost, float price, string unit)
        {
            string pic = Request.Form["pic"];
            RetailerManage db = new RetailerManage();
            db.InertCommodity(cname, pic, cost, price, unit);
            db.Closedb();
            return View();
        }
    }
}