using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Empro.Data
{
    public static class DatabaseInitializer
    {
        private const string ConnectionString = "Data Source=employees.db";

        public static void Initialize()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Employees (
                    EmployeeId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Furigana TEXT,
                    Gender TEXT,
                    Department TEXT,
                    EmploymentType TEXT,
                    BirthDate TEXT NOT NULL,
                    JoiningDate TEXT NOT NULL,
                    RetirementDate TEXT,
                    Address TEXT,
                    ResidentTaxType TEXT,
                    Notes TEXT,
                    SocialInsuranceStatus TEXT,
                    SocialInsuranceDate TEXT,
                    EmploymentInsuranceStatus TEXT,
                    EmploymentInsuranceDate TEXT
                )";
            command.ExecuteNonQuery();
        }
    }
}

