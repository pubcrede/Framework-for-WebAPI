//-----------------------------------------------------------------------
// <copyright file="HomeApiController.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Http;
using Genesys.Extras.Web.Http;

namespace Framework.WebServices
{
    /// <summary>
    /// Default WebApi controller
    /// </summary>
    public class HomeApiController : WebApiController
    {
        public const string ControllerName = "HomeApi";
        public const string IndexGetAction = "Index";
        public const string IndexPutAction = "IndexPut";
        public const string IndexPostAction = "IndexPost";
        public const string IndexDeleteAction = "IndexDelete";

        /// <summary>
        /// Default HttpGet route
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public string Index()
        {
            return String.Format("{0}{1}", WebApiController.MessageUpAndRunning, "Index() Get Method. Parameterless.");
        }

        /// <summary>
        /// Default HttpGet route with parameter
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public string Index(string id)
        {
            return String.Format("{0}{1}{2}", WebApiController.MessageUpAndRunning, "Index(string id) Get Method. id: ", id ?? "Null");
        }

        /// <summary>
        /// Default HttpPost route
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public string IndexPost()
        {
            return WebApiController.MessageUpAndRunning;
        }

        /// <summary>
        /// Default HttpPut route
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public string IndexPut()
        {
            return WebApiController.MessageUpAndRunning;
        }

        /// <summary>
        /// Default HttpDelete route
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public string IndexDelete()
        {
            return WebApiController.MessageUpAndRunning;
        }
    }
}
