//-----------------------------------------------------------------------
// <copyright file="DatabaseSchemaNameTests.cs" company="Genesys Source">
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
    public class DatabaseSchemaNameTests
    {
        private const string testValue = "dbo";
        private const string testValueNotFound = "NoSchema";
        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_ConnectionStringAttribute()
        {
            var testItem = new ClassWithDatabaseSchema();
            string result = testItem.GetAttributeValue<DatabaseSchemaName>(testValueNotFound);
            Assert.IsTrue(result != testValueNotFound);
            Assert.IsTrue(result == testValue);
        }

        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_DatabaseSchemaAttribute()
        {
        }

        /// <summary>
        /// Tests attributes        
        /// </summary>
        [DatabaseSchemaName(testValue)]
        internal class ClassWithDatabaseSchema
        {
        }
    }
}
