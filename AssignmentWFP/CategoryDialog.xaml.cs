using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace AssignmentWFP;

public partial class CategoryDialog : Window
{
    private readonly ICategoryServices _categoryServices;
    private readonly short? _categoryId;
    public CategoryDialog(ICategoryServices categoryServices, short? categoryId = null)
    {
        _categoryServices = categoryServices;
        _categoryId = categoryId;
        InitializeComponent();
        LoadData();
    }

    private async void LoadData()
    {
        if (_categoryId is null) return;
        var category = await _categoryServices.GetCategoryById((short)_categoryId);
        if (category is null) return;
        TxtCategoryId.Text = category.CategoryId.ToString();
        TxtCategoryName.Text = category.CategoryName;
        TxtCategoryDescription.Text = category.CategoryDesciption;
    }

    private async void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var category = new Category
            {
                CategoryName = TxtCategoryName.Text,
                CategoryDesciption = TxtCategoryDescription.Text
            };

            if (_categoryId is null)
            {
                await _categoryServices.AddCategory(category);
            }
            else
            {
                category.CategoryId = short.Parse(TxtCategoryId.Text);
                await _categoryServices.UpdateCategory(category);
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

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        this.Close();
    }
}