//-----------------------------------------------------------------------
// <copyright file="ActivityLoggerTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genesys.Framework.Activity;
using System.Linq;
using Genesys.Extensions;
using Framework.Test.Data;

namespace Framework.Test
{
    /// <summary>
    /// Tests code first ActivityLogger object saving activity to the database 
    /// </summary>
    [TestClass()]
    public partial class ActivityLoggerTests
    {
        /// <summary>
        /// Tests code first ActivityLogger object saving activity to the database
        /// </summary>
        [TestMethod()]
        public void Activity_ActivityLogger()
        {
            var preSaveCount = TypeExtension.DefaultInteger;
            var postSaveCount = TypeExtension.DefaultInteger;

            Tables.DropMigrationHistory();
            preSaveCount = ActivityLogger.GetAll("DefaultConnection", "Activity").Count();
            ActivityLogger log1 = new ActivityLogger("DefaultConnection", "Activity");
            log1.Save();
            postSaveCount = ActivityLogger.GetAll("DefaultConnection", "Activity").Count();
            Assert.IsTrue(log1.ActivityContextID != TypeExtension.DefaultInteger);
            Assert.IsTrue(postSaveCount == preSaveCount + 1);

            // Your custom schema
            ActivityLogger log2 = new ActivityLogger("DefaultConnection", "MySchema");
            preSaveCount = ActivityLogger.GetAll("DefaultConnection", "Activity").Count();
            log2.Save();
            postSaveCount = ActivityLogger.GetAll("DefaultConnection", "Activity").Count();
            Assert.IsTrue(log2.ActivityContextID != TypeExtension.DefaultInteger);
            Assert.IsTrue(postSaveCount == preSaveCount + 1);
        }
    }
}