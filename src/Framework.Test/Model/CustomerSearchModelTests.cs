//-----------------------------------------------------------------------
// <copyright file="CustomerSearchModelTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Framework.DataAccess;
using Framework.Entity;
using Genesys.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Framework.Test
{
    [TestClass()]
    public class CustomerSearchModelTests
    {
        /// <summary>
        /// Entity_CustomerInfo
        /// </summary>
        [TestMethod()]
        public void Model_CustomerSearch_GetBySearchFields()
        {
            var searchChar = "i";
            IQueryable<CustomerInfo> results;
            var searchModel = new CustomerSearchModel() { FirstName = searchChar, LastName = searchChar };
            results = CustomerInfo.GetByAny(searchModel);
            Assert.IsTrue(results.Count() > 0);
            searchModel.Results.FillRange(results);
            Assert.IsTrue(searchModel.FirstName == searchChar && searchModel.LastName == searchChar);
            Assert.IsTrue(searchModel.Results.Count() > 0);
        }
    }
}
