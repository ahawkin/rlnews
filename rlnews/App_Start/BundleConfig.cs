using System.Web;
using System.Web.Optimization;

namespace rlnews
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Assets/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/js/bootstrap.min.js",
                      "~/Assets/js/respond.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Assets/css/bootstrap.css",
                      "~/Assets/css/styles.min.css"));

            bundles.Add(new StyleBundle("~/bundles/js").Include(
                      "~/Assets/js/app.min.js"));
        }
    }
}
