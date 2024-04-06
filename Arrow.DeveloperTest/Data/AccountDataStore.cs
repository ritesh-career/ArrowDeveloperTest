using Arrow.DeveloperTest.Types;
using System.Linq;

namespace Arrow.DeveloperTest.Data
{
    public class AccountDataStore : IAccountDataStore
    {
        private readonly MainDbContext _db;

        public AccountDataStore(MainDbContext db)
        {
            _db = db;
        }

        public void Add(Account account)
        {
            _db.Accounts.Add(account);
        }

        public IQueryable<Account> GetAllAccounts()
        {
            return _db.Accounts;
        }

        public Account GetAccount(string accountNumber)
        {
            return _db.Accounts.SingleOrDefault(x => x.AccountNumber.Equals(accountNumber, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public void UpdateAccount(Account account)
        {
            _db.Accounts.Update(account);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
