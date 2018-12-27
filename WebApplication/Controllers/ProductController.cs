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

        public String GetListData(String begin_index, String end_index)
        {
            String lJsonStr = "";
            String lSQL = "";
            DataTable lDT = null;
            lSQL += "DECLARE";
            lSQL += "    @RowCount INT";
            lSQL += "    SELECT";
            lSQL += "           @RowCount=COUNT(1)";
            lSQL += "    FROM";
            lSQL += "           T_Product";
            lSQL += "   SELECT * FROM(";
            lSQL += "        SELECT";
            lSQL += "               @RowCount RowCnt";
            lSQL += "             , ROW_NUMBER()OVER(ORDER BY Code) i";
            lSQL += "             , Code";
            lSQL += "             , Name";
            lSQL += "             , ShortName";
            lSQL += "             , Type";
            lSQL += "             , Voltage";
            lSQL += "             , Spec";
            lSQL += "             , ClassID";
            lSQL += "        FROM";
            lSQL += "               T_Product)T";
            lSQL += "   WHERE i>=" + begin_index + " AND i<=" + end_index;
            

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lJsonStr = ConvertToJson.FromDataTable(lDT);
            }

            return lJsonStr;
        }
    }
}