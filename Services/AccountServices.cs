using BusinessObject;
using Repositories.abstraction;
using Services.abstraction;

namespace Services;

public class AccountServices : IAccountServices
{
    private readonly IAccountRepository _accountRepository;

    public AccountServices(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<SystemAccount?> Login(string username, string password)
    {
        return await _accountRepository.GetAccountByUsernameAndPassword(username, password).ConfigureAwait(false);
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
        var existAccount = await _accountRepository.GetAccountById(account.AccountId).ConfigureAwait(false);

        if (existAccount is null)
        {
            throw new ArgumentNullException(nameof(account), "Account doesn't exist to update!!");
        }

        existAccount.AccountName = account.AccountName;
        existAccount.AccountRole = account.AccountRole;

        var isSuccess = await _accountRepository.UpdateAccount(existAccount).ConfigureAwait(false);

        return isSuccess ? existAccount : null;
    }

    public async Task<bool> DeleteAccount(SystemAccount account)
    {
        var existAccount = await _accountRepository.GetAccountById(account.AccountId).ConfigureAwait(false);

        if (existAccount is null)
        {
            throw new ArgumentNullException(nameof(account), "Account doesn't exist to delete!!");
        }
        //TODO: change News author before delete account
        
        var isSuccess = await _accountRepository.DeleteAccount(existAccount).ConfigureAwait(false);

        return isSuccess;
    }
}