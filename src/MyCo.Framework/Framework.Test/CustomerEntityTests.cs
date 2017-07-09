//-----------------------------------------------------------------------
// <copyright file="CustomerTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Extensions;
using Genesys.Extras.Mathematics;
using Framework.Entity;

namespace Framework.Tests
{
    /// <summary>
    /// Test framework functionality
    /// </summary>
    /// <remarks></remarks>
    [TestClass()]
    public class CustomerEntityTests
    {
        List<int> recycleBin = new List<int>();
        List<CustomerModel> customersFromScreen = new List<CustomerModel>()
        {
            new CustomerModel() {FirstName = "John", MiddleName = "Adam", LastName = "Doe", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Jane", MiddleName = "Michelle", LastName = "Smith", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Xi", MiddleName = "", LastName = "Ling", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Juan", MiddleName = "", LastName = "Gomez", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Maki", MiddleName = "", LastName = "Ishii", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) }
        };

        /// <summary>
        /// Customer_Entity_CustomerNew
        /// </summary>
        /// <remarks></remarks>
        public void Customer_Entity_CustomerNew()
        {
            CustomerInfo newCustomer = new CustomerInfo();
            int newID = TypeExtension.DefaultInteger;
            Guid newKey = TypeExtension.DefaultGuid;

            // Simulate the service layer transforming the Model (CustomerModel) to the Data Access Object (CustomerInfo)
            newCustomer.Fill(customersFromScreen[Arithmetic.Random(1, 5)]);
            newCustomer.Save();
            newID = newCustomer.ID;
            newKey = newCustomer.Key;

            Assert.IsTrue(newCustomer.IsNew == false, "Customer did not save.");
            Assert.IsTrue(newID != TypeExtension.DefaultInteger, "Customer did not save.");
            Assert.IsTrue(newKey != TypeExtension.DefaultGuid, "Customer did not save.");

            // Inserted records must be added to recycle bin for cleanup
            recycleBin.Add(newCustomer.ID);
        }

        /// <summary>
        /// Customer_Entity_CustomerEdit
        /// </summary>
        /// <remarks></remarks>
        public void Customer_Entity_CustomerEdit()
        {
            CustomerInfo newCustomer = new CustomerInfo();
            int newID = TypeExtension.DefaultInteger;
            Guid newKey = TypeExtension.DefaultGuid;

            // Simulate the service layer transforming the Model (CustomerModel) to the Data Access Object (CustomerInfo)
            newCustomer.Fill(customersFromScreen[Arithmetic.Random(1, 5)]);
            newCustomer.Save();
            newID = newCustomer.ID;
            newKey = newCustomer.Key;

            Assert.IsTrue(newCustomer.IsNew == false, "Customer did not save.");
            Assert.IsTrue(newID != TypeExtension.DefaultInteger, "Customer did not save.");
            Assert.IsTrue(newKey != TypeExtension.DefaultGuid, "Customer did not save.");
            Assert.IsTrue(newCustomer.ID != TypeExtension.DefaultInteger, "Customer did not save.");
        }

        /// <summary>
        /// Customer_Entity_CustomerDisplay
        /// </summary>
        /// <remarks></remarks>
        public void Customer_Entity_CustomerGet()
        {
            CustomerInfo newCustomer = new CustomerInfo();
            int toDeleteID = TypeExtension.DefaultInteger;

            toDeleteID = recycleBin.Count() > 0 ? recycleBin[0] : CustomerInfo.GetAll().Take(1).FirstOrDefaultSafe().ID;
            newCustomer = CustomerInfo.GetByID(toDeleteID);
            newCustomer.Delete();
            Assert.IsTrue(newCustomer.ID != TypeExtension.DefaultInteger, "Customer didnt save.");
        }

        /// <summary>
        /// Customer_Entity_CustomerDelete
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public void Customer_Entity_CustomerDelete()
        {
            CustomerInfo customer = new CustomerInfo();
            int id = TypeExtension.DefaultInteger;

            // Simulate the service layer transforming the Model (CustomerModel) to the Data Access Object (CustomerInfo)
            customer.Fill(customersFromScreen[0]);
            id = customer.ID;
            customer.Delete();
            customer = CustomerInfo.GetByID(id);
            Assert.IsTrue(customer.ID == TypeExtension.DefaultInteger, "Customer didnt delete.");

            // Add to recycle bin for cleanup
            recycleBin.Add(customer.ID);
        }

        /// <summary>
        /// Customer_Entity_CustomerTests
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public void Customer_Entity_CustomerTypeGet()
        {
            IQueryable<CustomerType> types = CustomerType.GetAll();
            Assert.IsTrue(types.Any() == true, "Did not work");
        }

        /// <summary>
        /// Cleanup all data
        /// </summary>
        [ClassCleanupAttribute()]
        private void Cleanup()
        {
            foreach (int item in recycleBin)
            {
                CustomerInfo.GetByID(item).Delete();
            }
        }
    }
}
