//-----------------------------------------------------------------------
// <copyright file="CustomerSearchModel.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Framework.Entity;
using System;
using System.Collections.Generic;

namespace Framework.Entity
{
    /// <summary>
    /// Customer Search Results
    /// </summary>
    [CLSCompliant(true)]
    public class CustomerSearchModel : ModelEntity<CustomerSearchModel>, ICustomer
    {
        /// <summary>
        /// FirstName of customers
        /// </summary>     
        public string FirstName { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// MiddleName of customer
        /// </summary>
        public string MiddleName { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// LastName of customer
        /// </summary>
        public string LastName { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        public DateTime BirthDate { get; set; } = TypeExtension.DefaultDate;

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        public int GenderID { get; set; } = CustomerModel.Genders.NotSet.Key;

        /// <summary>
        /// Type of customer
        /// </summary>
        public Guid CustomerTypeKey { get; set; } = CustomerTypeModel.Types.None;

        /// <summary>
        /// Search results
        /// </summary>
        public List<CustomerModel> Results { get; set; } = new List<CustomerModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        public CustomerSearchModel()
                : base()
        {
            this.CustomerTypeKey = CustomerTypeModel.Types.Standard;
        }

    }
}