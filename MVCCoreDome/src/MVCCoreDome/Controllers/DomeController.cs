using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCCoreDome.Controllers
{
    public class DomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public string Index(string name, int id = 1)
        //{
        //    return HtmlEncoder.Default.Encode("Hello " + name + ",Count is: " + id);
        //}
    }
}