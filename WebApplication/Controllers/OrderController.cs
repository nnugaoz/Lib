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

        public String GetListData(string begin_index, string end_index)
        {
            String lDataInJsonStr = "";
            String lSQL = "";
            lSQL += "DECLARE @Count INT";
            lSQL += "       SELECT @Count=COUNT(1)";
            lSQL += "       FROM";
            lSQL += "       T_Order;";

            lSQL += "SELECT * FROM(";
            lSQL += "SELECT ";
            lSQL += "     @Count RowCnt";
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
            lSQL += " WHERE SNO >= " + begin_index + " AND SNO <= " + end_index;

            DataTable lDT = null;
            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lDataInJsonStr = ConvertToJson.FromDataTable(lDT);
            }
            return lDataInJsonStr;
        }
    }
}