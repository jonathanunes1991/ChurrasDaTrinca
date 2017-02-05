using System.Web;
using System.Web.Optimization;

namespace ChurrasDaTrinca.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/material.min.js",
                        "~/Scripts/chartist.min.js",
                        "~/Scripts/bootstrap-notify.js",
                        "~/Scripts/material-dashboard.js",
                        "~/Scripts/demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jQueryFixes.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-datepicker.pt-BR.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/material-dashboard.css",
                      "~/Content/css/demo.css",
                      "~/Content/css/custom.css",
                      "~/Content/css/datepicker/bootstrap-datepicker.css",
                      "~/Content/css/datepicker/bootstrap-datepicker.standalone.css",
                      "~/Content/css/datepicker/bootstrap-datepicker3.css",
                      "~/Content/css/datepicker/bootstrap-datepicker3.standalone.css"));
        }
    }
}
