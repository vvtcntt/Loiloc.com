using Loiloc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Loiloc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Menufactures", "{tag}-mm", new { controller = "Menufactures", action = "ManufacturesDetail", tag = UrlParameter.Optional }, new { controller = "^M.*", action = "^ManufacturesDetail$" });

            routes.MapRoute("productDetail", "{tag}.htm", new { controller = "product", action = "productDetail", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productDetail$" });
             routes.MapRoute("ListProduct", "{tag}.html", new { controller = "product", action = "productList", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productList$" });
            routes.MapRoute("capacitys", "loi-loc-nuoc/{tag}", new { controller = "product", action = "capacityList", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^capacityList$" });
            routes.MapRoute("dai-ly", "dai-ly/{tag}", new { controller = "agency", action = "agencyDetail", tag = UrlParameter.Optional }, new { controller = "^a.*", action = "^agencyDetail$" });

            routes.MapRoute(name: "bao-gia-loi", url: "bao-gia", defaults: new { controller = "baogia", action = "Index" });

            routes.MapRoute("Bao-gia", "Bao-gia/{tag}", new { controller = "baogia", action = "baoGiaList", tag = UrlParameter.Optional }, new { controller = "^b.*", action = "^baoGiaList$" });
            routes.MapRoute("Dich-vu", "Dich-vu/{tag}", new { controller = "services", action = "servicesDetail", tag = UrlParameter.Optional }, new { controller = "^s.*", action = "^servicesDetail$" });

            routes.MapRoute("Bao-hanh", "bao-hanh/{tag}", new { controller = "Guarantee", action = "DetailGuarantee", tag = UrlParameter.Optional }, new { controller = "^G.*", action = "^DetailGuarantee$" });
            routes.MapRoute("Dia-chi-Bao-hanh", "Dia-chi-Bao-hanh/{tag}", new { controller = "Guarantee", action = "ListGuarantee", tag = UrlParameter.Optional }, new { controller = "^G.*", action = "^ListGuarantee$" });

            routes.MapRoute("Tag-bao-hanh", "TagGuarantee/{tag}", new { controller = "Guarantee", action = "TagGuarantee", tag = UrlParameter.Optional }, new { controller = "^G.*", action = "^TagGuarantee$" });
            routes.MapRoute(name: "Gioi-thieu", url: "gioi-thieu", defaults: new { controller = "news", action = "introduction" });
            routes.MapRoute(name: "Dai-ly-loi-loc-nuoc", url: "Dai-ly-loi-loc-nuoc", defaults: new { controller = "agency", action = "listAgency" });
            routes.MapRoute("tagsAgency", "tagsAgency/{tag}", new { controller = "agency", action = "tagAgency", tag = UrlParameter.Optional }, new { controller = "^a.*", action = "^tagAgency$" });

            routes.MapRoute("Tag", "tags/{tag}", new { controller = "product", action = "productTag", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productTag$" });
            routes.MapRoute("TagNew", "tagsnews/{tag}", new { controller = "news", action = "tagNews", tag = UrlParameter.Optional }, new { controller = "^n.*", action = "^tagNews$" });
            routes.MapRoute(
    name: "CmsRoute",
    url: "{*tag}",
    defaults: new { controller = "news", action = "listNews" },
    constraints: new { tag = new CmsUrlConstraint() }
);
            routes.MapRoute("Chi-tiet-tin", "Tin-tuc/{tag}", new { controller = "News", action = "NewsDetail", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^NewsDetail$" });
            routes.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Login", action = "LoginIndex" });
            routes.MapRoute(name: "Gio-hang", url: "Gio-hang", defaults: new { controller = "Order", action = "OrderIndex" });
            routes.MapRoute(name: "Lien-he", url: "Lien-he", defaults: new { controller = "contact", action = "Index" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
