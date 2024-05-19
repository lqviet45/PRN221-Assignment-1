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
        }

        private void Account_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            var accountList = await _accountServices.GetAllAccount();
            DgAccount.ItemsSource = accountList.Select(a => new {
                a.AccountName,
                a.AccountEmail,
                a.AccountRole
            });
            CbRole.ItemsSource = new List<string>
            {
                "Manage",
                "User"
            };
        }
    }
}
