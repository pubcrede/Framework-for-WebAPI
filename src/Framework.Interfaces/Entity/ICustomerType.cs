//-----------------------------------------------------------------------
// <copyright file="ICustomerType.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Genesys.Framework.Name;

namespace Framework.Entity
{
    /// <summary>
    /// EXAMPLE: Shows CustomerType : ICustomerType read-only data accesss object 
    ///     transport to/from CustomerTypeModel : ICustomerType
    /// </summary>	
    [CLSCompliant(true)]
    public interface ICustomerType : INameID
    {
    }
}
