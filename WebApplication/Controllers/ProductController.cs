using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSLib;
using System.Data;
namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Car/
        public ActionResult List()
        {
            String lSQL = "SELECT Code, Name, Type, Voltage, Spec FROM T_Product";
            DataTable lDT = null;

            DBHelper.GetDataTable(lSQL, ref lDT);

            return View();
        }

    }
}