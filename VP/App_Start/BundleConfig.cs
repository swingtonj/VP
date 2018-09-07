using System.Web;
using System.Web.Optimization;

namespace VP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Assets/js/jquery-3.3.1.min.js",
                "~/Assets/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Assets/css/bootstrap.min.css",
                "~/Assets/css/font-awesome.min.css",
                      "~/Assets/css/master.min.css"));
        }
    }
}
