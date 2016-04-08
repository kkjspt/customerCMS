using System.Web;
using System.Web.Optimization;

namespace CustomerCMS
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      //"~/Scripts/respond.js",
                      "~/Scripts/typeahead-bs2.min.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/excanvas").Include(
                      "~/Scripts/excanvas.min.js"
                     ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-ui-1.10.3.custom.min.js",
                      "~/Scripts/jquery.ui.touch-punch.min.js",
                      "~/Scripts/jquery.slimscroll.min.js",
                      "~/Scripts/jquery.easy-pie-chart.min.js",
                      "~/Scripts/jquery.sparkline.min.js",
                      "~/Scripts/jquery.flot.min.js",
                      "~/Scripts/jquery.flot.pie.min.js",
                      "~/Scripts/jquery.flot.resize.min.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/ace-elements").Include(
                      "~/Scripts/ace-elements.min.js",
                      "~/Scripts/ace.min.js"
                     ));

           bundles.Add(new ScriptBundle("~/bundles/ace-extra").Include(
                      "~/Scripts/ace-extra.min.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/html5repond").Include(
                      "~/Scripts/html5shiv.js",
                      "~/Scripts/respond.min.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css/ie7").Include(
                      "~/Content/font-awesome-ie7.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css/ace").Include(
                      "~/Content/ace.min.css",
                      "~/Content/ace-rtl.min.css",
                      "~/Content/ace-skins.min.css"
                     ));
            bundles.Add(new StyleBundle("~/Content/css/ace/ie").Include(
                        "~/Content/ace-ie.min.css"
                    ));



        }
    }
}
