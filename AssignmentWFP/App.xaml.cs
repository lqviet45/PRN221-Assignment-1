using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.abstraction;
using Services;
using Services.abstraction;

namespace AssignmentWFP;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    public App()
    {
        IServiceCollection collection = new ServiceCollection();
        collection.AddSingleton<ICategoryRepository, CategoryRepository>();
        collection.AddSingleton<ICategoryServices, CategoryServices>();
        collection.AddSingleton<IAccountRepository, AccountRepository>();
        collection.AddSingleton<IAccountServices, AccountServices>();
        collection.AddSingleton<Login>();
        collection.AddSingleton<Account>();
        _serviceProvider = collection.BuildServiceProvider();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var login = _serviceProvider.GetRequiredService<Login>();
        login.Show();
    }
}