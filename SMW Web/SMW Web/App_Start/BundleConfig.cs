﻿using System.Web;
using System.Web.Optimization;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace SMW_Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));



            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            var bundleStyles = new StyleBundle("~/bundles/app_styles")
               .Include("~/bower_components/angular-notify/dist/angular-notify.min.css")
               .Include("~/bower_components/fontawesome/css/font-awesome.css")
               .Include("~/bower_components/metisMenu/dist/metisMenu.css")
               .Include("~/bower_components/animate.css/animate.css")
               .Include("~/bower_components/bootstrap/dist/css/bootstrap.css")
               .Include("~/bower_components/summernote/dist/summernote.css")
               .Include("~/bower_components/angular-ui-tree/dist/angular-ui-tree.min.css")
               .Include("~/bower_components/bootstrap-tour/build/css/bootstrap-tour.min.css")
               .Include("~/bower_components/datatables_plugins/integration/bootstrap/3/dataTables.bootstrap.css")
               .Include("~/bower_components/angular-xeditable/dist/css/xeditable.css")
               .Include("~/bower_components/ui-select/dist/select.min.css")
               .Include("~/bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css")
               .Include("~/bower_components/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css")
               .Include("~/bower_components/iCheck/skins/square/green.css")
               .Include("~/bower_components/iCheck/skins/square/purple.css")
               .Include("~/fonts/pe-icon-7-stroke/css/pe-icon-7-stroke.css")
               .Include("~/fonts/pe-icon-7-stroke/css/helper.css")
               .Include("~/styles/ng-table.min.css")
               .Include("~/app/styles/isteven-multi-select.css")
               .Include("~/styles/style.css");

             bundleStyles.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundleStyles);

            var bundle = new ScriptBundle("~/bundles/bower_components")
            //.Include("~/bower_components/jquery/dist/jquery.min.js")
            .Include("~/bower_components/jquery-ui/jquery-ui.min.js")
            .Include("~/bower_components/slimScroll/jquery.slimscroll.min.js")
            .Include("~/bower_components/angular/angular.min.js")
            .Include("~/bower_components/angular-sanitize/angular-sanitize.min.js")
            .Include("~/bower_components/angular-animate/angular-animate.min.js")
            .Include("~/bower_components/angular-ui-router/release/angular-ui-router.min.js")
            .Include("~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js")
            
            .Include("~/bower_components/jquery-flot/jquery.flot.js")
            .Include("~/bower_components/jquery-flot/jquery.flot.resize.js")
            .Include("~/bower_components/jquery-flot/jquery.flot.pie.js")
            .Include("~/bower_components/flot.curvedlines/curvedLines.js")
            .Include("~/bower_components/jquery.flot.spline/index.js")
            .Include("~/bower_components/angular-flot/angular-flot.js")
            .Include("~/bower_components/metisMenu/dist/metisMenu.min.js")

            .Include("~/bower_components/iCheck/icheck.min.js")
            .Include("~/bower_components/sparkline/index.js")
            .Include("~/bower_components/chartjs/Chart.min.js")
            .Include("~/bower_components/angles/angles.js")
            .Include("~/bower_components/peity/jquery.peity.min.js")
            .Include("~/bower_components/angular-peity/angular-peity.js")

            .Include("~/bower_components/angular-notify/dist/angular-notify.min.js")
            .Include("~/bower_components/chartjs/Chart.min.js")
            .Include("~/bower_components/angles/angles.js")
            .Include("~/bower_components/angular-ui-utils/ui-utils.js")
            .Include("~/bower_components/angular-ui-map/ui-map.js")
            .Include("~/bower_components/moment/min/moment.min.js")
            .Include("~/bower_components/summernote/dist/summernote.min.js")
            .Include("~/bower_components/angular-summernote/dist/angular-summernote.min.js")
            .Include("~/bower_components/angular-ui-tree/dist/angular-ui-tree.min.js")
            .Include("~/bower_components/bootstrap-tour/build/js/bootstrap-tour.min.js")
            .Include("~/bower_components/angular-bootstrap-tour/dist/angular-bootstrap-tour.min.js")
            .Include("~/bower_components/datatables/media/js/jquery.dataTables.min.js")
            .Include("~/bower_components/datatables_plugins/integration/bootstrap/3/dataTables.bootstrap.min.js")
            .Include("~/bower_components/angular-datatables/dist/angular-datatables.min.js")
            .Include("~/bower_components/angular-xeditable/dist/js/xeditable.min.js")
            .Include("~/bower_components/ui-select/dist/select.min.js")
            .Include("~/bower_components/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js")
            .Include("~/bower_components/angular-ui-sortable/sortable.min.js")
            .Include("~/bower_components/ng-table/ng-table.min.js")
            .Include("~/bower_components/ngStorage/ngStorage.min.js")
            .Include("~/js/angular-ui/ui-grid/ui-grid.min.js")
            .Include("~/bower_components/bootstrap/dist/js/bootstrap.min.js")
            .Include("~/bower_components/angular-route/angular-route.min.js");

            bundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundle);

            var bundleApp = new ScriptBundle("~/bundles/app_scripts")
                .Include("~/app/scripts/homer.js")
                .Include("~/app/scripts/filters/props.js")
                .Include("~/app/scripts/directives/checklist-model.js")


        .Include("~/app/scripts/filters/props.js")
.Include("~/app/scripts/directives/checklist-model.js")

.Include("~/app/scripts/controllers/template/main.js")
.Include("~/app/scripts/controllers/template/alerts.js")
.Include("~/app/scripts/controllers/template/modal.js")
.Include("~/app/scripts/controllers/template/charts_flot.js")
.Include("~/app/scripts/controllers/template/chartjs.js")
.Include("~/app/scripts/controllers/template/inline.js")
.Include("~/app/scripts/controllers/template/timeline.js")
.Include("~/app/scripts/controllers/template/list.js")
.Include("~/app/scripts/controllers/template/tour.js")
.Include("~/app/scripts/controllers/template/datatables.js")
.Include("~/app/scripts/controllers/template/wizard.js")
.Include("~/app/scripts/controllers/template/forms.js")
.Include("~/app/scripts/controllers/template/draggable.js")

.Include("~/app/scripts/factories/app-factory.js")
.Include("~/app/scripts/services/service.js")
.Include("~/app/scripts/controllers/modal.js")


.Include("~/app/scripts/controllers/dashboard.js")
 .Include("~/app/scripts/controllers/project.js")
 .Include("~/app/scripts/controllers/property.js")
 .Include("~/app/scripts/controllers/service.js")
 .Include("~/app/scripts/controllers/webquery.js")
 .Include("~/app/scripts/controllers/facility.js")
 .Include("~/app/scripts/controllers/footer.js")


.Include("~/app/scripts/controllers/application.js")
.Include("~/app/scripts/directives/touchSpin.js")
.Include("~/app/scripts/directives/angular-validation-match.js")

.Include("~/js/angular-bootstrap-nav-tree/abn_tree_directive.js")
.Include("~/js/angular-file-upload.min.js")
.Include("~/js/angular-images-resizer.js")
.Include("~/app/scripts/isteven-multi-select.js")


.Include("~/app/scripts/directives/valid-number.js")
.Include("~/app/scripts/controllers/search.js")


.Include("~/app/scripts/controllers/alerts.js");


            bundleApp.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundleApp);

            BundleTable.EnableOptimizations = Convert.ToBoolean(ConfigurationManager.AppSettings["BundleTable.EnableOptimizations"]);
        }
    }


    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
        
        
}