using System.Web;
using System.Web.Optimization;

namespace Hopnscotch.Portal.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/scripts/vendors").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/sammy-{version}.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/q.js",
                        "~/Scripts/breeze.debug.js",
                        "~/Scripts/toastr.js"
                        ));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/durandal.css",
                      "~/Content/toastr.css",
                      "~/Content/font-awesome.css",
                      "~/Content/main.css"));
        }
    }
}
