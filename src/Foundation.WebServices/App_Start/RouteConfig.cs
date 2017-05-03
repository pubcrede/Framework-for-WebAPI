//-----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;
using System.Web.Routing;

namespace Foundation.WebServices
{
    /// <summary>
    /// Config routes
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register Mvc routes
        /// </summary>
        /// <param name="routes">routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = HomeController.ControllerName, action = HomeController.IndexGetAction, id = UrlParameter.Optional }
            );
        }
    }
}
