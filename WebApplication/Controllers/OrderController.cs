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
    public class OrderController : Controller
    {
        private static readonly log4net.ILog log
= log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Order
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

            lSQL += "       SELECT COUNT(1)";
            lSQL += "       FROM";
            lSQL += "       T_Order;";
            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lRowCount = Convert.ToInt32(lDT.Rows[0][0].ToString());
                lPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lRowCount) / Convert.ToDouble(page_size)));

                lBegin_Index = (lPageIndex - 1) * lPageSize + 1;
                lEnd_Index = lPageIndex * lPageSize;

                lSQL = "SELECT * FROM(";
                lSQL += "    SELECT ";
                lSQL += "            " + lRowCount + "   RowCnt";
                lSQL += "           ," + lPageCount + " PageCnt";
                lSQL += "           ," + lBegin_Index + " BeginIndex";
                lSQL += "           ," + lEnd_Index + " EndIndex";
                lSQL += "     , ROW_NUMBER()OVER(ORDER BY ID) SNO";
                lSQL += "     , ID";
                lSQL += "     , CustomerCode";
                lSQL += "     , DistributorCode";
                lSQL += "     , SubmitDate";
                lSQL += "     , SubmitMan";
                lSQL += "     , RecAddress";
                lSQL += "     , DeliveryDate";
                lSQL += "     , Money";
                lSQL += "     , InvoiceMoney";
                lSQL += "     FROM";
                lSQL += "       T_Order";
                lSQL += ")T";
                lSQL += " WHERE SNO >= " + lBegin_Index + " AND SNO <= " + lEnd_Index;

                if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
                {
                    lResult = ConvertToJson.FromDataTable(lDT);
                }
            }
            return lResult;
        }
    }
}