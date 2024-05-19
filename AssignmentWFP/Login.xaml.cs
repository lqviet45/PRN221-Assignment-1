using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace AssignmentWFP;

public partial class Login : Window
{
    private readonly IAccountServices _accountServices;
    private readonly Account _accountView;

    public Login(IAccountServices accountServices, Account accountView)
    {
        _accountServices = accountServices;
        _accountView = accountView;
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        BtnLogin.IsEnabled = false;

        var username = TxtUsername.Text;
        var password = TextPassword.Text;
        var user = await _accountServices.Login(username, password).ConfigureAwait(false);

        if (user is null)
        {
            MessageBox.Show("Login fail, username or password not correct!!",
                "Login Error",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            this.Dispatcher.Invoke(() => { BtnLogin.IsEnabled = true; });

            return;
        }

        StaticUserLogin.UserLogin = user;
        this.Dispatcher.Invoke(() => { BtnLogin.IsEnabled = true; });
        
        
        _accountView.Show();
        this.Close();
    }
}