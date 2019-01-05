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

            lSQL += " SELECT COUNT(*) FROM T_Distributor";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lRowCount = Convert.ToInt32(lDT.Rows[0][0].ToString());
                lPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lRowCount) / Convert.ToDouble(page_size)));

                lBegin_Index = (lPageIndex - 1) * lPageSize + 1;
                lEnd_Index = lPageIndex * lPageSize;

                lSQL = " SELECT * FROM";
                lSQL += " (";
                lSQL += "    SELECT ";
                lSQL += "            " + lRowCount + "   RowCnt";
                lSQL += "           ," + lPageCount + " PageCnt";
                lSQL += "           ," + lBegin_Index + " BeginIndex";
                lSQL += "           ," + lEnd_Index + " EndIndex";
                lSQL += " 	,Code";
                lSQL += " 	,Name";
                lSQL += " 	,Province";
                lSQL += " 	,Address";
                lSQL += " 	,Telephone ";
                lSQL += " 	,ROW_NUMBER()OVER(ORDER BY Code) i";
                lSQL += " 	FROM T_Distributor";
                lSQL += " )T";
                lSQL += " WHERE i>=" + lBegin_Index + " AND i<=" + lEnd_Index;

                DBHelper.GetDataTable(lSQL, ref lDT);

                lResult = ConvertToJson.FromDataTable(lDT);
            }

            return lResult;
        }
    }
}