//-----------------------------------------------------------------------
// <copyright file="SerializerGenericTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Genesys.Extensions;
using Genesys.Extras.Serialization;
using Genesys.Extras.Text;

namespace Framework.Test
{
    [TestClass()]
    public class SerializerGenericTests
    {

        [TestMethod()]
        public void Serialization_SerializerGeneric_ValueTypes()
        {
            // Immutable string class
            var data1 = TypeExtension.DefaultString;
            var Testdata1 = "TestDataHere";
            ISerializer<object> Serialzer1 = new JsonSerializer<object>();
            data1 = Serialzer1.Serialize(Testdata1);
            Assert.IsTrue(Serialzer1.Deserialize(data1).ToString() == Testdata1);

            
            var data = TypeExtension.DefaultString;
            StringMutable TestData = "TestDataHere";
            var Serialzer = new JsonSerializer<StringMutable>();
            data = Serialzer.Serialize(TestData);
            Assert.IsTrue(Serialzer.Deserialize(data).ToString() == TestData.ToString());
        }

        [TestMethod()]
        public void Serialization_SerializerGeneric_ReferenceTypes()
        {
            // Collections, etc
            var ItemL = new List<int> { 1, 2, 3 };
            var Serializer = new JsonSerializer<List<int>>();
            var serializedDataL = Serializer.Serialize(ItemL);
            Assert.IsTrue(ItemL.Count == Serializer.Deserialize(serializedDataL).Count);
        }
    }
}