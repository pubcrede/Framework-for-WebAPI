//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using Genesys.Extras.Web.Http;

namespace Framework.WebServices
{
    /// <summary>
    /// Default Mvc controller
    /// </summary>
    public class HomeController : MvcController
    {
        public const string ControllerName = "Home";
        public const string IndexGetView = "Index";
        public const string IndexGetAction = "Index";
        public const string IndexPostAction = "IndexPost";
        public const string IndexPutAction = "IndexPut";
        public const string IndexDeleteAction = "IndexDelete";
        public const string AboutUsView = "About";
        public const string ContactUsView = "Contact";

        /// <summary>
        /// Default HttpGet route
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult Index()
        {
            return View(HomeController.IndexGetView);
        }

        /// <summary>
        /// Default HttpPost route
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult IndexPost()
        {
            return View(HomeController.IndexGetView);
        }

        /// <summary>
        /// Default HttpPut route
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public ActionResult IndexPut()
        {
            return View(HomeController.IndexGetView);
        }

        /// <summary>
        /// Default HttpDelete route
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public ActionResult IndexDelete()
        {
            return View(HomeController.IndexGetView);
        }

        /// <summary>
        /// About Us view
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult About()
        {
            return View(HomeController.AboutUsView);
        }

        /// <summary>
        /// Contact Us view
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult Contact()
        {
            return View(HomeController.ContactUsView);
        }
    }
}
