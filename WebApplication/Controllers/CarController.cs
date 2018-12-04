using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSLib;
using System.Data;
namespace WebApplication.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/
        public ActionResult Index()
        {
            return View();
        }

        public String HelloWorld()
        {

            MSSQLHelper lMSSQLHelper = new MSSQLHelper();
            String lSQL = "SELECT GETDATE()";
            DataSet lDS = lMSSQLHelper.Query(lSQL, null);

            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables[0].Rows.Count > 0)
            {
                return "{\"Return\":\"" + lDS.Tables[0].Rows[0][0].ToString() + "\"}";
            }
            else
            {
                return "{\"Return\":\"\"}";
            }
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}