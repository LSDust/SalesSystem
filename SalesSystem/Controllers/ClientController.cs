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
    }
}