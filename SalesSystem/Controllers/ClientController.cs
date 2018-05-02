using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesSystem.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            return View();
        }
        public String InitPorductList()
        {
            Client db = new Client();
            string jsondata = db.SelectCommodityInfo(Session["id"].ToString());
            db.Closedb();
            return jsondata;
        }
        public String InitRetailer()
        {
            Client db = new Client();
            string jsondata = db.SelectRetailerInfo();
            db.Closedb();
            return jsondata;
        }
        public ActionResult Personal()
        {
            return View();
        }
        public bool UpdatePurchaserInfo(string name,string tel)
        {
            Client db = new Client();
            bool flag = db.UpdatePurchaser(Session["id"].ToString(), name, tel);
            db.Closedb();
            return flag;
        }
    }
}