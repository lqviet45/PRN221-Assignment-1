using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace LeQuocVietWFP;

public partial class CategoryView : Window
{
    private readonly ICategoryServices _categoryServices;
    private readonly Lazy<ViewControl> _viewControl;
    public CategoryView(ICategoryServices categoryServices, Func<ViewControl> viewControlFunc)
    {
        _categoryServices = categoryServices;
        _viewControl = new Lazy<ViewControl>(viewControlFunc);
        InitializeComponent();
        DataContext = this;
        LoadCategories();
    }
    
    private async void LoadCategories()
    {
        var categories = await _categoryServices.GetAllCategory();
        DataGridCategories.ItemsSource = categories;
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new CategoryDialog(_categoryServices);
        if (dialog.ShowDialog() == true)
        {
            LoadCategories();
        }
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        var selected = (Category)DataGridCategories.SelectedItem;
        if (selected == null)
        {
            MessageBox.Show("Please select a category to edit.");
            return;
        }
        var dialog = new CategoryDialog(_categoryServices, selected.CategoryId);
        if (dialog.ShowDialog() == true)
        {
            LoadCategories();
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        var selected = (Category)DataGridCategories.SelectedItem;
        if (selected == null)
        {
            MessageBox.Show("Please select a category to delete.");
            return;
        }

        if (MessageBox.Show("Are you sure you want to delete this ?", "Delete Confirmation",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

        await _categoryServices.DeleteCategory(selected.CategoryId);
        LoadCategories();
    }

    private void CategoryView_OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        this.Visibility = Visibility.Hidden;
        _viewControl.Value.Show();
    }
}