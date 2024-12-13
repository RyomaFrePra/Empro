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
                var gender = GenderComboBox.SelectedItem != null ? (Gender)Enum.Parse(typeof(Gender), ((ComboBoxItem)GenderComboBox.SelectedItem).Content.ToString()) : Gender.Male;

                var newEmployee = new Employee
                {
                    EmployeeNumber = EmployeeNumberTextBox.Text,
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Kana = KanaTextBox.Text,
                    Gender = gender,
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
                    EmploymentInsuranceJoinDate = EmploymentInsuranceJoinDatePicker.SelectedDate
                };

                // 保存処理を呼び出す
                EmployeeDataStore.AddEmployee(newEmployee);

                MessageBox.Show("社員情報を保存しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();

                // トップ画面に戻る
                NavigationService.Navigate(new MainPage());
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"無効な値が入力されました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"必須フィールドが未入力です: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
            }
        }
    }
}
