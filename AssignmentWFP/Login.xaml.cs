using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace AssignmentWFP;

public partial class Login : Window
{
    private readonly IAccountServices _accountServices;
    private readonly Account _accountView;
    private readonly NewsArticleView _newsArticleView;

    public Login(IAccountServices accountServices, Account accountView, NewsArticleView newsArticle)
    {
        _accountServices = accountServices;
        _accountView = accountView;
        _newsArticleView = newsArticle;
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
        if(user.AccountRole == 1)
        {

        }

        this.Dispatcher.Invoke(() =>
            {
                BtnLogin.IsEnabled = true; 
                _accountView.Show();
                this.Close();
            });
        
    }

    private void BtnViewNews_Click(object sender, RoutedEventArgs e)
    {
        _newsArticleView.Show();
        this.Close();
    }
}