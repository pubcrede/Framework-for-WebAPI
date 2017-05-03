//-----------------------------------------------------------------------
// <copyright file="CustomerController.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Foundation.Entity;
using Genesys.Extensions;
using Genesys.Extras.Web.Http;
using Genesys.Foundation.Data;
using System.Web.Mvc;

namespace Foundation.WebApp
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
            var reader = ReadOnlyDatabase<CustomerInfo>.Construct();
            var model = new CustomerModel();
            model.Fill(reader.GetByID(id.TryParseInt32()));
            if (model.ID == TypeExtension.DefaultInteger)
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
            var reader = ReadOnlyDatabase<CustomerInfo>.Construct();                  
            model.Fill(reader.GetByID(model.ID));
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
            var customer = new CustomerInfo();
            
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
            var reader = ReadOnlyDatabase<CustomerInfo>.Construct();
            var model = new CustomerModel();
            model.Fill(reader.GetByID(id.TryParseInt32()));
            if (model.ID == TypeExtension.DefaultInteger)
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
            var reader = ReadOnlyDatabase<CustomerInfo>.Construct();
            var customer = new CustomerInfo();
            
            customer = reader.GetByID(model.ID);
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
            var reader = ReadOnlyDatabase<CustomerInfo>.Construct();
            var model = new CustomerModel();
            model.Fill(reader.GetByID(id.TryParseInt32()));
            if (model.ID == TypeExtension.DefaultInteger)
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
            var reader = ReadOnlyDatabase<CustomerInfo>.Construct();
            var customer = new CustomerInfo();

            customer = reader.GetByID(model.ID);
            customer.Delete();
            customer = reader.GetByID(model.ID); // Verify delete, success returns empty object
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