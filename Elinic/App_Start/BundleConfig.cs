using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Elinic
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254726
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                  "~/Scripts/WebForms/WebForms.js",
                  "~/Scripts/WebForms/WebUIValidation.js",
                  "~/Scripts/WebForms/MenuStandards.js",
                  "~/Scripts/WebForms/Focus.js",
                  "~/Scripts/WebForms/GridView.js",
                  "~/Scripts/WebForms/DetailsView.js",
                  "~/Scripts/WebForms/TreeView.js",
                  "~/Scripts/WebForms/WebParts.js"));

            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryScripts").Include(
               "~/Scripts/jquery.min.js",
               "~/Scripts/jquery.imagesloaded.js",
               "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.wookmark.min.js",
               "~/Scripts/s-gallery-master/hammer.js",
               "~/Scripts/s-gallery-master/plugins.js",
               "~/Scripts/s-gallery-master/screenfull.min.js",
               "~/Scripts/s-gallery-master/scripts.js",
               "~/Scripts/DataTables/jquery.dataTables.js"
               ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                 "~/Content/reset.css",
                 "~/Content/main.css",
                 "~/Content/styles.css",
                 "~/Content/DataTables/css/demo_table_jui.css",
                 "~/Content/themes/base/jquery.ui.all.css",
                 "~/Content/font-awesome.min.css",
                 "~/Content/bootstrap - responsive.css"));


            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
"~/Scripts/modernizr-*"));

            BundleTable.EnableOptimizations = true;
        }
    }
}