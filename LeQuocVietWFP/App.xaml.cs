using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.abstraction;
using Services;
using Services.abstraction;

namespace LeQuocVietWFP;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;
    public App()
    {
        IServiceCollection collection = new ServiceCollection();
        collection.AddScoped<ICategoryRepository, CategoryRepository>();
        collection.AddSingleton<ICategoryServices, CategoryServices>();
        collection.AddScoped<IAccountRepository, AccountRepository>();
        collection.AddSingleton<IAccountServices, AccountServices>();
        collection.AddScoped<INewsArticleRepository, NewsArticleRepository>();
        collection.AddSingleton<INewsArticleServices, NewsArticleServices>();
        collection.AddScoped<ITagRepository, TagRepository>();
        collection.AddSingleton<ITagServices, TagServices>();
        collection.AddSingleton<Login>();
        collection.AddSingleton<Account>();
        collection.AddSingleton<NewsArticleView>();
        collection.AddSingleton<ViewControl>();
        collection.AddSingleton<CategoryView>();
        collection.AddSingleton<UserProfile>();
        collection.AddSingleton<NewsHistory>();
        
        collection.AddTransient<Func<ViewControl>>(provider => provider.GetRequiredService<ViewControl>);
        collection.AddTransient<Func<NewsArticleView>>(provider => provider.GetRequiredService<NewsArticleView>);
        collection.AddTransient<Func<Account>>(provider => provider.GetRequiredService<Account>);
        collection.AddTransient<Func<Login>>(provider => provider.GetRequiredService<Login>);
        collection.AddTransient<Func<CategoryView>>(provider => provider.GetRequiredService<CategoryView>);
        collection.AddTransient<Func<UserProfile>>(provider => provider.GetRequiredService<UserProfile>);
        collection.AddTransient<Func<NewsHistory>>(provider => provider.GetRequiredService<NewsHistory>);
        
        _serviceProvider = collection.BuildServiceProvider();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var login = _serviceProvider.GetRequiredService<Login>();
        login.Show();
    }
}