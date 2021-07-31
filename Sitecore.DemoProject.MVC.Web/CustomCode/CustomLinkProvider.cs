using Sitecore.Buckets.Managers;
using Sitecore.Buckets.Extensions;
using Sitecore.Links.UrlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.IO;

namespace Sitecore.DemoProject.MVC.Web.CustomCode
{
    public class CustomLinkProvider : ItemUrlBuilder
    {
        public CustomLinkProvider(DefaultItemUrlBuilderOptions option) : base(option)
        {

        }
        public override string Build(Sitecore.Data.Items.Item item, ItemUrlBuilderOptions options)
        {
            ///Books/2019/05/11/05/05/The-Last-Lecture
            ///Books/The-Last-Lecture
            if (BucketManager.IsItemContainedWithinBucket(item))
            {
                var bucketItem = item.GetParentBucketItemOrParent();
                if (bucketItem != null && bucketItem.IsABucket())//books item
                {
                    var bucketUrl = base.Build(bucketItem, options);
                    return FileUtil.MakePath(bucketUrl, item.Name.Replace(' ', '-'));
                }
            }
            return base.Build(item, options);
        }

    }
}