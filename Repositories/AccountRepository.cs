using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.abstraction;

namespace Repositories;

public class AccountRepository : IAccountRepository
{
    public async Task<IEnumerable<SystemAccount>> GetAllAccount()
        => await AccountDao.GetAll()
            .Include(a => a.NewsArticles)
            .ToListAsync();

    public async Task<SystemAccount?> GetAccountById(short id)
        => await AccountDao.GetByIdAsync(id);

    public Task<SystemAccount?> GetAccountByUsernameAndPassword(string username, string password)
        => AccountDao.GetAccountByUsernameAndPassword(username, password);

    public Task<SystemAccount?> GetAccountByUsernameAndPasswordRazor(string username, string password)
        => AccountDao.GetAccountByUsernameAndPasswordRazor(username, password);

    public Task<bool> AddAccount(SystemAccount account)
        => AccountDao.AddAsync(account);

    public Task<bool> UpdateAccount(SystemAccount account)
        => AccountDao.UpdateAsync(account);

    public Task<bool> DeleteAccount(SystemAccount account)
        => AccountDao.DeleteAsync(account);
}