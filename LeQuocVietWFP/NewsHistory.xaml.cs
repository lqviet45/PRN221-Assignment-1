using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using BusinessObject;
using Services.abstraction;

namespace LeQuocVietWFP;

public partial class NewsHistory : Window
{
    private readonly INewsArticleServices _newsArticleServices;
    public NewsHistory(INewsArticleServices newsArticleServices)
    {
        _newsArticleServices = newsArticleServices;
        InitializeComponent();
    }

    private async Task LoadData()
    {
        var id = StaticUserLogin.UserLogin?.AccountId;
        
        if (id is null) return;
        
        DgNewsArticle.ItemsSource = await _newsArticleServices
            .GetArticlesByUserId((short)id);
    }
    
    private void DgNewsArticle_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            var selectedNews = (NewsArticle)DgNewsArticle.SelectedItem;
            if (selectedNews == null) return;

            TxtNewsTitle.Text = selectedNews.NewsArticleId;
            TxtNewsArticleId.Text = selectedNews.NewsTitle ?? "";
            TxtNewsContent.Text = selectedNews.NewsContent ?? "";
            TxtCategory.Text = selectedNews.Category?.CategoryName ?? "";
            ChkNewsStatus.IsChecked = selectedNews.NewsStatus;
            LbTags.ItemsSource = selectedNews.Tags;
            TxtCreateBy.Text = selectedNews.CreatedBy?.AccountName ?? "";
            TxtModifiedDate.Text = selectedNews.ModifiedDate.ToString() ?? "";
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private async void NewsHistory_OnLoaded(object sender, RoutedEventArgs e)
    {
        await LoadData();
    }

    private void NewsHistory_OnClosing(object? sender, CancelEventArgs e)
    {
        DialogResult = false;
    }
}