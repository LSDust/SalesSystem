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

        public void InitCommdity()
        {
            RetailerManage db = new RetailerManage();
            db.SelectCommodity(Session["id"].ToString());
        }
        [HttpPost]
        public ActionResult AddCommdity(string cname, float cost, float price, string unit)
        {
            string pic = Request.Form["pic"];
            RetailerManage db = new RetailerManage();
            db.InertCommodity(cname, pic, cost, price, unit);
            db.Closedb();
            return View();
        }
    }
}