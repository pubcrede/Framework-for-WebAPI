//-----------------------------------------------------------------------
// <copyright file="MyApplication.cs" company="Genesys Source">
//      Copyright (c) Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using Genesys.Extras.Configuration;
using System.Data.SqlClient;

namespace Framework.Test.Data
{
    /// <summary>
    /// Utility methods for table
    /// </summary>
    public class Tables
    {
        /// <summary>
        /// Removes EF code-first migration history table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="schema"></param>
        public static void DropMigrationHistory(string database = "[FrameworkData]", string schema = "[Activity]")
        {
            // Must remove __MigrationHistory for EF Code First objects to auto-create their tables
            var configuration = new ConfigurationManagerFull();

            try
            {
                using (var connection = new SqlConnection(configuration.ConnectionStringValue("DefaultConnection")))
                {
                    using (var command = new SqlCommand("Drop Table " + database + "." + schema + ".[__MigrationHistory]", connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                // Ignore connection errors
            }
        }
    }
}
