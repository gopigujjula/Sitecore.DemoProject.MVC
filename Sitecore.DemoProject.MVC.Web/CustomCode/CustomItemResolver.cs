using Sitecore.Buckets.Managers;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.DemoProject.MVC.Web.CustomCode
{
    public class CustomItemResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
           if(Context.Item==null)
            {
                var requestUrl = args.Url.ItemPath;
                ///Books/The-Last-Lecture
                var index = requestUrl.LastIndexOf('/');
                if (index > 0)
                {
                    var bucketPath = requestUrl.Substring(0, index);
                    var bucketItem = args.GetItem(bucketPath);
                    if (bucketItem != null && BucketManager.IsBucket(bucketItem))
                    {
                        var itemName = requestUrl.Substring(index + 1).Replace("-", " ");

                        using (var searchContext = ContentSearchManager.GetIndex("sitecore_web_index")
                            .CreateSearchContext())
                        {
                            var result = searchContext.GetQueryable<SearchResultItem>().
                                Where(x => x.Name == itemName 
                                && x.TemplateId == new Data.ID("{C77CBF36-E660-4C1A-AB94-85F172CA180A}"))
                                .FirstOrDefault();
                            if (result != null)
                                Context.Item = result.GetItem();
                        }
                    }
                }
            }
        }
    }
}