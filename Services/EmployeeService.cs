using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _repository = new EmployeeRepository();
        }

        public List<Employee> GetAllEmployees() => _repository.GetEmployees();

        public void RegisterEmployee(Employee employee) => _repository.AddEmployee(employee);
    }
}
