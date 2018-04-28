using SalesSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public void AddCommodity(string id, string cname, float cost, float price, string unit)
        {
            string pic = Request.Form["pic"];
            RetailerManage db = new RetailerManage();
            db.InertCommodity(id, cname, pic, cost, price, unit);
            db.Closedb();
            db.InserManage(Session["id"].ToString(), id, 0);
            db.Closedb();
        }
        public void SaveCommodity(string id,string cname,decimal cost,decimal price,string unit)
        {
            RetailerManage db = new RetailerManage();
            db.UpdateCommodity(id, cname, cost, price, unit);
        }
        public bool RemovalCommodity(string id)
        {
            RetailerManage db = new RetailerManage();
            db.DeleteManage(id);
            db.Closedb();
            bool flag = db.DeleteCommodity(id);
            db.Closedb();
            return flag;
        }
    }
}