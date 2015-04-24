using System.Web;
using System.Web.Optimization;

namespace ParticipantsOfWar
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-aria.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-highcharts.js",
                "~/Scripts/i18n/angular-locale_ru-ru.js",
                "~/Scripts/angular-material.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/angular-ui/ui-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/pow_app").Include(
                "~/App_Front/pow_app.js")
                .IncludeDirectory("~/App_Front/controllers", "*.js")
                .IncludeDirectory("~/App_Front/services", "*.js")
                .IncludeDirectory("~/App_Front/directives", "*.js")
                .IncludeDirectory("~/App_Front/filters", "*.js")
                .IncludeDirectory("~/App_Front/viewmodels", "*.js")
                );


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-material/angular-material.css",
                      "~/Content/site.css"));
        }
    }
}
