using System.ComponentModel;
using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace LeQuocVietWFP;

public partial class UserProfile : Window
{
    private readonly IAccountServices _accountServices;
    private readonly INewsArticleServices _newsArticleServices;
    private readonly Lazy<ViewControl> _viewControl;
    
    public UserProfile(IAccountServices accountServices, Func<ViewControl> viewControlFunc, 
        INewsArticleServices newsArticleServices)
    {
        _accountServices = accountServices;
        _newsArticleServices = newsArticleServices;
        _viewControl = new Lazy<ViewControl>(viewControlFunc);
        InitializeComponent();
        LoadData();
    }

    private async void LoadData()
    {
        if (StaticUserLogin.UserLogin?.AccountId is null) return;
        var user = await _accountServices.GetAccountById(StaticUserLogin.UserLogin.AccountId);
        if (user is null) return;
        
        TxtAccountName.Text = user.AccountName ?? "";
        TxtAccountEmail.Text = user.AccountEmail ?? "";
        TxtAccountRole.Text = user.AccountRole switch
        {
            1 => "Staff",
            2 => "Lecturer",
            _ => ""
        };
        TxtAccountPassword.Password = user.AccountPassword ?? "";
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if (TxtConfirmPassword.Password != TxtAccountPassword.Password && !string.IsNullOrEmpty(TxtConfirmPassword.Password))
        {
            MessageBox.Show("Confirm Password and password is not the same"
                , "Error"
                , MessageBoxButton.OK, MessageBoxImage.Error);
        }
        try
        {
            var account = new SystemAccount
            {
                AccountId = StaticUserLogin.UserLogin!.AccountId,
                AccountName = TxtAccountName.Text,
                AccountEmail = TxtAccountEmail.Text,
                AccountRole = TxtAccountRole.Text switch
                {
                    "Staff" => 1,
                    "Lecturer" => 2,
                    _ => 0
                },
                AccountPassword = TxtAccountPassword.Password
            };
            await _accountServices.UpdateAccount(account);
            LoadData();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void UserProfile_OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        this.Visibility = Visibility.Hidden;
        _viewControl.Value.Show();
    }

    private void BtnNewsHistory_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new NewsHistory(_newsArticleServices);
        dialog.ShowDialog();
    }
}