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

            bundles.Add(new ScriptBundle("~/bundles/jsPDF").Include(
                        "~/Scripts/jsPDF/jspdf.min.js",
                        "~/Scripts/jsPDF/jspdf.plugin.autotable.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/userscripts").Include(
                        "~/Scripts/AddDeleteList.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/BootstrapToggle/bootstrap4-toggle.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2").Include(
                      "~/Scripts/SweetAlert2/sweetalert2.all.js",
                      "~/Scripts/SweetAlert2/sweetalert2.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                      "~/Scripts/Select2/select2.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/SweetAlert2/sweetalert2.css",
                      "~/Content/Select2/select2.css",
                      "~/Content/FontAwesome5/font-awesome.css",
                      "~/Content/BootstrapToggle/bootstrap4-toggle.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Error").Include(
          "~/Content/Error/Error.css"));

            bundles.Add(new StyleBundle("~/Content/tiles").Include(
                      "~/Content/CustomWidgetsCSS/Tiles.css"));
        }
    }
}
