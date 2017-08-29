//-----------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System.Web.Mvc;

namespace Framework.WebServices
{
    /// <summary>
    /// Adds filters to the Http pipeline
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers global filters at startup
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
