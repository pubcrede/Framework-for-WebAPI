//-----------------------------------------------------------------------
// <copyright file="CustomerTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
// 
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
        [TestMethod()]
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
        [TestMethod()]
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
        [TestMethod()]
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
            CustomerInfo newCustomer = new CustomerInfo();

            // Simulate the service layer transforming the Model (CustomerModel) to the Data Access Object (CustomerInfo)
            newCustomer.Fill(customersFromScreen[0]);
            newCustomer.Save();
            Assert.IsTrue(newCustomer.ID != TypeExtension.DefaultInteger, "Customer didnt save.");

            // Add to recycle bin for cleanup
            recycleBin.Add(newCustomer.ID);
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
