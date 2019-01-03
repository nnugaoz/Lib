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

        public String GetDataList(String begin_index, String end_index)
        {
            String lResult = "";
            String lSQL = "";
            DataTable lDT = null;

            lSQL += "DECLARE @row_count INT";
            lSQL += " SELECT @row_count=COUNT(*) FROM T_Product_Characteristic WHERE Del='0'";

            lSQL += " SELECT";
            lSQL += "        ID,";
            lSQL += "        Type,";
            lSQL += "        Characteristic,";
            lSQL += "        @row_count RowCnt";
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
            lSQL += "                        i>=" + begin_index;
            lSQL += "                AND     i<=" + end_index + " )";
            lSQL += " AND     Del='0'";
            lSQL += " ORDER BY";
            lSQL += "        Type,";
            lSQL += "        Characteristic";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lResult = ConvertToJson.FromDataTable(lDT);
            }

            return lResult;
        }
    }
}