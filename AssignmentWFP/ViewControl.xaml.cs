using System.ComponentModel;
using System.Windows;
using BusinessObject;

namespace AssignmentWFP;

public partial class ViewControl : Window
{
    private readonly Account _accountView;
    private readonly NewsArticleView _newsArticleView;
    private readonly Login _login;

    public ViewControl(Account accountView, NewsArticleView newsArticleView, Login login)
    {
        _accountView = accountView;
        _newsArticleView = newsArticleView;
        _login = login;
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
        _newsArticleView.ShowDialog();
        this.Hide();
    }

    private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
    {
        StaticUserLogin.UserLogin = null;
        _login.Show();
        this.Close();
    }

    private void ViewControl_OnLoaded(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}