using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Empro.Models;

namespace Empro.Views
{
    public partial class EmployeeEditPage : Page
    {
        private string _employeeNumber;

        public EmployeeEditPage(string employeeNumber)
        {
            InitializeComponent();
            _employeeNumber = employeeNumber;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            var employee = EmployeeDataStore.GetEmployee(_employeeNumber);
            if (employee != null)
            {
                EmployeeNumberTextBox.Text = employee.EmployeeNumber;
                FirstNameTextBox.Text = employee.FirstName;
                LastNameTextBox.Text = employee.LastName;
                KanaTextBox.Text = employee.Kana;
                GenderComboBox.SelectedItem = GenderComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == employee.Gender.ToString());
                DepartmentComboBox.SelectedItem = DepartmentComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == employee.Department.ToString());
                EmploymentTypeComboBox.SelectedItem = EmploymentTypeComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == employee.EmploymentType.ToString());
                BirthDatePicker.SelectedDate = employee.BirthDate;
                JoinDatePicker.SelectedDate = employee.JoinDate;
                ResignDatePicker.SelectedDate = employee.ResignDate;
                TaxAddressTextBox.Text = employee.TaxAddress;
                ResidentTaxCategoryComboBox.SelectedItem = ResidentTaxCategoryComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == employee.ResidentTaxCategory.ToString());
                NotesTextBox.Text = employee.Notes;
                SocialInsuranceComboBox.SelectedItem = SocialInsuranceComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == (employee.IsSocialInsuranceJoined ? "Joined" : "NotJoined"));
                SocialInsuranceJoinDatePicker.SelectedDate = employee.SocialInsuranceJoinDate;
                EmploymentInsuranceComboBox.SelectedItem = EmploymentInsuranceComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == (employee.IsEmploymentInsuranceJoined ? "Joined" : "NotJoined"));
                EmploymentInsuranceJoinDatePicker.SelectedDate = employee.EmploymentInsuranceJoinDate;
                SocialInsuranceLeaveDatePicker.SelectedDate = employee.SocialInsuranceLeaveDate; // 社保脱退日
                EmploymentInsuranceLeaveDatePicker.SelectedDate = employee.EmploymentInsuranceLeaveDate; // 雇用保険脱退日
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee
            {
                EmployeeNumber = EmployeeNumberTextBox.Text,
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Kana = KanaTextBox.Text,
                Gender = GenderComboBox.SelectedItem != null ? (Gender)Enum.Parse(typeof(Gender), ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString()) : Gender.Male,
                Department = DepartmentComboBox.SelectedItem != null ? (DepartmentType)Enum.Parse(typeof(DepartmentType), ((ComboBoxItem)DepartmentComboBox.SelectedItem).Content.ToString()) : DepartmentType.Royal,
                EmploymentType = EmploymentTypeComboBox.SelectedItem != null ? (EmploymentType)Enum.Parse(typeof(EmploymentType), ((ComboBoxItem)EmploymentTypeComboBox.SelectedItem).Content.ToString()) : EmploymentType.FullTime,
                BirthDate = BirthDatePicker.SelectedDate,
                JoinDate = JoinDatePicker.SelectedDate,
                ResignDate = ResignDatePicker.SelectedDate,
                TaxAddress = TaxAddressTextBox.Text,
                ResidentTaxCategory = ResidentTaxCategoryComboBox.SelectedItem != null ? (TaxCategory)Enum.Parse(typeof(TaxCategory), ((ComboBoxItem)ResidentTaxCategoryComboBox.SelectedItem).Content.ToString()) : TaxCategory.Ordinary,
                Notes = NotesTextBox.Text,
                IsSocialInsuranceJoined = SocialInsuranceComboBox.SelectedItem != null && ((ComboBoxItem)SocialInsuranceComboBox.SelectedItem).Content.ToString() == "Joined",
                SocialInsuranceJoinDate = SocialInsuranceJoinDatePicker.SelectedDate,
                IsEmploymentInsuranceJoined = EmploymentInsuranceComboBox.SelectedItem != null && ((ComboBoxItem)EmploymentInsuranceComboBox.SelectedItem).Content.ToString() == "Joined",
                EmploymentInsuranceJoinDate = EmploymentInsuranceJoinDatePicker.SelectedDate,
                SocialInsuranceLeaveDate = SocialInsuranceLeaveDatePicker.SelectedDate, // 社保脱退日
                EmploymentInsuranceLeaveDate = EmploymentInsuranceLeaveDatePicker.SelectedDate // 雇用保険脱退日
            };

            EmployeeDataStore.UpdateEmployee(employee);
            MessageBox.Show("社員情報を更新しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDataStore.DeleteEmployee(_employeeNumber);
            MessageBox.Show("社員情報を削除しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage());
        }
    }
}
