//-----------------------------------------------------------------------
// <copyright file="ReadOnlyDatabaseNameTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
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
using Foundation.Entity;
using Genesys.Extensions;
using Genesys.Foundation.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Foundation.Test
{
    [TestClass()]
    public class ReadOnlyDatabaseTests
    {
        /// <summary>
        /// ReadOnlyDatabase context and connection
        /// </summary>
        [TestMethod()]
        public void Data_ReadOnlyDatabase_GetAll()
        {
            var reader = ReadOnlyDatabase<CustomerType>.Construct();
            var results = reader.GetAll();
            Assert.IsTrue(results.Count() > 0);
        }

        /// <summary>
        /// ReadOnlyDatabase context and connection
        /// </summary>
        [TestMethod()]
        public void Data_ReadOnlyDatabase_GetByID()
        {
            var reader = ReadOnlyDatabase<CustomerType>.Construct();
            var all = reader.GetAll();
            int id = reader.GetAllExcludeDefault().FirstOrDefaultSafe().ID;
            var results = reader.GetByID(id);
            Assert.IsTrue(results.ID != TypeExtension.DefaultInteger);
            Assert.IsTrue(results.Key != TypeExtension.DefaultGuid);
        }

        /// <summary>
        /// ReadOnlyDatabase context and connection
        /// </summary>
        [TestMethod()]
        public void Data_ReadOnlyDatabase_GetByKey()
        {
            var reader = ReadOnlyDatabase<CustomerType>.Construct();
            var all = reader.GetAll();
            Guid key = reader.GetAllExcludeDefault().FirstOrDefaultSafe().Key;
            var results = reader.GetByKey(key);
            Assert.IsTrue(results.ID != TypeExtension.DefaultInteger);
            Assert.IsTrue(results.Key != TypeExtension.DefaultGuid);
        }
    }
}
