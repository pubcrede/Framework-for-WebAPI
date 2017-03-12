//-----------------------------------------------------------------------
// <copyright file="CustomerCloudTests.cs" company="Genesys Source">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Genesys.Extensions;
using Framework.Entity;
using Genesys.Extras.Mathematics;
using Genesys.Extras.Net;
using Genesys.Extras.Configuration;
using System.Threading.Tasks;

namespace Framework.Tests
{
    /// <summary>
    /// Test framework functionality
    /// </summary>
    /// <remarks></remarks>
    [TestClass()]
    public class CustomerCloudTests
    {
        List<int> recycleBin = new List<int>();
        List<CustomerModel> customerTestData = new List<CustomerModel>()
        {
            new CustomerModel() {FirstName = "John", MiddleName = "Adam", LastName = "Doe", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Jane", MiddleName = "Michelle", LastName = "Smith", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Xi", MiddleName = "", LastName = "Ling", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Juan", MiddleName = "", LastName = "Gomez", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) },
            new CustomerModel() {FirstName = "Maki", MiddleName = "", LastName = "Ishii", BirthDate = DateTime.Today.AddYears(Arithmetic.Random(2).Negate()) }
        };

        /// <summary>
        /// Create a new customer in the cloud
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Customer_Cloud_CustomerPut()
        {
            CustomerModel customerToCreate = new CustomerModel();
            string urlRoot = new ConfigurationManagerFull().AppSettingValue("MyWebService");
            string urlFull = String.Format("{0}/{1}", urlRoot, "Customer");
            HttpRequestPut<CustomerModel> request;

            // Simulate the service layer transforming the Model (CustomerModel) to the Data Access Object (CustomerInfo)
            customerToCreate.Fill(customerTestData[Arithmetic.Random(1, customerTestData.Count)]);
            // Call the cloud and get results
            request = new HttpRequestPut<CustomerModel>(urlFull, customerToCreate);
            customerToCreate = await request.SendAsync();
            Assert.IsTrue(customerToCreate.ID != TypeExtension.DefaultInteger, "Customer did not save.");
            Assert.IsTrue(customerToCreate.Key != TypeExtension.DefaultGuid, "Customer did not save.");

            // Inserted records must be added to recycle bin for cleanup
            recycleBin.Add(customerToCreate.ID);
        }

        /// <summary>
        /// Edit a customer in the cloud
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Customer_Cloud_CustomerPost()
        {
            CustomerModel responseData = new CustomerModel();
            HttpRequestGet<CustomerModel> requestGet;
            HttpRequestPost<CustomerModel> request;
            int idToGet = TypeExtension.DefaultInteger;
            string urlRoot = new ConfigurationManagerFull().AppSettingValue("MyWebService");
            string urlFull = TypeExtension.DefaultString;

            // Ensure customers exist
            await this.Customer_Cloud_CustomerPut();
            idToGet = recycleBin.Count() > 0 ? recycleBin[0] : TypeExtension.DefaultInteger;

            // Call the cloud and get results
            urlFull = String.Format("{0}/{1}/{2}", urlRoot, "Customer", idToGet);
            requestGet = new HttpRequestGet<CustomerModel>(urlFull);
            responseData = await requestGet.SendAsync();
            Assert.IsTrue(responseData.ID != TypeExtension.DefaultInteger, "Customer did not get.");
            Assert.IsTrue(responseData.Key != TypeExtension.DefaultGuid, "Customer did not get.");

            // Now edit the customer
            responseData.FirstName = responseData.FirstName + "Edited";
            request = new HttpRequestPost<CustomerModel>(urlFull, responseData);
            responseData = await request.SendAsync();
            Assert.IsTrue(responseData.ID != TypeExtension.DefaultInteger, "Customer did not get.");
            Assert.IsTrue(responseData.FirstName.Contains("Edited") == true, "Customer did not get.");
        }

        /// <summary>
        /// Delete a customer from the cloud
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Customer_Cloud_CustomerGet()
        {
            CustomerModel responseData = new CustomerModel();
            HttpRequestGet<CustomerModel> request;
            int idToGet = TypeExtension.DefaultInteger;
            string urlRoot = new ConfigurationManagerFull().AppSettingValue("MyWebService");
            string urlFull = TypeExtension.DefaultString;

            // Ensure customers exist
            await this.Customer_Cloud_CustomerPut();
            idToGet = recycleBin.Count() > 0 ? recycleBin[0] : TypeExtension.DefaultInteger;

            // Call the cloud and get results
            urlFull = String.Format("{0}/{1}/{2}", urlRoot, "Customer", idToGet);
            request = new HttpRequestGet<CustomerModel>(urlFull);
            responseData = await request.SendAsync();
            Assert.IsTrue(responseData.ID != TypeExtension.DefaultInteger, "Customer did not get.");
            Assert.IsTrue(responseData.Key != TypeExtension.DefaultGuid, "Customer did not get.");
        }

        /// <summary>
        /// Get a customer from the cloud
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Customer_Cloud_CustomerDelete()
        {
            CustomerModel responseData = new CustomerModel();
            HttpRequestGet<CustomerModel> requestGet;
            HttpRequestDelete<CustomerModel> requestDelete;
            int idToDelete = TypeExtension.DefaultInteger;
            string urlRoot = new ConfigurationManagerFull().AppSettingValue("MyWebService");
            string urlFull = TypeExtension.DefaultString;

            // Ensure customers exist
            await this.Customer_Cloud_CustomerPut();
            idToDelete = recycleBin.Count() > 0 ? recycleBin[0] : TypeExtension.DefaultInteger;

            // Call the cloud and get results
            urlFull = String.Format("{0}/{1}/{2}", urlRoot, "Customer", idToDelete);
            requestDelete = new HttpRequestDelete<CustomerModel>(urlFull);
            responseData = await requestDelete.SendAsync();

            // Success is: Get Customer by ID should return a empty constructed CustomerInfo object.
            requestGet = new HttpRequestGet<CustomerModel>(urlFull);
            responseData = await requestDelete.SendAsync();
            Assert.IsTrue(responseData.ID == TypeExtension.DefaultInteger, "Customer did not delete.");
            Assert.IsTrue(responseData.Key == TypeExtension.DefaultGuid, "Customer did not delete.");
        }

        /// <summary>
        /// Delete a customer from the cloud
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public async Task Customer_Cloud_CustomerSearchGet()
        {
            CustomerSearchModel returnData = new CustomerSearchModel();
            HttpRequestGet<CustomerSearchModel> request;
            string urlRoot = new ConfigurationManagerFull().AppSettingValue("MyWebService");
            string urlFull = TypeExtension.DefaultString;

            // Find customer by first name. -1 for ID does not exist, and empty string Last Name doesnt exist
            urlFull = String.Format("{0}/{1}/{2}?firstName={3}&lastName={4}", urlRoot, "CustomerSearch", -1, "i", "");
            request = new HttpRequestGet<CustomerSearchModel>(urlFull);
            returnData = await request.SendAsync();
            Assert.IsTrue(returnData.Results.Count > 0, "Customer did not get.");
            Assert.IsTrue(returnData.FirstName != TypeExtension.DefaultString, "Customer did not get.");
        }

        /// <summary>
        /// Get customer types
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public void Customer_Cloud_CustomerTypeGet()
        {
            IQueryable<CustomerType> types = CustomerType.GetAll();
            Assert.IsTrue(types.Any() == true, "Did not work");
        }

        /// <summary>
        /// Dates serializing differently when returned from canned webapi method
        /// One date is Date.UtcNow().AddYears(-20) and another is DefaultDate. The first is -HH:MM:SSZ and the other is 00:00:00 (no Z)
        /// </summary>
        [TestMethod()]
        public void Customer_Cloud_DateSerialization()
        {            
            CustomerSearchModel model;
            string serialized;
            var serializer = new Genesys.Extras.Serialization.JsonSerializer<CustomerSearchModel>();
            // test with "BirthDate":"1990-11-22T00:00:00"
            model = new CustomerSearchModel() { ID = -1, FirstName = "gg", LastName = "", BirthDate = new DateTime(1990, 11, 22) };
            model.Results.Add(new CustomerModel() { FirstName = "first", LastName = "last", BirthDate = new DateTime(2012, 01, 01) });
            serialized = serializer.Serialize(model);
            Assert.IsTrue(true);
            model = serializer.Deserialize(serialized);
            Assert.IsTrue(true);

            // Works: test with "BirthDate":"1995-12-27T23:08:29.260991Z" - Incorrect format (found when = Date.UtcNow().AddYears(-20))
            model = new CustomerSearchModel() { ID = -1, FirstName = "gg", LastName = "", BirthDate = DateTime.UtcNow.AddYears(-20) };
            model.Results.Add(new CustomerModel() { FirstName = "first", LastName = "last", BirthDate = new DateTime(2012, 01, 01) });
            serialized = serializer.Serialize(model);
            Assert.IsTrue(true);
            model = serializer.Deserialize(serialized);
            Assert.IsTrue(true);
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
