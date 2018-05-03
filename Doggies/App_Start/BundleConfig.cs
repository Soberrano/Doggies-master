using System.Web;
using System.Web.Optimization;

namespace Doggies
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/bootstrap.js"
                        , "~/Scripts/respond.js"
                        , "~/External/angular.js"
                        , "~/External/angular-aria.js"
                        , "~/External/angular-locale_ru-ru.js"
                        , "~/External/angular-message-format.js"
                        , "~/External/angular-messages.js"
                        , "~/External/angular-route.js"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/applications").Include(

                        "~/Scripts/app.js"

                        , "~/Scripts/Modules/httpRequest.js"
                        , "~/Scripts/Modules/pageLoader.js"
                        , "~/Scripts/Modules/requestPromise.js"
                        , "~/Scripts/Modules/security.js"
                        , "~/Scripts/Modules/tools.js"

                        , "~/Scripts/Services/reservationService.js"

                        , "~/Scripts/Controllers/SecurityCtrl.js"
                        , "~/Scripts/Controllers/RedirectCtrl.js"
                        , "~/Scripts/Controllers/MainCtrl.js"
                        , "~/Scripts/Controllers/HallCtrl.js"
                        , "~/Scripts/Controllers/TeachersCtrl.js"
                        , "~/Scripts/Controllers/FilterCtrl.js"
                        , "~/Scripts/Controllers/DogCtrl.js"

                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
