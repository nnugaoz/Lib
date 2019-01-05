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
        public ActionResult Index()
        {
            return View();
        }

        public String GetListData(String page_index, string page_size)
        {
            String lResult = "";
            String lSQL = "";
            DataTable lDT = null;
            Int32 lPageIndex = 0;
            Int32 lPageSize = 0;
            Int32 lRowCount = 0;
            Int32 lPageCount = 0;
            Int32 lBegin_Index = 0;
            Int32 lEnd_Index = 0;

            lPageIndex = Convert.ToInt32(page_index);
            lPageSize = Convert.ToInt32(page_size);

            lSQL += "    SELECT";
            lSQL += "           COUNT(1)";
            lSQL += "    FROM";
            lSQL += "           T_Product";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lRowCount = Convert.ToInt32(lDT.Rows[0][0].ToString());
                lPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lRowCount) / Convert.ToDouble(page_size)));

                lBegin_Index = (lPageIndex - 1) * lPageSize + 1;
                lEnd_Index = lPageIndex * lPageSize;

                lSQL = "   SELECT * FROM(";
                lSQL += "    SELECT ";
                lSQL += "            " +  lRowCount + "   RowCnt";
                lSQL += "           ," + lPageCount + " PageCnt";
                lSQL += "           ," + lBegin_Index + " BeginIndex";
                lSQL += "           ," + lEnd_Index + " EndIndex";
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
                lSQL += "             WHERE i >= " + lBegin_Index + " AND i <= " + lEnd_Index;


                if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
                {
                    lResult = ConvertToJson.FromDataTable(lDT);
                }
            }

            return lResult;
        }
    }
}