using CSLib;
using CSLib.BackEnd.JSon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class DistributorController : Controller
    {
        // GET: Distributor
        public ActionResult Index()
        {
            return View();
        }

        public String GetListData(String begin_index, String end_index)
        {
            String lResult = "";
            String lSQL = "";
            DataTable lDT = null;

            lSQL += " DECLARE @row_count INT";
            lSQL += " SELECT @row_count = COUNT(*) FROM T_Distributor";
            lSQL += " SELECT * FROM";
            lSQL += " (";
            lSQL += " 	SELECT ";
            lSQL += " 	Code";
            lSQL += " 	,Name";
            lSQL += " 	,Province";
            lSQL += " 	,Address";
            lSQL += " 	,Telephone ";
            lSQL += " 	,ROW_NUMBER()OVER(ORDER BY Code) i";
            lSQL += " 	,@row_count c";
            lSQL += " 	FROM T_Distributor";
            lSQL += " )T";
            lSQL += " WHERE i>=" + begin_index + " AND i<=" + end_index;

            DBHelper.GetDataTable(lSQL, ref lDT);

            lResult = ConvertToJson.FromDataTable(lDT);

            return lResult;
        }
    }
}