//-----------------------------------------------------------------------
// <copyright file="ValidationRuleTests.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Framework.Validation;
using Genesys.Framework.Worker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Test
{
    [TestClass()]
    public class ValidationRuleTests
    {
        [TestMethod()]
        public void Validation_ValidationRule()
        {
            WorkerResult result = new WorkerResult() { ReturnID = 123 };            
            ValidationRule<WorkerResult> rule = new ValidationRule<WorkerResult>("ReturnID", x => x.ReturnID > 0);
            Assert.IsTrue(rule.Validate(result) == true, "Did not work");
        }

        [TestMethod()]
        public void Validation_Validate()
        {
            WorkerResult result = new WorkerResult() { ReturnID = 123 };
            ValidationRule<WorkerResult> rule = new ValidationRule<WorkerResult>("ReturnID", x => x.ReturnID > 0);
            Assert.IsTrue(rule.Validate(result) == true, "Did not work");
        }
    }
}