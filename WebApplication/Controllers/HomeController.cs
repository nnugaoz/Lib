using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        //应用程序首页
        public ActionResult Index()
        {
            return View();
        }
	}
}