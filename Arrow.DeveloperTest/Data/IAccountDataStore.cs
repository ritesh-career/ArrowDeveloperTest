using Arrow.DeveloperTest.Types;
using System.Linq;

namespace Arrow.DeveloperTest.Data
{
    public interface IAccountDataStore
    {
        IQueryable<Account> GetAllAccounts();
        void Add(Account account);
        Account GetAccount(string accountNumber);
        void UpdateAccount(Account account);
        void SaveChanges();
    }
}