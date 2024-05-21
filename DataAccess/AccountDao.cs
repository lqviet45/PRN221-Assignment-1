using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public static class AccountDao
{
    public static async Task<SystemAccount?> GetAccountByUsernameAndPassword(string username, string password)
    {
        var config = GetConfiguration();

        var adminUsername = config.GetSection("AdminAccount:Username").Value;
        var adminPassword = config.GetSection("AdminAccount:Password").Value;

        if (username == adminUsername && password == adminPassword)
        {
            return new SystemAccount()
            {
                AccountEmail = username,
                AccountName = "admin",
                AccountRole = 3
            };
        }
        var context = new FunewsManagementDbContext();
        var userAccount = await context.SystemAccounts
            .Where(a => a.AccountEmail == username && a.AccountPassword == password)
            .FirstOrDefaultAsync();

        return userAccount;
    }

    public static IQueryable<SystemAccount> GetAll()
    {
        var context = new FunewsManagementDbContext();

        return context.SystemAccounts.AsQueryable();
    }

    public static async Task<SystemAccount?> GetByIdAsync(short id)
    {
        var context = new FunewsManagementDbContext();

        return await context.SystemAccounts
            .Include(a => a.NewsArticles)
            .SingleOrDefaultAsync(a => a.AccountId == id);
    }

    public static async Task<bool> AddAsync(SystemAccount systemAccount)
    {
        var context = new FunewsManagementDbContext();
        context.Add(systemAccount);

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> UpdateAsync(SystemAccount systemAccount)
    {
        var context = new FunewsManagementDbContext();
        var existAccount = await context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountId == systemAccount.AccountId);

        if (existAccount is null)
        {
            throw new ArgumentNullException(nameof(systemAccount), "Account doesn't exist to update!!");
        }

        existAccount.AccountName = systemAccount.AccountName;
        existAccount.AccountRole = systemAccount.AccountRole;
        existAccount.AccountPassword = systemAccount.AccountPassword;

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> DeleteAsync(SystemAccount systemAccount)
    {
        var context = new FunewsManagementDbContext();
        context.Remove(systemAccount);

        return await context.SaveChangesAsync() > 0;
    }
    
    private static IConfiguration GetConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        return config;
    }
}