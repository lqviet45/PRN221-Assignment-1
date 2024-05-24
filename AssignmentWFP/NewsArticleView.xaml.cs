using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Services.abstraction;

namespace AssignmentWFP
{
    /// <summary>
    /// Interaction logic for NewsArticle.xaml
    /// </summary>
    public partial class NewsArticleView : Window
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly IAccountServices _accountServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ITagServices _tagServices;
        public NewsArticleView(INewsArticleServices newsArticleServices, IAccountServices accountServices, ICategoryServices categoryServices, ITagServices tagServices)
        {
            _newsArticleServices = newsArticleServices;
            _accountServices = accountServices;
            _categoryServices = categoryServices;
            _tagServices = tagServices;
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            DgNewsArticle.ItemsSource = await _newsArticleServices.GetAllArticle();
        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (StaticUserLogin.UserLogin?.AccountRole != 1)
            {
                MessageBox.Show("You don't have permission to do this!!!",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dialog = new NewsArticleViewDialog(_newsArticleServices, _accountServices, _categoryServices, _tagServices, TxtNewsArticleId.Text);
            if (dialog.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (StaticUserLogin.UserLogin?.AccountRole != 1)
            {
                MessageBox.Show("You don't have permission to do this!!!",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedNews = (NewsArticle)DgNewsArticle.SelectedItem;
            if (selectedNews == null)
            {
                MessageBox.Show("Please select a news article to update.");
                return;
            }

            var dialog = new NewsArticleViewDialog(_newsArticleServices, _accountServices, _categoryServices, _tagServices, selectedNews.NewsArticleId);
            if (dialog.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (StaticUserLogin.UserLogin?.AccountRole != 1)
            {
                MessageBox.Show("You don't have permission to do this!!!",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedNews = (NewsArticle)DgNewsArticle.SelectedItem;
            if (selectedNews == null)
            {
                MessageBox.Show("Please select a news article to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this news article?", "Delete Confirmation",
                    MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            
            await _newsArticleServices.DeleteArticle(selectedNews.NewsArticleId);
            LoadData();
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

        private void NewsArticleView_OnClosing(object? sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
