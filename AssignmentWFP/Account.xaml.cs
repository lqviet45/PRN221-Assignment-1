using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Services.abstraction;

namespace AssignmentWFP
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        private readonly IAccountServices _accountServices;

        public Account(IAccountServices accountServices)
        {
            _accountServices = accountServices;
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            var accountList = await _accountServices.GetAllAccount();
            DgAccount.ItemsSource = accountList.Select(a => new
            {
                a.AccountId,
                a.AccountName,
                a.AccountEmail,
                a.AccountRole
            });
            CbRole.ItemsSource = new List<string>
            {
                "Staff",
                "Lecturer"
            };
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to delete this user!!",
                "Delete Account",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                var id = short.Parse(TxtId.Text);
                var isSuccess = await _accountServices.DeleteAccount(id);
                if (isSuccess)
                {
                    MessageBox.Show("Delete Success",
                        "",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
            
                MessageBox.Show("Delete Fail, System Error",
                    "",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AccountDialog(_accountServices);
            if (dialog.ShowDialog() == true)
            {
                LoadData();
            }
        }
        
        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAccount = (dynamic)DgAccount.SelectedItem;
            if (selectedAccount != null)
            {
                var dialog = new AccountDialog(_accountServices, selectedAccount.AccountId);
                if (dialog.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select an account to update.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DgAccount_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedAccount = (dynamic)DgAccount.SelectedItem;
                if (selectedAccount == null) return;
                
                TxtId.Text = selectedAccount.AccountId.ToString();
                TxtUsername.Text = selectedAccount.AccountName ?? "";
                TxtEmail.Text = selectedAccount.AccountEmail ?? "";
                //CbRole.SelectedItem = selectedAccount.AccountRole.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}