using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Empro.Models;

namespace Empro.Data
{
    public class EmployeeRepository
    {
        private const string ConnectionString = "Data Source=employees.db";

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Furigana = reader.GetString(2),
                    Gender = reader.GetString(3),
                    Department = reader.GetString(4),
                    EmploymentType = reader.GetString(5),
                    BirthDate = DateTime.Parse(reader.GetString(6)),
                    JoiningDate = DateTime.Parse(reader.GetString(7)),
                    RetirementDate = reader.IsDBNull(8) ? null : DateTime.Parse(reader.GetString(8)),
                    Address = reader.GetString(9),
                    ResidentTaxType = reader.GetString(10),
                    Notes = reader.GetString(11),
                    SocialInsuranceStatus = reader.GetString(12),
                    SocialInsuranceDate = reader.IsDBNull(13) ? null : DateTime.Parse(reader.GetString(13)),
                    EmploymentInsuranceStatus = reader.GetString(14),
                    EmploymentInsuranceDate = reader.IsDBNull(15) ? null : DateTime.Parse(reader.GetString(15)),
                });
            }

            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Employees (Name, Furigana, Gender, Department, EmploymentType, BirthDate, JoiningDate, RetirementDate,
                                       Address, ResidentTaxType, Notes, SocialInsuranceStatus, SocialInsuranceDate,
                                       EmploymentInsuranceStatus, EmploymentInsuranceDate)
                VALUES (@Name, @Furigana, @Gender, @Department, @EmploymentType, @BirthDate, @JoiningDate, @RetirementDate,
                        @Address, @ResidentTaxType, @Notes, @SocialInsuranceStatus, @SocialInsuranceDate,
                        @EmploymentInsuranceStatus, @EmploymentInsuranceDate)";
            command.Parameters.AddWithValue("@Name", employee.Name);
            command.Parameters.AddWithValue("@Furigana", employee.Furigana);
            command.Parameters.AddWithValue("@Gender", employee.Gender);
            command.Parameters.AddWithValue("@Department", employee.Department);
            command.Parameters.AddWithValue("@EmploymentType", employee.EmploymentType);
            command.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
            command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
            command.Parameters.AddWithValue("@RetirementDate", employee.RetirementDate);
            command.Parameters.AddWithValue("@Address", employee.Address);
            command.Parameters.AddWithValue("@ResidentTaxType", employee.ResidentTaxType);
            command.Parameters.AddWithValue("@Notes", employee.Notes);
            command.Parameters.AddWithValue("@SocialInsuranceStatus", employee.SocialInsuranceStatus);
            command.Parameters.AddWithValue("@SocialInsuranceDate", employee.SocialInsuranceDate);
            command.Parameters.AddWithValue("@EmploymentInsuranceStatus", employee.EmploymentInsuranceStatus);
            command.Parameters.AddWithValue("@EmploymentInsuranceDate", employee.EmploymentInsuranceDate);

            command.ExecuteNonQuery();
        }
    }
}

