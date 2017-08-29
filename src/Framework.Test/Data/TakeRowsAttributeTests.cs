//-----------------------------------------------------------------------
// <copyright file="TakeRowsTests.cs" company="Genesys Source">
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
    public class TakeRowsTests
    {
        private const int testValue = 25;
        private const int testValueNotFound = 100;
        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_TakeRowsAttribute()
        {
            var testItem = new ClassWithTakeRows();
            int result = testItem.GetAttributeValue<TakeRows, int>(testValueNotFound);
            Assert.IsTrue(result != testValueNotFound);
            Assert.IsTrue(result == testValue);
        }

        /// <summary>
        /// Tests attributes        
        /// </summary>
        [TakeRows(testValue)]
        internal class ClassWithTakeRows
        {            
        }
    }
}
