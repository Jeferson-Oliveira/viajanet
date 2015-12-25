using System.Web;
using System.Web.Optimization;

namespace viajanet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
        
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // css basic files
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/assets/css/styles.css",
                "~/Content/assets/plugins/jstree/dist/themes/avenger/style.min.css",
                "~/Content/assets/plugins/codeprettifier/prettify.css",
                "~/Content/assets/plugins/iCheck/skins/minimal/blue.css",
                "~/Content/assets/fonts/font-awesome/css/font-awesome.min.css",
                "~/Content/assets/plugins/datatables/dataTables.bootstrap.css",
                "~/Content/assets/plugins/datatables/dataTables.fontAwesome.css"
                ));
            // js básic files

            bundles.Add(new ScriptBundle("~/Content/assets/js").Include(
               "~/Content/assets/js/enquire.min.js",
               "~/Content/assets/js/application.js"
               ));


            // jquery files
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/currency.js",// fix to values with comma
                        "~/Scripts/jquery.mask.min.js")); // masks

            // bootstrap js files

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js"));




            // plugins the avenger theme
            bundles.Add(new ScriptBundle("~/Content/assets/plugins").Include(
                "~/Content/assets/plugins/easypiechart/jquery.easypiechart.js",
                "~/Content/assets/plugins/sparklines/jquery.sparklines.min.js",
                "~/Content/assets/plugins/jstree/dist/jstree.min.js",
                "~/Content/assets/plugins/codeprettifier/prettify.js",
                "~/Content/assets/plugins/bootstrap-switch/bootstrap-switch.js",
                "~/Content/assets/plugins/bootstrap-tabdrop/js/bootstrap-tabdrop.js",
                "~/Content/assets/plugins/iCheck/icheck.min.js",
                "~/Content/assets/plugins/simpleWeather/jquery.simpleWeather.min.js",
                "~/Content/assets/plugins/nanoScroller/js/jquery.nanoscroller.min.js",
                "~/Content/assets/plugins/jquery-mousewheel/jquery.mousewheel.min.js",
                "~/Content/assets/plugins/bootbox/bootbox.js",
                "~/Content/assets/plugins/datatables/jquery.dataTables.js",
                "~/Content/assets/plugins/datatables/dataTables.bootstrap.js",
                "~/Content/assets/demo/demo-datatables.js"
                ));

        }
    }
}
