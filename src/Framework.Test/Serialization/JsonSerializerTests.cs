//-----------------------------------------------------------------------
// <copyright file="JsonSerializerTests.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Genesys.Extensions;
using Genesys.Extras.Serialization;
using Genesys.Extras.Text;

namespace Framework.Test
{
    [TestClass()]
    public class JsonSerializerTests
    {
        [TestMethod()]
        public void Serialization_Json_ValueTypes()
        {
            var data1 = TypeExtension.DefaultString;
            var testData1 = "TestDataHere";
            ISerializer<object> serialzer1 = new JsonSerializer<object>();
            data1 = serialzer1.Serialize(testData1);
            Assert.IsTrue(serialzer1.Deserialize(data1).ToString() == testData1);
            
            var data = TypeExtension.DefaultString;
            StringMutable testData = "TestDataHere";
            var serialzer = new JsonSerializer<StringMutable>();
            data = serialzer.Serialize(testData);
            Assert.IsTrue(serialzer.Deserialize(data).ToString() == testData.ToString());
        }

        [TestMethod()]
        public void Serialization_Json_ReferenceTypes()
        {
            var ItemL = new List<int> { 1, 2, 3 };
            var Serializer = new JsonSerializer<List<int>>();
            var SerializedDataL = Serializer.Serialize(ItemL);
            Assert.IsTrue(ItemL.Count == Serializer.Deserialize(SerializedDataL).Count);
        }
    }
}