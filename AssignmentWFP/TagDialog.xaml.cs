using System.Windows;
using BusinessObject;
using Services.abstraction;

namespace AssignmentWFP;

public partial class TagDialog : Window
{
    private readonly ITagServices _tagServices;
    public TagDialog(ITagServices tagServices)
    {
        _tagServices = tagServices;
        InitializeComponent();
    }

    private async void BtnSave_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var tag = new Tag
            {
                TagName = TxtTagName.Text,
                Note = TxtNote.Text
            };

            await _tagServices.AddTag(tag);
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
        DialogResult = false;
        this.Close();
    }
}