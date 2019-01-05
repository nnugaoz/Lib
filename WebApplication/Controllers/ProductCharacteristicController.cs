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
    public class ProductCharacteristicController : Controller
    {
        // GET: ProductCharacteristic
        public ActionResult Index()
        {
            return View();
        }

        public String GetDataList(String page_index, string page_size)
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

            lSQL = " SELECT COUNT(*) FROM T_Product_Characteristic WHERE Del='0'";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lRowCount = Convert.ToInt32(lDT.Rows[0][0].ToString());

                lSQL = "SELECT COUNT(DISTINCT Type)";
                lSQL += " FROM";
                lSQL += " T_Product_Characteristic";
                lSQL += " WHERE";
                lSQL += " Del='0'";

                if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
                {
                    lPageCount = Convert.ToInt32(lDT.Rows[0][0].ToString());

                    lBegin_Index = (lPageIndex - 1) * lPageSize + 1;
                    lEnd_Index = lPageIndex * lPageSize;

                    lSQL = "    SELECT ";
                    lSQL += "            " + lRowCount + "   RowCnt";
                    lSQL += "           ," + lPageCount + " PageCnt";
                    lSQL += "           ," + lBegin_Index + " BeginIndex";
                    lSQL += "           ," + lEnd_Index + " EndIndex";
                    lSQL += "        ,ID";
                    lSQL += "        ,Type";
                    lSQL += "        ,Characteristic";
                    lSQL += " FROM";
                    lSQL += "        T_Product_Characteristic";
                    lSQL += " WHERE";
                    lSQL += "        Type IN";
                    lSQL += "        (";
                    lSQL += "                SELECT";
                    lSQL += "                        Type";
                    lSQL += "                FROM";
                    lSQL += "                        (";
                    lSQL += "                                SELECT";
                    lSQL += "                                        ROW_NUMBER()OVER(ORDER BY Type) i ,";
                    lSQL += "                                        Type";
                    lSQL += "                                FROM";
                    lSQL += "                                        (";
                    lSQL += "                                                SELECT DISTINCT";
                    lSQL += "                                                        Type";
                    lSQL += "                                                FROM";
                    lSQL += "                                                        T_Product_Characteristic";
                    lSQL += "                                                WHERE";
                    lSQL += "                                                        Del='0') T1)T2";
                    lSQL += "                WHERE";
                    lSQL += "                        i>=" + lBegin_Index;
                    lSQL += "                AND     i<=" + lEnd_Index + " )";
                    lSQL += " AND     Del='0'";
                    lSQL += " ORDER BY";
                    lSQL += "        Type,";
                    lSQL += "        Characteristic";

                    if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
                    {
                        lResult = ConvertToJson.FromDataTable(lDT);
                    }
                }
            }

            return lResult;
        }
    }
}