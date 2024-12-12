using System;
using System.Collections.Generic;
using Empro.Models;

namespace Empro.Data
{
    public class EmployeeRepository
    {
        private List<Employee> _employees = new List<Employee>();

        // 社員情報を取得する
        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        // 新しい社員を追加する
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        // 社員情報を更新する
        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _employees.Find(e => e.EmployeeNumber == employee.EmployeeNumber);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Kana = employee.Kana;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Department = employee.Department;
                existingEmployee.EmploymentType = employee.EmploymentType;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.JoinDate = employee.JoinDate;
                existingEmployee.ResignDate = employee.ResignDate;
                existingEmployee.TaxAddress = employee.TaxAddress;
                existingEmployee.ResidentTaxCategory = employee.ResidentTaxCategory;
                existingEmployee.Notes = employee.Notes;
                existingEmployee.IsSocialInsuranceJoined = employee.IsSocialInsuranceJoined;
                existingEmployee.SocialInsuranceJoinDate = employee.SocialInsuranceJoinDate;
                existingEmployee.IsEmploymentInsuranceJoined = employee.IsEmploymentInsuranceJoined;
                existingEmployee.EmploymentInsuranceJoinDate = employee.EmploymentInsuranceJoinDate;
            }
        }

        // 社員情報を削除する
        public void DeleteEmployee(string employeeNumber)
        {
            var employee = _employees.Find(e => e.EmployeeNumber == employeeNumber);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
    }
}