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
    public class ProductClassController : Controller
    {
        // GET: ProductClass
        public ActionResult Index()
        {
            return View();
        }

        public String GetListData()
        {
            String lResult = "";
            String lSQL = "";
            DataTable lDT = null;

            lSQL += "DECLARE";
            lSQL += "    @RowCnt INT";
            lSQL += "    SELECT";
            lSQL += "           @RowCnt=COUNT(*)";
            lSQL += "    FROM";
            lSQL += "           T_Product_Class";
            lSQL += "    WHERE";
            lSQL += "           DEL='0'";
            lSQL += "    SELECT";
            lSQL += "             @RowCnt                       RowCnt";
            lSQL += "           , ROW_NUMBER()OVER(ORDER BY ID) SerNO";
            lSQL += "           , Title";
            lSQL += "    FROM";
            lSQL += "             T_Product_Class";
            lSQL += "    WHERE";
            lSQL += "             DEL='0'";

            if(DBHelper.GetDataTable(lSQL, ref lDT)== EXESQLRET.SUCCESS)
            {
                lResult = ConvertToJson.FromDataTable(lDT);
            }
            return lResult;
        }
    }
}