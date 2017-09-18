//-----------------------------------------------------------------------
// <copyright file="CustomerType.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Framework.Entity;
using Genesys.Extensions;
using Genesys.Framework.Data;
using Genesys.Framework.Entity;
using System;
using System.Linq;

namespace Framework.DataAccess
{
    /// <summary>
    /// EntityCustomer
    /// </summary>
    [CLSCompliant(true), ConnectionStringName("DefaultConnection")]
    public partial class CustomerType : EntityInfo<CustomerType>, ICustomerType
    {
        /// <summary>
        /// CustomerTypeID enumeration for static values
        /// </summary>
        public struct Types
        {
            public static Guid None { get; set; } = TypeExtension.DefaultGuid;
            public static Guid Standard { get; set; } = new Guid("BF3797EE-06A5-47F2-9016-369BEB21D944");
            public static Guid Premium { get; set; } = new Guid("36B08B23-0C1D-4488-B557-69665FD666E1");
            public static Guid Lifetime { get; set; } = new Guid("51A84CE1-4846-4A71-971A-CB610EEB4848");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerType()
            : base()
        {
        }

        /// <summary>
        /// Gets all records that exactly equal passed name
        /// </summary>
        /// <param name="name">Value to search CustomerTypeName field </param>
        /// <returns>All records matching the passed name</returns>
        public static IQueryable<CustomerType> GetByName(string name)
        {
            var reader = ReadOnlyDatabase<CustomerType>.Construct();
            return reader.GetAll().Where(x => x.Name == name);
        }
    }
}
