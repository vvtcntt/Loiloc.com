using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Loiloc.Models;
namespace Loiloc.Models
{
    public class CmsUrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,  RouteValueDictionary values, RouteDirection routeDirection)
        {
            var db = new Loiloc.Models.LoilocContext();
             if (values[parameterName] != null)
            {
                var tag = values[parameterName].ToString();
                 return db.tblGroupNews.Any(p => p.Tag == tag);
            }
            return false;
        }
    }
}