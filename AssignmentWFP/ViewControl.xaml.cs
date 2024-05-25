using System.ComponentModel;
using System.Windows;
using BusinessObject;

namespace AssignmentWFP;

public partial class ViewControl : Window
{
    private readonly Lazy<Account> _accountView;
    private readonly Lazy<NewsArticleView> _newsArticleView;
    private readonly Lazy<Login> _login;

    public ViewControl(Func<Account> accountViewFunc, Func<NewsArticleView> newsArticleViewFunc, Func<Login> loginFunc)
    {
        _accountView = new Lazy<Account>(accountViewFunc);
        _newsArticleView = new Lazy<NewsArticleView>(newsArticleViewFunc);
        _login = new Lazy<Login>(loginFunc);
        InitializeComponent();
    }

    private void LoadData()
    {
        switch (StaticUserLogin.UserLogin?.AccountRole)
        {
            case 1:
                BtnAccountView.Visibility = Visibility.Collapsed;
                break;
            case 3:
                BtnProfile.Visibility = Visibility.Collapsed;
                BtnNews.Visibility = Visibility.Collapsed;
                break;
            case 2:
                BtnAccountView.Visibility = Visibility.Collapsed;
                break;
        }
    }

    private void BtnNews_OnClick(object sender, RoutedEventArgs e)
    {
        _newsArticleView.Value.Show();
        this.Close();
    }

    private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
    {
        StaticUserLogin.UserLogin = null;
        _login.Value.Show();
        this.Close();
    }

    private void ViewControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        LoadData();
    }

    private void ViewControl_OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        this.Visibility = Visibility.Hidden;
    }
}