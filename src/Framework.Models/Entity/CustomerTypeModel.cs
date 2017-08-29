//-----------------------------------------------------------------------
// <copyright file="CustomerTypeModel.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Genesys.Framework.Name;
using Genesys.Extensions;

namespace Framework.Entity
{
    /// <summary>
    /// Customer Type view/http transport model, mainly for a key/value of ID/Name
    /// </summary>
    [CLSCompliant(true)]
    public class CustomerTypeModel : NameIDModel, ICustomerType
    {
        /// <summary>
        /// Common customer type keys used as an ID for the table column CustomerType.CustomerTypeKey
        /// </summary>
        public struct Types
        {
            public static Guid None { get; set; } = TypeExtension.DefaultGuid;
            public static Guid Standard { get; set; } = new Guid("BF3797EE-06A5-47F2-9016-369BEB21D944");
            public static Guid Premium { get; set; } = new Guid("36B08B23-0C1D-4488-B557-69665FD666E1");
            public static Guid Lifetime { get; set; } = new Guid("51A84CE1-4846-4A71-971A-CB610EEB4848");
        }
    }
}
