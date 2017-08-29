//-----------------------------------------------------------------------
// <copyright file="ExceptionLoggerTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Framework.Activity;
using Framework.Test.Data;
using System.Linq;
using Genesys.Extensions;

namespace Framework.Test
{
    /// <summary>
    /// Tests code first ExceptionLogger functionality
    /// </summary>
    [TestClass()]
    public partial class ExceptionLoggerTests
    {
        /// <summary>
        /// Tests code first ExceptionLogger saving to the database
        /// </summary>
        [TestMethod()]
        public void Activity_ExceptionLogger()
        {
            var preSaveCount = TypeExtension.DefaultInteger;
            var postSaveCount = TypeExtension.DefaultInteger;

            Tables.DropMigrationHistory();

            ExceptionLogger log1 = new ExceptionLogger("DefaultConnection", "Activity");
            preSaveCount = ExceptionLogger.GetAll("DefaultConnection", "Activity").Count();
            log1.Save();
            postSaveCount = ExceptionLogger.GetAll("DefaultConnection", "Activity").Count();
            Assert.IsTrue(log1.ExceptionLogID != TypeExtension.DefaultInteger, "ActivityLogger threw exception.");
            Assert.IsTrue(postSaveCount == preSaveCount + 1);

            // Your custom schema
            ExceptionLogger log2 = new ExceptionLogger("DefaultConnection", "MySchema");
            preSaveCount = ExceptionLogger.GetAll("DefaultConnection", "Activity").Count();
            log2.Save();
            postSaveCount = ExceptionLogger.GetAll("DefaultConnection", "Activity").Count();
            Assert.IsTrue(log2.ExceptionLogID != TypeExtension.DefaultInteger, "ActivityLogger threw exception.");
            Assert.IsTrue(postSaveCount == preSaveCount + 1);
        }
    }
}