using Sitecore.DemoProject.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.DemoProject.MVC.Web.Extensions;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Fields;

namespace Sitecore.DemoProject.MVC.Web.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            var model = new NavigationViewModel();
            List<Navigation> navigations = new List<Navigation>();

            var homeItem = Sitecore.Context.Site.HomeItem();
            navigations.Add(BuildNavigation(homeItem));

            if(homeItem.HasChildren)
            {
                foreach(Item childItem in homeItem.Children)
                {
                    CheckboxField hideInNavigation = childItem.Fields["Hide_in_Navigation"];
                    if (hideInNavigation!=null && !hideInNavigation.Checked)
                    {
                        navigations.Add(BuildNavigation(childItem));
                    }
                }
            }
            model.Navigations = navigations;

            return View(model);
        }

        private Navigation BuildNavigation(Item item)
        {
            return new Navigation
            {
                NavigationTitle = item.Fields["Navigation_Title"]?.Value,
                NavigationLink = item.Url(),
                ActiveClass = PageContext.Current.Item.ID == item.ID ? "active" : string.Empty
            };
        }
    }
}