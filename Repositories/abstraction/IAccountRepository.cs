using BusinessObject;

namespace Repositories.abstraction;

public interface IAccountRepository
{
    public Task<IEnumerable<SystemAccount>> GetAllAccount();
    public Task<SystemAccount?> GetAccountById(short id);

    public Task<SystemAccount?> GetAccountByUsernameAndPassword(string username, string password);

    public Task<bool> AddAccount(SystemAccount category);

    public Task<bool> UpdateAccount(SystemAccount category);

    public Task<bool> DeleteAccount(SystemAccount category);
}