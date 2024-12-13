using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Empro.Models;

namespace Empro.Views
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            LoadEmployeeData();
            InitializeFilters();
        }

        private void LoadEmployeeData()
        {
            var employees = EmployeeDataStore.GetEmployees();
            EmployeeGrid.ItemsSource = employees;
            UpdateEmployeeCount(employees.Count);
        }

        private void InitializeFilters()
        {
            var employmentTypes = new List<string> { "All" };
            employmentTypes.AddRange(Enum.GetValues(typeof(EmploymentType)).Cast<EmploymentType>().Select(e => e.ToString()));
            EmploymentTypeFilter.ItemsSource = employmentTypes;

            var genders = new List<string> { "All" };
            genders.AddRange(Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(g => g.ToString()));
            GenderFilter.ItemsSource = genders;

            SocialInsuranceFilter.ItemsSource = new List<string> { "All", "Joined", "NotJoined" };

            var departments = new List<string> { "All" };
            departments.AddRange(Enum.GetValues(typeof(DepartmentType)).Cast<DepartmentType>().Select(d => d.ToString()));
            DepartmentFilter.ItemsSource = departments;

            EmploymentInsuranceFilter.ItemsSource = new List<string> { "All", "Joined", "NotJoined" };
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            var employees = EmployeeDataStore.GetEmployees();

            if (EmploymentTypeFilter.SelectedItem != null && EmploymentTypeFilter.SelectedItem.ToString() != "All")
            {
                var selectedEmploymentType = (EmploymentType)Enum.Parse(typeof(EmploymentType), EmploymentTypeFilter.SelectedItem.ToString());
                employees = employees.Where(e => e.EmploymentType == selectedEmploymentType).ToList();
            }

            if (GenderFilter.SelectedItem != null && GenderFilter.SelectedItem.ToString() != "All")
            {
                var selectedGender = (Gender)Enum.Parse(typeof(Gender), GenderFilter.SelectedItem.ToString());
                employees = employees.Where(e => e.Gender == selectedGender).ToList();
            }

            if (SocialInsuranceFilter.SelectedItem != null && SocialInsuranceFilter.SelectedItem.ToString() != "All")
            {
                var selectedSocialInsurance = SocialInsuranceFilter.SelectedItem.ToString();
                if (selectedSocialInsurance == "Joined")
                {
                    employees = employees.Where(e => e.IsSocialInsuranceJoined).ToList();
                }
                else if (selectedSocialInsurance == "NotJoined")
                {
                    employees = employees.Where(e => !e.IsSocialInsuranceJoined).ToList();
                }
            }

            if (DepartmentFilter.SelectedItem != null && DepartmentFilter.SelectedItem.ToString() != "All")
            {
                var selectedDepartment = (DepartmentType)Enum.Parse(typeof(DepartmentType), DepartmentFilter.SelectedItem.ToString());
                employees = employees.Where(e => e.Department == selectedDepartment).ToList();
            }

            if (EmploymentInsuranceFilter.SelectedItem != null && EmploymentInsuranceFilter.SelectedItem.ToString() != "All")
            {
                var selectedEmploymentInsurance = EmploymentInsuranceFilter.SelectedItem.ToString();
                if (selectedEmploymentInsurance == "Joined")
                {
                    employees = employees.Where(e => e.IsEmploymentInsuranceJoined).ToList();
                }
                else if (selectedEmploymentInsurance == "NotJoined")
                {
                    employees = employees.Where(e => !e.IsEmploymentInsuranceJoined).ToList();
                }
            }

            EmployeeGrid.ItemsSource = employees;
            UpdateEmployeeCount(employees.Count);
        }

        private void UpdateEmployeeCount(int count)
        {
            EmployeeCountLabel.Content = $"該当社員数：{count}人";
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var employeeNumber = button.Tag.ToString();
                NavigationService.Navigate(new EmployeeEditPage(employeeNumber));
            }
        }
    }
}

