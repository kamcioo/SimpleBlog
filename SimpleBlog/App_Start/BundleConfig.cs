using System.Web;
using System.Web.Optimization;

namespace SimpleBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/confirmation").Include(
                "~/Scripts/confirmation.js"));

            bundles.Add(new StyleBundle("~/bundles/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/bundles/Admin/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/AdminPage.css",
                "~/Content/font-awesome.css",
                "~/Content/manage-users.css"));
            bundles.Add(new StyleBundle("~/bundles/DataTables/css").Include(
                "~/Content/dataTables.bootstrap.css",
                "~/Content/dataTables.responsive.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.responsive.js"));

            bundles.Add(new ScriptBundle("~/bundles/ChangeHeader").Include(
                      "~/Scripts/change-header-title.js"));
        }
    }
}
