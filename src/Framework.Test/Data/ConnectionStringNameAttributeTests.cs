//-----------------------------------------------------------------------
// <copyright file="ConnectionStringNameTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extensions;
using Genesys.Extras.Configuration;
using Genesys.Framework.Data;
using Genesys.Framework.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Test
{
    [TestClass()]
    public class ConnectionStringNameTests
    {
        private const string testValue = "DefaultConnection";
        private const string testValueNotFound = "NoConnection";
        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_ConnectionStringAttribute()
        {
            var testItem = new ClassWithConnectString();
            string result = testItem.GetAttributeValue<ConnectionStringName>(testValueNotFound);
            Assert.IsTrue(result != testValueNotFound);
            Assert.IsTrue(result == testValue);
        }

        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_ConnectionStringFromConfig()
        {
            var result = TypeExtension.DefaultString;
            var configManager = new ConfigurationManagerFull();
            var configConnectString = new ConnectionStringSafe();
            configConnectString = configManager.ConnectionString(this.GetAttributeValue<ConnectionStringName>(ConnectionStringName.DefaultValue));
            result = configConnectString.ToEF(typeof(ClassWithConnectString));
            Assert.IsTrue(result != TypeExtension.DefaultString);
            Assert.IsTrue(configConnectString.IsValid == true);
            Assert.IsTrue(configConnectString.IsEF || configConnectString.IsADO);
            Assert.IsTrue(configConnectString.ConnectionStringType != ConnectionStringSafe.ConnectionStringTypes.Empty
                && configConnectString.ConnectionStringType != ConnectionStringSafe.ConnectionStringTypes.Invalid);
        }

        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_ConnectionStringEntity()
        {
            var result = TypeExtension.DefaultString;
            var configManager = new ConfigurationManagerFull();
            var configConnectString = new ConnectionStringSafe();
            configConnectString = configManager.ConnectionString(this.GetAttributeValue<ConnectionStringName>(ConnectionStringName.DefaultValue));
            result = configConnectString.ToEF(typeof(EntityWithConnectString));
            Assert.IsTrue(result != TypeExtension.DefaultString);
            Assert.IsTrue(configConnectString.IsValid == true);
            Assert.IsTrue(configConnectString.IsEF || configConnectString.IsADO);
            Assert.IsTrue(configConnectString.ConnectionStringType != ConnectionStringSafe.ConnectionStringTypes.Empty
                && configConnectString.ConnectionStringType != ConnectionStringSafe.ConnectionStringTypes.Invalid);
        }

        /// <summary>
        /// Attribute-based connection string nanems
        /// </summary>
        [TestMethod()]
        public void Data_ConnectionStringDatabase()
        {
            var result = TypeExtension.DefaultString;
            var configManager = new ConfigurationManagerFull();
            var configConnectString = new ConnectionStringSafe();

            configConnectString = configManager.ConnectionString(this.GetAttributeValue<ConnectionStringName>(ConnectionStringName.DefaultValue));
            result = configConnectString.ToEF(typeof(EntityWithConnectString));
            Assert.IsTrue(result != TypeExtension.DefaultString);
            Assert.IsTrue(configConnectString.IsValid == true);
            Assert.IsTrue(configConnectString.IsEF || configConnectString.IsADO);
            Assert.IsTrue(configConnectString.ConnectionStringType != ConnectionStringSafe.ConnectionStringTypes.Empty
                && configConnectString.ConnectionStringType != ConnectionStringSafe.ConnectionStringTypes.Invalid);
        }

        /// <summary>
        /// Tests attributes        
        /// </summary>
        [ConnectionStringName(testValue)]
        internal class ClassWithConnectString
        {            
        }

        /// <summary>
        /// Tests attributes        
        /// </summary>
        [ConnectionStringName(testValue)]
        internal class EntityWithConnectString : EntityInfo<EntityWithConnectString>
        {
        }
    }
}
