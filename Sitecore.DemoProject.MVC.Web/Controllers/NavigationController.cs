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

        #region Build Navigation by Crawling the Tree
        public ActionResult Index()
        {
            //Item by ID
            //var aboutItem = Sitecore.Context.Database.GetItem(new Data.ID("{0F244654-BF48-4DB5-A40B-1FEBC41E0DDB}"));
            //var readItemID = aboutItem.ID;
            //var readItemName = aboutItem.Name;

            //var aboutItemByPath = Sitecore.Context.Database.GetItem("/sitecore/content/Builderz/Home/About Us");
            //var readItemPath = aboutItem.Paths.FullPath;
            //var displayName = aboutItem.DisplayName;

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
        #endregion


        #region Build Navigation using datasource approach
        public ActionResult HeaderNavigationByDS()
        {
            var model = new NavigationViewModel();
            List<Navigation> parentNavigations = new List<Navigation>();

            var dataSource = RenderingContext.Current?.Rendering.Item;

            if (dataSource != null && dataSource.HasChildren)
            {
                foreach (Item parentItem in dataSource.Children)
                {
                    var parentNavigation = BuildNavigationByDS(parentItem);
                    var childNavigations = new List<Navigation>();

                    if (parentItem.HasChildren)
                    {
                        foreach (Item childItem in parentItem.Children)
                        {
                            var childNavigation = BuildNavigationByDS(childItem);
                            childNavigations.Add(childNavigation);
                        }
                        parentNavigation.ChildNavigations = childNavigations;
                    }
                    parentNavigations.Add(parentNavigation);
                }
            }
            model.Navigations = parentNavigations;
            return View(model);
        }

        private Navigation BuildNavigationByDS(Item item)
        {
            return new Navigation
            {
                NavigationTitle = item.Fields["Navigation_Title"]?.Value,
                NavigationLink = ((LinkField)item.Fields["Navigation_Link"]).GetFriendlyUrl(),
                ActiveClass = PageContext.Current.Item.ID == ((LinkField)item.Fields["Navigation_Link"]).TargetID ? "active" : string.Empty
            };
        }
        #endregion
    }
}