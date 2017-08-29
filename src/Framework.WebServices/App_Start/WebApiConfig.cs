//-----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Http;

namespace Framework.WebServices
{
    /// <summary>
    /// WebApiConfig
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register routes.
        /// </summary>
        /// <param name="config">config</param>6
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Route for WebApi's default /api/Controller. For familiarity with standard Web API.e
            config.Routes.MapHttpRoute(
                name: "DefaulApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // Route for /v1/Controller. Allows expansion through versioning /v1/.
            // Important: Default parameter "id" (above) changed to "key" (below)
            config.Routes.MapHttpRoute(
                name: "DefaultV1",
                routeTemplate: "v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            
            // Route for /v1/ (with no controller). Used to test is services is listening to any requests.
            config.Routes.MapHttpRoute(
                name: "DefaultV1Naked",
                routeTemplate: "v1",
                defaults: new { controller = HomeApiController.ControllerName, action = HomeApiController.IndexGetAction });
        }
    }
}
