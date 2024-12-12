using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Empro.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Furigana { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string EmploymentType { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? RetirementDate { get; set; }
        public string Address { get; set; }
        public string ResidentTaxType { get; set; }
        public string Notes { get; set; }
        public string SocialInsuranceStatus { get; set; }
        public DateTime? SocialInsuranceDate { get; set; }
        public string EmploymentInsuranceStatus { get; set; }
        public DateTime? EmploymentInsuranceDate { get; set; }
    }
}
