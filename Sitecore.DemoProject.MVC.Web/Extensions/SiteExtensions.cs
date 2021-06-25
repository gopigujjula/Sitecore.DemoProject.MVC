using Sitecore.Data.Items;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.DemoProject.MVC.Web.Extensions
{
    public static class SiteExtensions
    {
        public static Item HomeItem(this SiteContext siteContext)
        {
            return Context.Database.GetItem(siteContext.StartPath);
        }
    }
}