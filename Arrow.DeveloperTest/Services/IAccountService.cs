using Arrow.DeveloperTest.Types;
using System.Linq;

namespace Arrow.DeveloperTest.Services
{
    public interface IAccountService
    {
        void AddAccount(Account account);
        Account GetAccount(string accountNumber);
        IQueryable<Account> GetAllAccounts();
        void UpdateAccount(Account account, decimal amount);    
    }
}
