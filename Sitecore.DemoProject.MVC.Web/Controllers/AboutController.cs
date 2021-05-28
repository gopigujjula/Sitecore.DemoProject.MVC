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
            //1.Pass Item to the view
            var model = new AboutViewModel()
            {
                Item = RenderingContext.Current?.Rendering.Item
            };
            //2. read the values, build the model and pass it to view
            
            //3. read the values using fieldrenderer (supports experience editor)
            return View(model);
        }
    }
}