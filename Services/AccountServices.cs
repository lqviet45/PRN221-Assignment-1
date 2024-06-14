using BusinessObject;
using Microsoft.Extensions.Configuration;
using Repositories.abstraction;
using Services.abstraction;

namespace Services;

public class AccountServices : IAccountServices
{
    private readonly IAccountRepository _accountRepository;
    private readonly INewsArticleServices _newsArticleServices;
    private readonly IConfiguration _configuration;

    public AccountServices(IAccountRepository accountRepository, INewsArticleServices newsArticleServices, IConfiguration configuration)
    {
        _accountRepository = accountRepository;
        _newsArticleServices = newsArticleServices;
        _configuration = configuration;
    }

    public async Task<SystemAccount?> Login(string username, string password)
    {
        return await _accountRepository.GetAccountByUsernameAndPassword(username, password).ConfigureAwait(false);
    }
    
    public async Task<SystemAccount> LoginRazor(string username, string password)
    {
        var adminUsername = _configuration.GetSection("AdminAccount:Username").Value;
        var adminPassword = _configuration.GetSection("AdminAccount:Password").Value;
        
        if (username == adminUsername && password == adminPassword)
        {
            return new SystemAccount()
            {
                AccountEmail = username,
                AccountName = "admin",
                AccountRole = 3
            };
        }
        
        var account = await _accountRepository.GetAccountByUsernameAndPasswordRazor(username, password).ConfigureAwait(false);

        if (account is null)
        {
            throw new ArgumentException("Username or password is incorrect!!");
        }
        
        return account;
    }

    public async Task<IEnumerable<SystemAccount>> GetAllAccount()
    {
        return await _accountRepository.GetAllAccount();
    }

    public async Task<SystemAccount?> GetAccountById(short id)
    {
        return await _accountRepository.GetAccountById(id).ConfigureAwait(false);
    }

    public async Task<SystemAccount?> AddAccount(SystemAccount account)
    {
        var isSuccess = await _accountRepository.AddAccount(account).ConfigureAwait(false);

        return isSuccess ? account : null;
    }

    public async Task<SystemAccount?> UpdateAccount(SystemAccount account)
    {
        var existAccount = await _accountRepository.GetAccountById(account.AccountId);

        if (existAccount is null)
        {
            throw new ArgumentNullException(nameof(existAccount), "Account doesn't exist to update!!");
        }

        existAccount.AccountName = account.AccountName;
        existAccount.AccountRole = account.AccountRole;
        existAccount.AccountPassword = account.AccountPassword;
        
        var isSuccess = await _accountRepository.UpdateAccount(account).ConfigureAwait(false);

        return isSuccess ? account : null;
    }

    public async Task<bool> DeleteAccount(short id)
    {
        var existAccount = await _accountRepository.GetAccountById(id).ConfigureAwait(false);

        if (existAccount is null)
        {
            throw new ArgumentNullException(nameof(existAccount), "Account doesn't exist to delete!!");
        }
        await _newsArticleServices.UpdateArticlesWhenDeleteAccount(existAccount.NewsArticles.ToList())
            .ConfigureAwait(false);
        
        var isSuccess = await _accountRepository.DeleteAccount(existAccount).ConfigureAwait(false);

        return isSuccess;
    }
}