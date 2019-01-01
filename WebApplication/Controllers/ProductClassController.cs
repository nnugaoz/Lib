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

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(String ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        [HttpPost]
        public String Add(String pClassName)
        {
            String lSQL = "";
            DataTable lDT = null;
            String lResult = "";

            lSQL += "SELECT ";
            lSQL += " ID";
            lSQL += " FROM";
            lSQL += " T_Product_Class";
            lSQL += " WHERE Title='" + pClassName + "'";
            lSQL += " AND Del='0'";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                //产品分类重复
                lResult = "1";
            }
            else
            {
                lSQL = "INSERT INTO T_Product_Class(";
                lSQL += " ID";
                lSQL += ", Title";
                lSQL += ", Del";
                lSQL += ", EditMan";
                lSQL += ", EditDate";
                lSQL += ")VALUES(";
                lSQL += "'" + Guid.NewGuid().ToString() + "'";
                lSQL += ", '" + pClassName + "'";
                lSQL += ", '0'";
                lSQL += ", 'gaozhi'";
                lSQL += ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                lSQL += ")";

                if (DBHelper.ExecuteSQL(lSQL) == EXESQLRET.SUCCESS)
                {
                    lResult = "2";
                }
                else
                {
                    lResult = "0";
                }
            }
            return lResult;
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
            lSQL += "           , ID";
            lSQL += "           , Title";
            lSQL += "    FROM";
            lSQL += "             T_Product_Class";
            lSQL += "    WHERE";
            lSQL += "             DEL='0'";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lResult = ConvertToJson.FromDataTable(lDT);
            }
            return lResult;
        }

        public String GetByID(String ID)
        {
            String lResult = "";
            String lSQL = "";
            DataTable lDT = null;
            lSQL += " SELECT ";
            lSQL += " ID";
            lSQL += ", Title";
            lSQL += " FROM";
            lSQL += " T_Product_Class";
            lSQL += " WHERE Del='0'";
            lSQL += " AND ID='" + ID + "'";

            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lResult = ConvertToJson.FromDataTable(lDT);
            }

            return lResult;
        }

        public String Update(String ID, String Title)
        {
            String lResult = "";
            String lSQL = "";
            lSQL += "UPDATE T_Product_Class SET ";
            lSQL += " Title='" + Title + "'";
            lSQL += " WHERE ID='" + ID + "'";
            lSQL += " AND Del='0'";

            if (DBHelper.ExecuteSQL(lSQL) == EXESQLRET.SUCCESS)
            {
                lResult = "1";
            }
            else
            {
                lResult = "0";
            }
            return lResult;
        }
    }
}