using System;

namespace Empro.Models
{
    public class Employee
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Kana { get; set; }
        public Gender Gender { get; set; }
        public DepartmentType Department { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public string TaxAddress { get; set; }
        public TaxCategory ResidentTaxCategory { get; set; }
        public string Notes { get; set; }
        public bool IsSocialInsuranceJoined { get; set; }
        public DateTime? SocialInsuranceJoinDate { get; set; }
        public bool IsEmploymentInsuranceJoined { get; set; }
        public DateTime? EmploymentInsuranceJoinDate { get; set; }
        public DateTime? SocialInsuranceLeaveDate { get; set; } // 社保脱退日
        public DateTime? EmploymentInsuranceLeaveDate { get; set; } // 雇用保険脱退日
    }


    public enum Gender
    {
        Male, // 男
        Female // 女
    }

    public enum DepartmentType
    {
        Royal, // ロイヤル
        Annex, // アネックス
        SalaGujo // サーラ郡上
    }

    public enum EmploymentType
    {
        Executive, // 役員
        FullTime, // 正社員
        PartTime // パート
    }

    public enum TaxCategory
    {
        Ordinary, // 普通徴収
        Special // 特別徴収
    }
}