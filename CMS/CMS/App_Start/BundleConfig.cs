using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CMS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
           .Include("~/Scripts/jquery.unobtrusive*","~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
           .Include("~/Scripts/jquery-{version}.js"));
        }
    }
}