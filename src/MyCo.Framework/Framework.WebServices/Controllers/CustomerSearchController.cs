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
using System.Web.Mvc;
using Genesys.Extensions;
using Genesys.Extras.Web.Http;
using Framework.Entity;
using System.Linq;

namespace Framework.WebServices
{
    /// <summary>
    /// Searches for customer records
    /// </summary>
    public class CustomerSearchController : WebApiController
    {
        public const string ControllerName = "CustomerSearch";
        public const string GetActionName = "Search";
        public const string GetAction = "Get";
        public const string PostAction = "Post";
        public const string SearchUriMask = "{0}/{1}/{2}?firstName={3}&lastName={4}";

        /// <summary>
        /// Parameterized HttpGet search, refreshing only the results region
        /// </summary>
        /// <param name="id">ID to include in search results</param>
        /// <param name="firstName">Text to search in first name</param>
        /// <param name="lastName">Text to search in the last name field</param>
        /// <returns>Partial view of only the search results region</returns>
        [HttpGet()]
        public CustomerSearchModel Get(string id, string firstName, string lastName)
        {
            Int32 idStrong = id.TryParseInt32();
            CustomerSearchModel model = new CustomerSearchModel() { ID = idStrong, FirstName = firstName, LastName = lastName };
            IQueryable<CustomerInfo> searchResults;

            searchResults = CustomerInfo.GetBySearchFields(model); // Find matches based on all fields
            if (searchResults.Any() == true)
            {
                model.Results.FillRange(searchResults);
            }

            return model;
        }

        /// <summary>
        /// Performs a full HttpPost search, accepting search parameters and returning search parameters and results
        /// </summary>
        /// <param name="model">Model of type ICustomer with results list</param>
        /// <returns>JSON of search parameters and any found results</returns>
        [HttpPost()]
        public CustomerSearchModel Post(CustomerSearchModel model)
        {
            IQueryable<CustomerInfo> searchResults;

            searchResults = CustomerInfo.GetBySearchFields(model); // Find matches based on all fields
            if (searchResults.Any() == true)
            {
                model.Results.FillRange(searchResults.ToList());
            }

            return model;
        }
    }
}