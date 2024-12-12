using System;
using System.Collections.Generic;
using Empro.Data;
using Empro.Models;

namespace Empro.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService()
        {
            _repository = new EmployeeRepository();  // EmployeeRepository のインスタンスを作成
        }

        // 社員情報を取得する
        public List<Employee> GetAllEmployees() => _repository.GetAllEmployees();

        // 新しい社員を登録する
        public void RegisterEmployee(Employee employee)
        {
            _repository.AddEmployee(employee);  // EmployeeRepository を通して社員を追加
        }
    }
}