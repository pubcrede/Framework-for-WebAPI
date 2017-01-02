//-----------------------------------------------------------------------
// <copyright file="CustomerController.cs" company="Genesys Source">
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
    public class CustomerController : MvcController
    {
        public const string ControllerName = "Customer";
        public const string SummaryAction = "Summary";
        public const string SummaryView = "~/Views/Customer/CustomerSummary.cshtml";
        public const string CreateAction = "Create";
        public const string CreateView = "~/Views/Customer/CustomerCreate.cshtml";
        public const string EditAction = "Edit";
        public const string EditView = "~/Views/Customer/CustomerEdit.cshtml";
        public const string DeleteAction = "Delete";
        public const string DeleteView = "~/Views/Customer/CustomerDelete.cshtml";

        /// <summary>
        /// Displays entity
        /// </summary>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Summary(string id)
        {
            CustomerInfo customer = new CustomerInfo();
            CustomerModel model = new CustomerModel();

            customer = CustomerInfo.GetByID(id.TryParseInt32());            
            if (customer.ID != TypeExtension.DefaultInteger)
            {
                model.Fill(customer); // Fill the CustomerModel view model, so the class can be specific to the screen's needs and drop the heavy data access items.
            } else
            {
                ModelState.AddModelError("", "No customer found");
            }
            return View(CustomerController.SummaryView, model);
        }

        /// <summary>
        /// Customer Summary with Edit/Delete functionality
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult Summary(CustomerModel model)
        {
            CustomerInfo customer = new CustomerInfo();
            
            customer = CustomerInfo.GetByID(model.ID);            
            model.Fill(customer);

            return View(CustomerController.EditView, model);
        }
        /// <summary>
        /// Displays entity for editing
        /// </summary>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult Create()
        {
            return View(CustomerController.CreateView, new CustomerModel());
        }

        /// <summary>
        /// Saves changes to a Customer
        /// </summary>
        /// <param name="model">Full customer model worth of data to be saved</param>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult Create(CustomerModel model)
        {
            CustomerInfo customer = new CustomerInfo();
            
            customer.Fill(model);            
            customer.Save(); // Save screen changes to database.
            model.Fill(customer);  // Fill the CustomerModel view model, so the class can be specific to the screen's needs and drop the heavy data access items.

            return View(CustomerController.SummaryView, model);
        }

        /// <summary>
        /// Displays entity for editing
        /// </summary>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult Edit(string id)
        {
            CustomerInfo customer = new CustomerInfo();
            CustomerModel model = new CustomerModel();

            customer = CustomerInfo.GetByID(id.TryParseInt32());
            if (customer.ID != TypeExtension.DefaultInteger)
            {
                model.Fill(customer); // Fill the CustomerModel view model, so the class can be specific to the screen's needs and drop the heavy data access items.
            } else
            {
                ModelState.AddModelError("", "No customer found");
            }

            return View(CustomerController.EditView, model);
        }

        /// <summary>
        /// Saves changes to a Customer
        /// </summary>
        /// <param name="model">Full customer model worth of data with user changes</param>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult Edit(CustomerModel model)
        {
            CustomerInfo customer = new CustomerInfo();
            
            customer = CustomerInfo.GetByID(model.ID);
            customer.Fill(model); // Overlay all screen edits on-top of the data-access-object, to preserve untouched original data
            customer.Save();
            model.Fill(customer); // Go back to screen model for ui-specific functionality to be available to view/page

            return View(CustomerController.SummaryView, model);
        }

        /// <summary>
        /// Displays entity for deleting
        /// </summary>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult Delete(string id)
        {
            CustomerInfo customer = new CustomerInfo();
            CustomerModel model = new CustomerModel();

            customer = CustomerInfo.GetByID(id.TryParseInt32());            
            if (customer.ID != TypeExtension.DefaultInteger)
            {
                model.Fill(customer); // Fill the CustomerModel view model, so the class can be specific to the screen's needs and drop the heavy data access items.
            } 
            else
            {
                ModelState.AddModelError("", "No customer found");                
            }

            return View(CustomerController.DeleteView, model);
        }

        /// <summary>
        /// Deletes a customer by key
        /// </summary>
        /// <param name="model">Customer to delete</param>
        /// <returns>View rendered with model data</returns>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult Delete(CustomerModel model)
        {
            CustomerInfo customer = new CustomerInfo();

            customer = CustomerInfo.GetByID(model.ID);
            customer.Delete();
            customer = CustomerInfo.GetByID(model.ID);
            if (customer.ID == TypeExtension.DefaultInteger)
            {
                model.Fill(customer); // Fill the CustomerModel view model, so the class can be specific to the screen's needs and drop the heavy data access items.
                ModelState.AddModelError("", "Successfully deleted");
            } 
            else
            {
                ModelState.AddModelError("", "Failed to delete");
            }

            return View(CustomerController.DeleteView, model);
        }
    }
}
