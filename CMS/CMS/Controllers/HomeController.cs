using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.BLL;
using CMS.IBLL;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private InterfaceUserService userService;

        public HomeController()
        {
            userService = new UserService();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View(userService.Find(User.Identity.Name));
        }
    }
}