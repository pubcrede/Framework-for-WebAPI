//-----------------------------------------------------------------------
// <copyright file="ICustomer.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
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
using Genesys.Framework.Entity;

namespace Framework.Entity
{
    /// <summary>
    /// Customer
    /// </summary>    
    [CLSCompliant(true)]
    public interface ICustomer : IID
    {
        /// <summary>
        /// FirstName of customer
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// MiddleName of customer
        /// </summary>
        string MiddleName { get; set; }

        /// <summary>
        /// LastName of customer
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        DateTime BirthDate { get; set; }

        /// <summary>
        /// Gender of customer using ISO-5218 integers
        /// </summary>
        int GenderID { get; set; }

        /// <summary>
        /// Type of customer
        /// </summary>
        Guid CustomerTypeKey { get; set; }
    }
}
