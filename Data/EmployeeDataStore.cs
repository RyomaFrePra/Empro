using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using Empro.Models;

namespace Empro
{
    public static class EmployeeDataStore
    {
        private static string connectionString = "Data Source=employee.db";

        static EmployeeDataStore()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Employees (
                    EmployeeNumber TEXT PRIMARY KEY,
                    FirstName TEXT,
                    LastName TEXT,
                    Kana TEXT,
                    Gender TEXT,
                    Department TEXT,
                    EmploymentType TEXT,
                    BirthDate TEXT,
                    JoinDate TEXT,
                    ResignDate TEXT,
                    TaxAddress TEXT,
                    ResidentTaxCategory TEXT,
                    Notes TEXT,
                    IsSocialInsuranceJoined INTEGER,
                    SocialInsuranceJoinDate TEXT,
                    IsEmploymentInsuranceJoined INTEGER,
                    EmploymentInsuranceJoinDate TEXT,
                    SocialInsuranceLeaveDate TEXT, -- 社保脱退日
                    EmploymentInsuranceLeaveDate TEXT -- 雇用保険脱退日
                )";
                command.ExecuteNonQuery();
            }
        }

        public static void AddEmployee(Employee employee)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO Employees (
                EmployeeNumber, FirstName, LastName, Kana, Gender, Department, EmploymentType, BirthDate, JoinDate, ResignDate, TaxAddress, ResidentTaxCategory, Notes, IsSocialInsuranceJoined, SocialInsuranceJoinDate, IsEmploymentInsuranceJoined, EmploymentInsuranceJoinDate, SocialInsuranceLeaveDate, EmploymentInsuranceLeaveDate
            ) VALUES (
                $EmployeeNumber, $FirstName, $LastName, $Kana, $Gender, $Department, $EmploymentType, $BirthDate, $JoinDate, $ResignDate, $TaxAddress, $ResidentTaxCategory, $Notes, $IsSocialInsuranceJoined, $SocialInsuranceJoinDate, $IsEmploymentInsuranceJoined, $EmploymentInsuranceJoinDate, $SocialInsuranceLeaveDate, $EmploymentInsuranceLeaveDate
            )";
                command.Parameters.AddWithValue("$EmployeeNumber", employee.EmployeeNumber);
                command.Parameters.AddWithValue("$FirstName", employee.FirstName);
                command.Parameters.AddWithValue("$LastName", employee.LastName);
                command.Parameters.AddWithValue("$Kana", employee.Kana);
                command.Parameters.AddWithValue("$Gender", employee.Gender.ToString());
                command.Parameters.AddWithValue("$Department", employee.Department.ToString());
                command.Parameters.AddWithValue("$EmploymentType", employee.EmploymentType.ToString());
                command.Parameters.AddWithValue("$BirthDate", employee.BirthDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$JoinDate", employee.JoinDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$ResignDate", employee.ResignDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$TaxAddress", employee.TaxAddress);
                command.Parameters.AddWithValue("$ResidentTaxCategory", employee.ResidentTaxCategory.ToString());
                command.Parameters.AddWithValue("$Notes", employee.Notes);
                command.Parameters.AddWithValue("$IsSocialInsuranceJoined", employee.IsSocialInsuranceJoined ? 1 : 0);
                command.Parameters.AddWithValue("$SocialInsuranceJoinDate", employee.SocialInsuranceJoinDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$IsEmploymentInsuranceJoined", employee.IsEmploymentInsuranceJoined ? 1 : 0);
                command.Parameters.AddWithValue("$EmploymentInsuranceJoinDate", employee.EmploymentInsuranceJoinDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$SocialInsuranceLeaveDate", employee.SocialInsuranceLeaveDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$EmploymentInsuranceLeaveDate", employee.EmploymentInsuranceLeaveDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.ExecuteNonQuery();
            }
        }


        public static List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Employees";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            EmployeeNumber = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Kana = reader.GetString(3),
                            Gender = Enum.Parse<Gender>(reader.GetString(4)),
                            Department = Enum.Parse<DepartmentType>(reader.GetString(5)),
                            EmploymentType = Enum.Parse<EmploymentType>(reader.GetString(6)),
                            BirthDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                            JoinDate = reader.IsDBNull(8) ? (DateTime?)null : DateTime.Parse(reader.GetString(8)),
                            ResignDate = reader.IsDBNull(9) ? (DateTime?)null : DateTime.Parse(reader.GetString(9)),
                            TaxAddress = reader.GetString(10),
                            ResidentTaxCategory = Enum.Parse<TaxCategory>(reader.GetString(11)),
                            Notes = reader.GetString(12),
                            IsSocialInsuranceJoined = reader.GetInt32(13) == 1,
                            SocialInsuranceJoinDate = reader.IsDBNull(14) ? (DateTime?)null : DateTime.Parse(reader.GetString(14)),
                            IsEmploymentInsuranceJoined = reader.GetInt32(15) == 1,
                            EmploymentInsuranceJoinDate = reader.IsDBNull(16) ? (DateTime?)null : DateTime.Parse(reader.GetString(16)),
                            SocialInsuranceLeaveDate = reader.IsDBNull(17) ? (DateTime?)null : DateTime.Parse(reader.GetString(17)), // 社保脱退日
                            EmploymentInsuranceLeaveDate = reader.IsDBNull(18) ? (DateTime?)null : DateTime.Parse(reader.GetString(18)) // 雇用保険脱退日
                        };
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        public static Employee GetEmployee(string employeeNumber)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Employees WHERE EmployeeNumber = $EmployeeNumber";
                command.Parameters.AddWithValue("$EmployeeNumber", employeeNumber);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmployeeNumber = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Kana = reader.GetString(3),
                            Gender = Enum.Parse<Gender>(reader.GetString(4)),
                            Department = Enum.Parse<DepartmentType>(reader.GetString(5)),
                            EmploymentType = Enum.Parse<EmploymentType>(reader.GetString(6)),
                            BirthDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader.GetString(7)),
                            JoinDate = reader.IsDBNull(8) ? (DateTime?)null : DateTime.Parse(reader.GetString(8)),
                            ResignDate = reader.IsDBNull(9) ? (DateTime?)null : DateTime.Parse(reader.GetString(9)),
                            TaxAddress = reader.GetString(10),
                            ResidentTaxCategory = Enum.Parse<TaxCategory>(reader.GetString(11)),
                            Notes = reader.GetString(12),
                            IsSocialInsuranceJoined = reader.GetInt32(13) == 1,
                            SocialInsuranceJoinDate = reader.IsDBNull(14) ? (DateTime?)null : DateTime.Parse(reader.GetString(14)),
                            IsEmploymentInsuranceJoined = reader.GetInt32(15) == 1,
                            EmploymentInsuranceJoinDate = reader.IsDBNull(16) ? (DateTime?)null : DateTime.Parse(reader.GetString(16)),
                            SocialInsuranceLeaveDate = reader.IsDBNull(17) ? (DateTime?)null : DateTime.Parse(reader.GetString(17)), // 社保脱退日
                            EmploymentInsuranceLeaveDate = reader.IsDBNull(18) ? (DateTime?)null : DateTime.Parse(reader.GetString(18)) // 雇用保険脱退日
                        };
                    }
                }
            }

            return null;
        }


        public static void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            UPDATE Employees SET
                FirstName = $FirstName,
                LastName = $LastName,
                Kana = $Kana,
                Gender = $Gender,
                Department = $Department,
                EmploymentType = $EmploymentType,
                BirthDate = $BirthDate,
                JoinDate = $JoinDate,
                ResignDate = $ResignDate,
                TaxAddress = $TaxAddress,
                ResidentTaxCategory = $ResidentTaxCategory,
                Notes = $Notes,
                IsSocialInsuranceJoined = $IsSocialInsuranceJoined,
                SocialInsuranceJoinDate = $SocialInsuranceJoinDate,
                IsEmploymentInsuranceJoined = $IsEmploymentInsuranceJoined,
                EmploymentInsuranceJoinDate = $EmploymentInsuranceJoinDate,
                SocialInsuranceLeaveDate = $SocialInsuranceLeaveDate,
                EmploymentInsuranceLeaveDate = $EmploymentInsuranceLeaveDate
            WHERE EmployeeNumber = $EmployeeNumber";
                command.Parameters.AddWithValue("$EmployeeNumber", employee.EmployeeNumber);
                command.Parameters.AddWithValue("$FirstName", employee.FirstName);
                command.Parameters.AddWithValue("$LastName", employee.LastName);
                command.Parameters.AddWithValue("$Kana", employee.Kana);
                command.Parameters.AddWithValue("$Gender", employee.Gender.ToString());
                command.Parameters.AddWithValue("$Department", employee.Department.ToString());
                command.Parameters.AddWithValue("$EmploymentType", employee.EmploymentType.ToString());
                command.Parameters.AddWithValue("$BirthDate", employee.BirthDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$JoinDate", employee.JoinDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$ResignDate", employee.ResignDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$TaxAddress", employee.TaxAddress);
                command.Parameters.AddWithValue("$ResidentTaxCategory", employee.ResidentTaxCategory.ToString());
                command.Parameters.AddWithValue("$Notes", employee.Notes);
                command.Parameters.AddWithValue("$IsSocialInsuranceJoined", employee.IsSocialInsuranceJoined ? 1 : 0);
                command.Parameters.AddWithValue("$SocialInsuranceJoinDate", employee.SocialInsuranceJoinDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$IsEmploymentInsuranceJoined", employee.IsEmploymentInsuranceJoined ? 1 : 0);
                command.Parameters.AddWithValue("$EmploymentInsuranceJoinDate", employee.EmploymentInsuranceJoinDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$SocialInsuranceLeaveDate", employee.SocialInsuranceLeaveDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("$EmploymentInsuranceLeaveDate", employee.EmploymentInsuranceLeaveDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.ExecuteNonQuery();
            }
        }


        public static void DeleteEmployee(string employeeNumber)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Employees WHERE EmployeeNumber = $EmployeeNumber";
                command.Parameters.AddWithValue("$EmployeeNumber", employeeNumber);
                command.ExecuteNonQuery();
            }
        }
    }
}


