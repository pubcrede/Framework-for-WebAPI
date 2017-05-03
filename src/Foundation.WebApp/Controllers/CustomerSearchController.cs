//-----------------------------------------------------------------------
// <copyright file="CustomerSearchController.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Web.Mvc;
using Genesys.Extensions;
using Genesys.Extras.Web.Http;
using Foundation.Entity;
using System.Collections.Generic;

namespace Foundation.WebApp
{
    /// <summary>
    /// Creates a Customer
    /// </summary>
    [Authorize]
    public class CustomerSearchController : MvcController
    {
        public const string ControllerName = "CustomerSearch";
        public const string SearchAction = "Search";
        public const string SearchView = "~/Views/CustomerSearch/CustomerSearch.cshtml";
        public const string SearchResultsAction = "SearchResults";
        public const string SearchResultsView = "~/Views/CustomerSearch/CustomerSearchResults.cshtml";

        /// <summary>
        /// Shows the search page
        /// </summary>
        /// <returns>Initialized search page</returns>
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult Search()
        {            
            return View(CustomerSearchController.SearchView, new CustomerSearchModel());
        }

        /// <summary>
        /// Performs a full-post back search, accepting search parameters and returning search parameters and results
        /// </summary>
        /// <param name="model">Model of type ICustomer with results</param>
        /// <returns>View of search parameters and any found results</returns>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult Search(CustomerSearchModel model)
        {
            IQueryable<CustomerInfo> searchResults;

            ModelState.Clear();
            searchResults = CustomerInfo.GetByAny(model).Take(25);
            if (searchResults.Any() == true)
            {
                model.Results.FillRange(searchResults.ToList());                
            } 
            else
            {
                ModelState.AddModelError("Result", "0 matches found");
            }

            return View(CustomerSearchController.SearchView, model);
        }

        /// <summary>
        /// Client-side version of search, refreshing only the results region
        /// </summary>
        /// <param name="id">ID to include in search results</param>
        /// <param name="firstName">Text to search in first name</param>
        /// <param name="lastName">Text to search in the last name field</param>
        /// <returns>Partial view of only the search results region</returns>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult SearchResults(string id, string firstName, string lastName)
        {
            Int32 idStrong = id.TryParseInt32();
            CustomerSearchModel model = new CustomerSearchModel() { ID = idStrong, FirstName = firstName, LastName = lastName };
            IQueryable<CustomerInfo> searchResults;

            ModelState.Clear();
            searchResults = CustomerInfo.GetByAny(model).Take(25);
            if (searchResults.Any() == true)
            {
                model.Results.FillRange(searchResults);
            } 
            else
            {
                ModelState.AddModelError("Result", "0 matches found");
            }

            return PartialView(CustomerSearchController.SearchResultsView, model.Results); // Return partial view for client-side to render
        }
    }
}