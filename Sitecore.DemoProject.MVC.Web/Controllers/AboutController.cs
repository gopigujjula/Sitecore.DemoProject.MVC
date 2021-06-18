using Sitecore.DemoProject.MVC.Web.Models;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.DemoProject.MVC.Web.Controllers
{
    public class AboutController : Controller
    {       
        public ActionResult Index()
        {
            var model = new AboutViewModel()
            {
                Item = RenderingContext.Current?.Rendering.Item
            };
            return View(model);
        }
    }
}