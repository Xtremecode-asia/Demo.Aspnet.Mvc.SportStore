using System.Web.Mvc;
using System.Web.Routing;

namespace Demo.Aspnet.Mvc.SportStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            // Url: / 
            routes.MapRoute( null, string.Empty, new 
                {
                    controller = "Product", action ="List", category = null as string, pageIndex = 1
                });

            // Url: /Page2
            routes.MapRoute(null, "Page{pageIndex}", new
                {
                    controller= "Product", action="List", category= null as string, pageIndex = @"\d+" 
                });

            // Url: /Soccer
            routes.MapRoute(null, "{category}", new
                {
                    controller = "Product", action="List", pageIndex = 1
                });

            // Url: /Soccer/Page2
            routes.MapRoute(null, "{category}/Page{pageIndex}", new
            {
                controller = "Product",
                action = "List",
                pageIndex = @"\d+"
            });
            
            // Url: /Anything/Else
            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}