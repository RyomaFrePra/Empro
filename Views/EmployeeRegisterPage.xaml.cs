using System;
using System.Windows;
using System.Windows.Controls;
using Empro.Models;

namespace Empro.Views
{
    public partial class EmployeeRegisterPage : Page
    {
        public EmployeeRegisterPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("必須項目を入力してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newEmployee = new Employee
                {
                    EmployeeNumber = EmployeeNumberTextBox.Text,
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Kana = KanaTextBox.Text,
                    Gender = (Gender)Enum.Parse(typeof(Gender), ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString()),
                    Department = (DepartmentType)Enum.Parse(typeof(DepartmentType), ((ComboBoxItem)DepartmentComboBox.SelectedItem).Content.ToString()),
                    EmploymentType = (EmploymentType)Enum.Parse(typeof(EmploymentType), ((ComboBoxItem)EmploymentTypeComboBox.SelectedItem).Content.ToString()),
                    BirthDate = BirthDatePicker.SelectedDate,
                    JoinDate = JoinDatePicker.SelectedDate,
                    ResignDate = ResignDatePicker.SelectedDate,
                    TaxAddress = TaxAddressTextBox.Text,
                    ResidentTaxCategory = (TaxCategory)Enum.Parse(typeof(TaxCategory), ((ComboBoxItem)ResidentTaxCategoryComboBox.SelectedItem).Content.ToString()),
                    Notes = NotesTextBox.Text,
                    IsSocialInsuranceJoined = SocialInsuranceComboBox.SelectedItem.ToString() == "加入",
                    SocialInsuranceJoinDate = SocialInsuranceJoinDatePicker.SelectedDate,
                    IsEmploymentInsuranceJoined = EmploymentInsuranceComboBox.SelectedItem.ToString() == "加入",
                    EmploymentInsuranceJoinDate = EmploymentInsuranceJoinDatePicker.SelectedDate
                };

                // 保存処理を呼び出す (仮の保存メソッド)
                SaveEmployee(newEmployee);

                MessageBox.Show("社員情報を保存しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            EmployeeNumberTextBox.Text = string.Empty;
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            KanaTextBox.Text = string.Empty;
            GenderComboBox.SelectedIndex = -1;
            DepartmentComboBox.SelectedIndex = -1;
            EmploymentTypeComboBox.SelectedIndex = -1;
            BirthDatePicker.SelectedDate = null;
            JoinDatePicker.SelectedDate = null;
            ResignDatePicker.SelectedDate = null;
            TaxAddressTextBox.Text = string.Empty;
            ResidentTaxCategoryComboBox.SelectedIndex = -1;
            NotesTextBox.Text = string.Empty;
            SocialInsuranceComboBox.SelectedIndex = -1;
            SocialInsuranceJoinDatePicker.SelectedDate = null;
            EmploymentInsuranceComboBox.SelectedIndex = -1;
            EmploymentInsuranceJoinDatePicker.SelectedDate = null;
        }

        private void SaveEmployee(Employee employee)
        {
            // データベースに保存する処理を実装
        }
    }
}