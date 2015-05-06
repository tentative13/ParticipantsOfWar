using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
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
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.signalR-2.2.0.min.js",
                        "~/Scripts/jquery-ui.js"
                        ));

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
                "~/Scripts/angular-ui/angular-ui-date/date.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/pow_app")
                //.Include(
                //"~/App_Front/pow_app.js")
              .IncludeDirectory("~/App_Front", "*.js", true)
                //.IncludeDirectory("~/App_Front/services", "*.js")
                //.IncludeDirectory("~/App_Front/directives", "*.js")
                //.IncludeDirectory("~/App_Front/filters", "*.js")
                //.IncludeDirectory("~/App_Front/viewmodels", "*.js")
              );


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/angular-material/angular-material.css",
                      "~/Content/site.css"));

            // Enable optimisation based on web.config setting
            BundleTable.EnableOptimizations = bool.Parse(ConfigurationManager.AppSettings["BundleOptimisation"]);

            bundles.Add(new PartialsBundle("pow_app", "~/bundles/partials")
            .IncludeDirectory("~/App_Front/views", "*.html", true));

        }
    }


    public class PartialsTransform : IBundleTransform
    {
        private readonly string _moduleName;

        public PartialsTransform(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (response == null)
                throw new ArgumentNullException("response");

            if (string.IsNullOrWhiteSpace(_moduleName))
            {
                response.Content = "// No or wrong app name defined";
                response.ContentType = "text/javascript";
                return;
            }

            var strBundleResponse = new StringBuilder();
            // Javascript module for Angular that uses templateCache
            strBundleResponse.Append("(function(){");
            strBundleResponse.AppendFormat(
                @"angular.module('{0}').run(['$templateCache',function(t){{",
                _moduleName);

            foreach (var file in response.Files)
            {
                string fileId = VirtualPathUtility.ToAbsolute(file.IncludedVirtualPath);
                string filePath = HttpContext.Current.Server.MapPath(file.IncludedVirtualPath);
                string fileContent = File.ReadAllText(filePath);
                strBundleResponse.AppendFormat("t.put({0},{1});",
                        JsonConvert.SerializeObject(fileId),
                        JsonConvert.SerializeObject(fileContent));
            }
            strBundleResponse.Append(@"}]);");
            strBundleResponse.Append("})();");

            response.Files = new BundleFile[] { };
            response.Content = strBundleResponse.ToString();
            response.ContentType = "text/javascript";
        }
    }

    public class PartialsBundle : Bundle
    {
        public PartialsBundle(string moduleName, string virtualPath)
            : base(virtualPath, new[] { (IBundleTransform)new PartialsTransform(moduleName) })
        {
        }
    }
}
