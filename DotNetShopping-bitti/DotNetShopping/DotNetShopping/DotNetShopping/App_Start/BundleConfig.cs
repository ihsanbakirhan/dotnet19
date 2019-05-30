﻿using System.Web;
using System.Web.Optimization;

namespace DotNetShopping
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include("~/Scripts/select2.min.js"));
            

             bundles.Add(new ScriptBundle("~/bundles/shopping").Include(
                      "~/Scripts/shopping.js"));

            bundles.Add(new StyleBundle("~/Content/Site/css").Include(
                      "~/Content/Bootstrap/bootstrap.css",
                      "~/Content/Site/Site.css",
                      "~/Content/css/select2.min.css",
                      "~/Content/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                      "~/Content/Bootstrap/bootstrap.css",
                      "~/Content/Admin/animate.min.css",
                      "~/Content/Admin/light-bootstrap-dashboard.css",
                      "~/Content/Admin/pe-icon-7-stroke.css",
                      "~/Content/Admin/Admin.css",
                      "~/Content/jquery-ui.css"));
        }
    }
}
