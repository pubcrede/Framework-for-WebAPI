//-----------------------------------------------------------------------
// <copyright file="DataAccessBehaviorNameTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Framework.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Test
{
    [TestClass()]
    public class DataAccessBehaviorNameTests
    {
        private const DataAccessBehaviors testValue = DataAccessBehaviors.SelectOnly;
        private const DataAccessBehaviors testValueNotFound = DataAccessBehaviors.AllAccess;

        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_DataAccessBehaviorAttribute()
        {
            var testItem = new ClassWithDataAccessBehavior();
            DataAccessBehaviors result = testItem.GetAttributeValue<DataAccessBehavior, DataAccessBehaviors>(testValueNotFound);
            Assert.IsTrue(result != testValueNotFound);
            Assert.IsTrue(result == testValue);
        }

        /// <summary>
        /// Tests attributes        
        /// </summary>
        [DataAccessBehavior(testValue)]
        internal class ClassWithDataAccessBehavior
        {            
        }
    }
}
