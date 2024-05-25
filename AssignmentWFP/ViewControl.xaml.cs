using System.ComponentModel;
using System.Windows;
using BusinessObject;

namespace AssignmentWFP;

public partial class ViewControl : Window
{
    private readonly Lazy<Account> _accountView;
    private readonly Lazy<NewsArticleView> _newsArticleView;
    private readonly Lazy<Login> _login;
    private readonly Lazy<CategoryView> _categoryView;
    private readonly Lazy<UserProfile> _userProfile;

    public ViewControl(Func<Account> accountViewFunc, Func<NewsArticleView> newsArticleViewFunc, 
        Func<Login> loginFunc, Func<CategoryView> categoryFunc, Func<UserProfile> userProfileFunc)
    {
        _accountView = new Lazy<Account>(accountViewFunc);
        _newsArticleView = new Lazy<NewsArticleView>(newsArticleViewFunc);
        _login = new Lazy<Login>(loginFunc);
        _categoryView = new Lazy<CategoryView>(categoryFunc);
        _userProfile = new Lazy<UserProfile>(userProfileFunc);
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
                BtnCategory.Visibility = Visibility.Collapsed;
                break;
            case 2:
                BtnAccountView.Visibility = Visibility.Collapsed;
                BtnCategory.Visibility = Visibility.Collapsed;
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

    private void BtnAccountView_OnClick(object sender, RoutedEventArgs e)
    {
        _accountView.Value.Show();
        this.Close();
    }

    private void BtnCategory_OnClick(object sender, RoutedEventArgs e)
    {
        _categoryView.Value.Show();
        this.Close();
    }

    private void BtnProfile_OnClick(object sender, RoutedEventArgs e)
    {
        _userProfile.Value.Show();
        this.Close();
    }
}