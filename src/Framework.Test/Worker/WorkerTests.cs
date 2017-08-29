//-----------------------------------------------------------------------
// <copyright file="ProcessTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Genesys.Extensions;
using Genesys.Extras.Serialization;
using Genesys.Framework.Name;
using Genesys.Framework.Session;
using Genesys.Framework.Worker;

namespace Framework.Test
{
    /// <summary>
    /// Tests for interop process classes
    /// </summary>
    [TestClass()]
    public class ProcessTests
    {
        /// <summary> 
        /// Worker_SessionContextKnownType
        /// ProcessParameter has ISessionContext as parameter, but SessionContext is passed in to DataContractJsonSerializer
        ///   Had to add SessionContext as known type
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public void Worker_SessionContextKnownType()
        {
            String dataToSendSerialized2 = TypeExtension.DefaultString;
            SessionContext context = new SessionContext(this.ToString(), Guid.NewGuid().ToString(), "MyName");
            NameIDModel dataIn = new NameIDModel() { Name = "NameField" };
            WorkerParameter<NameIDModel> item2 = new WorkerParameter<NameIDModel>() { Context = context, DataIn = dataIn };
            ISerializer<WorkerParameter<NameIDModel>> serializer2 = new JsonSerializer<WorkerParameter<NameIDModel>>();

            // Test Serialization            
            dataToSendSerialized2 = serializer2.Serialize(item2);
            Assert.IsTrue(dataToSendSerialized2 != TypeExtension.DefaultString, "Did not work");
        }

        /// <summary> 
        /// Worker_WorkerParameterSerialize
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public void Worker_WorkerParameterSerialize()
        {
            // Initialize
            var dataToSendSerialized = TypeExtension.DefaultString;
            var context = new SessionContext(this.ToString(), Guid.NewGuid().ToString(), "MyName");
            var dataIn = new NameIDModel() { Name = "NameField" };
            var item1 = new WorkerParameter<NameIDModel>() { Context = context, DataIn = dataIn };
            var serializer = new JsonSerializer<WorkerParameter<NameIDModel>>();

            // Disable exceptions, we just want to look at results
            serializer.ThrowException = false;

            // Test Item1 Serialization
            dataToSendSerialized = serializer.Serialize(item1);
            Assert.IsTrue(dataToSendSerialized != TypeExtension.DefaultString, "Did not work");
            item1 = serializer.Deserialize(dataToSendSerialized);
            Assert.IsTrue(item1 != null, "Did not work.");
        }

        /// <summary>
        /// Person_PersonTests
        /// </summary>
        /// <remarks></remarks>
        [TestMethod()]
        public void Worker_WorkerResultSerialize()
        {
            // Initialize
            var DataToSendSerialized = TypeExtension.DefaultString;
            var Item1 = new SessionContext(this.ToString(), Guid.NewGuid().ToString(), "MyName");
            ISerializer<ISessionContext> Serializer1 = new JsonSerializer<ISessionContext>();
            ISerializer<SessionContext> Serializer2 = new JsonSerializer<SessionContext>();

            // Test Serialization
            DataToSendSerialized = Serializer1.Serialize(Item1);
            Assert.IsTrue(DataToSendSerialized != TypeExtension.DefaultString, "Did not work");

            // Test Serialization
            DataToSendSerialized = Serializer2.Serialize(Item1);
            Assert.IsTrue(DataToSendSerialized != TypeExtension.DefaultString, "Did not work");
        }
    }
}