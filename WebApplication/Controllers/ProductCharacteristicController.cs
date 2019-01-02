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

            lSQL += " DECLARE";
            lSQL += "         @row_count INT";
            lSQL += "         SELECT";
            lSQL += "                 @row_count=COUNT(*)";
            lSQL += "         FROM";
            lSQL += "                 T_Product_Characteristic";
            lSQL += "         WHERE";
            lSQL += "                 Del='0'";
            lSQL += "         SELECT *";
            lSQL += "         FROM";
            lSQL += "                 (";
            lSQL += "                         SELECT";
            lSQL += "                                 ROW_NUMBER()OVER(ORDER BY ID) i      ,";
            lSQL += "                                 @row_count                    RowCnt ,";
            lSQL += "                                 ID                                   ,";
            lSQL += "                                 Type                                 ,";
            lSQL += "                                 Characteristic";
            lSQL += "                         FROM";
            lSQL += "                                 T_Product_Characteristic";
            lSQL += "                         WHERE";
            lSQL += "                                 Del='0')T";
            lSQL += "         WHERE";
            lSQL += "                 i>=" + begin_index;
            lSQL += "         AND     i<=" + end_index;

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lResult = ConvertToJson.FromDataTable(lDT);
            }

            return lResult;
        }
    }
}