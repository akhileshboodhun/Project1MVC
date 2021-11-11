using System.Web;
using System.Web.Optimization;

namespace Project1MVC
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

            bundles.Add(new ScriptBundle("~/bundles/ParticlesJS").Include(
                        "~/Scripts/ParticlesJS/particles*"));




            bundles.Add(new ScriptBundle("~/bundles/userscripts").Include(
                        "~/Scripts/AddDeleteList.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap4-toggle.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2").Include(
                      "~/Scripts/sweetalert2.all.js",
                      "~/Scripts/sweetalert2.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap4-toggle.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Error").Include(
                      "~/Content/Error/Error.css"));

        }
    }
}
