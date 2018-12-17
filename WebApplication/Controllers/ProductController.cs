using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSLib;
using System.Data;
using CSLib.BackEnd.JSon;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Car/
        public ActionResult List()
        {
            return View();
        }

        public String GetListData()
        {
            String lJsonStr = "";
            String lSQL = "";
            DataTable lDT = null;

            lSQL = "SELECT TOP (100)";
            lSQL += " Code";
            lSQL += ", Name";
            lSQL += ", ShortName";
            lSQL += ", Type";
            lSQL += ", Voltage";
            lSQL += ", Spec";
            lSQL += ", ClassID";
            lSQL += " FROM T_Product";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lJsonStr = ConvertToJson.FromDataTable(lDT);
            }

            return lJsonStr;
        }
    }
}