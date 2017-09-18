//-----------------------------------------------------------------------
// <copyright file="ICustomer.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
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
