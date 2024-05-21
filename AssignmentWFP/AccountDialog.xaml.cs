using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace AssignmentWFP;

public partial class AccountDialog : Window
{
    private readonly IAccountServices _accountServices;
    private readonly short? _accountId;

    public AccountDialog(IAccountServices accountServices, short? accountId = null)
    {
        InitializeComponent();
        _accountServices = accountServices;
        _accountId = accountId;
        LoadData();
    }
    
    private async void LoadData()
    {
        CbRole.ItemsSource = new List<string> { "Staff", "Lecturer" };

        if (!_accountId.HasValue) return;
        
        var account = await _accountServices.GetAccountById(_accountId.Value);
        
        if (account is null) return;
        TxtId.Text = _accountId.Value.ToString();
        TxtUsername.Text = account.AccountName ?? "";
        TxtEmail.Text = account.AccountEmail ?? "";
        CbRole.Text = account.AccountRole switch
        {
            1 => "Staff",
            2 => "Lecturer",
            _ => ""
        };
        TxtPassword.Text = account.AccountPassword ?? "";
    }

    private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var account = new SystemAccount
            {
                AccountId = short.Parse(TxtId.Text),
                AccountName = TxtUsername.Text,
                AccountEmail = TxtEmail.Text,
                AccountRole = CbRole.Text switch
                {
                    "Staff" => 1,
                    "Lecturer" => 2,
                    _ => 0
                },
                AccountPassword = TxtPassword.Text
            };

            if (_accountId.HasValue)
            {
                await _accountServices.UpdateAccount(account);
            }
            else
            {
                await _accountServices.AddAccount(account);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        
        DialogResult = true;
        Close();
    }
}