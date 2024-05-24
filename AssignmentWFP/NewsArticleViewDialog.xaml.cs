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
    private readonly ITagServices _tagServices;
    public NewsArticleViewDialog(INewsArticleServices articleServices, IAccountServices accountServices, ICategoryServices categoryServices, ITagServices tagServices, string? newsArticleId = null)
    {
        _newsArticleServices = articleServices;
        _accountServices = accountServices;
        _categoryServices = categoryServices;
        _tagServices = tagServices;
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
        var tags = await _tagServices.GetAllTag();
        LbTags.ItemsSource = tags.Select(tag => new TagSelection
        {
            TagId = tag.TagId,
            TagName = tag.TagName
        }).ToList();
        
        if (article is null) return;
        
        TxtNewsArticleId.Text = article.NewsArticleId;
        TxtNewsTitle.Text = article.NewsTitle ?? "";
        TxtNewsContent.Text = article.NewsContent ?? "";
        CbCategory.Text = article.Category?.CategoryName ?? "";
        ChkNewsStatus.IsChecked = article.NewsStatus;
        CbCreatedBy.Text = article.CreatedBy?.AccountName ?? "";
        TxtModifiedDate.Text = article.ModifiedDate.ToString() ?? "";
        foreach (var tag in article.Tags)
        {
            var item = LbTags.Items.OfType<TagSelection>().FirstOrDefault(t => t.TagId == tag.TagId);
            if (item != null)
            {
                item.IsSelected = true;
            }
        }
    }
    
    
    private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var selectedCategory = (Category)CbCategory.SelectedItem;
            var selectedAccount = (SystemAccount)CbCreatedBy.SelectedItem;
            var selectedTags = LbTags.Items.OfType<TagSelection>()
                .Where(t => t.IsSelected)
                .Select(t => new Tag { TagId = t.TagId, TagName = t.TagName })
                .ToList();

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
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
        
        DialogResult = true;
        this.Close();
    }

    private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        this.Close();
    }
    
    public class TagSelection
    {
        public int TagId { get; set; }
        public string? TagName { get; set; }
        public bool IsSelected { get; set; }
    }

    private void BtnAddTag_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new TagDialog(_tagServices);
        if (dialog.ShowDialog() == true) {
            LoadData();        
        }
    }
}