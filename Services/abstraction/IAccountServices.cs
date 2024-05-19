using BusinessObject;

namespace Services.abstraction;

public interface IAccountServices
{
    public Task<SystemAccount?> Login(string username, string password);

    public Task<IEnumerable<SystemAccount>> GetAllAccount();
    
    public Task<SystemAccount?> GetAccountById(short id);

    public Task<SystemAccount?> AddAccount(SystemAccount account);

    public Task<SystemAccount?> UpdateAccount(SystemAccount account);

    public Task<bool> DeleteAccount(SystemAccount account);
}