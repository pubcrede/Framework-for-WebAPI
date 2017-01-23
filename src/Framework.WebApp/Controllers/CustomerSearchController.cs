//-----------------------------------------------------------------------
// <copyright file="CustomerSearchController.cs" company="Genesys Source">
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Web.Mvc;
using Genesys.Extensions;
using Genesys.Extras.Web.Http;
using Framework.Entity;
using System.Collections.Generic;

namespace Framework.WebApp
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
        public const string SearchUriMask = "{0}/{1}/{2}?firstName={3}&lastName={4}";

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
            searchResults = CustomerInfo.GetBySearchFields(model); // Find matches based on all fields
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
            searchResults = CustomerInfo.GetBySearchFields(model); // Find matches based on all fields
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
