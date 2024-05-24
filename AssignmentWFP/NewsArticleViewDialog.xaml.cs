using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace AssignmentWFP;

public partial class NewsArticleViewDialog : Window
{
    private readonly INewsArticleServices _newsArticleServices;
    private readonly IAccountServices _accountServices;
    private readonly string? _newsArticleId;
    private readonly ICategoryServices _categoryServices;
    public NewsArticleViewDialog(INewsArticleServices articleServices, IAccountServices accountServices, ICategoryServices categoryServices, string? newsArticleId = null)
    {
        _newsArticleServices = articleServices;
        _accountServices = accountServices;
        _categoryServices = categoryServices;
        if (!string.IsNullOrEmpty(newsArticleId))
        {
            _newsArticleId = newsArticleId;
        }
        InitializeComponent();
        LoadData();
    }

    private async void LoadData()
    {
        var article = await _newsArticleServices.GetArticleById(_newsArticleId!);
        CbCreatedBy.ItemsSource = await _accountServices.GetAllAccount();
        CbCategory.ItemsSource = await _categoryServices.GetAllCategory();
        CbCategory.DisplayMemberPath = "CategoryName";
        CbCreatedBy.DisplayMemberPath = "AccountName";
    if (article is null) return;
        TxtNewsArticleId.Text = article.NewsArticleId;
        TxtNewsTitle.Text = article.NewsTitle ?? "";
        TxtNewsContent.Text = article.NewsContent ?? "";
        CbCategory.Text = article.Category?.CategoryName ?? "";
        ChkNewsStatus.IsChecked = article.NewsStatus;
        LbTags.ItemsSource = article.Tags;
        CbCreatedBy.Text = article.CreatedBy?.AccountName ?? "";
        TxtModifiedDate.Text = article.ModifiedDate.ToString() ?? "";
    }
    
    
    private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedCategory = (Category)CbCategory.SelectedItem;
        var selectedAccount = (SystemAccount)CbCreatedBy.SelectedItem;
        var selectedTags = LbTags.SelectedItems.Cast<Tag>().ToList();

        var article = new NewsArticle
        {
            NewsArticleId = TxtNewsArticleId.Text,
            NewsTitle = TxtNewsTitle.Text,
            NewsContent = TxtNewsContent.Text,
            CategoryId = selectedCategory.CategoryId,
            Category = selectedCategory,
            NewsStatus = ChkNewsStatus.IsChecked ?? false,
            CreatedById = selectedAccount.AccountId,
            CreatedBy = selectedAccount,
            ModifiedDate = DateTime.Now,
            Tags = selectedTags
        };

        if (string.IsNullOrEmpty(_newsArticleId))
        {
            await _newsArticleServices.AddArticle(article);
        }
        else
        {
            article.NewsArticleId = _newsArticleId;
            await _newsArticleServices.UpdateArticle(article);
        }
    }

    private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}