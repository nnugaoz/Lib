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
        // GET: Order
        public ActionResult List()
        {
            return View();
        }

        public String GetListData()
        {
            String lDataInJsonStr = "";
            String lSQL = "";
            lSQL += "DECLARE @Count INT";
            lSQL += "       SELECT @Count=COUNT(1)";
            lSQL += "       FROM";
            lSQL += "       T_Order;";

            lSQL += "SELECT TOP(10)";
            lSQL += "     @Count RowCnt";
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
            DataTable lDT = null;
            if (DBHelper.GetDataTable(lSQL, ref lDT) == EXESQLRET.SUCCESS)
            {
                lDataInJsonStr = ConvertToJson.FromDataTable(lDT);
            }
            return lDataInJsonStr;
        }
    }
}