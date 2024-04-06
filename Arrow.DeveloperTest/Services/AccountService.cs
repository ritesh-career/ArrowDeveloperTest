using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStore _accountDataStore;

        public AccountService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore;
        }

        public Account GetAccount(string accountNumber)
        {
            return _accountDataStore.GetAccount(accountNumber);
        }

        public void UpdateAccount(Account account, decimal amount)
        {
            account.Balance -= amount;            
            _accountDataStore.UpdateAccount(account);
            _accountDataStore.SaveChanges();
        }

        public void AddAccount(Account account)
        {     
            var result = this.GetAccount(account.AccountNumber);

            if (result != null)
            {
                throw new InvalidOperationException("Account already exists");
            }

            _accountDataStore.Add(account);
            _accountDataStore.SaveChanges();
        }

        public IQueryable<Account> GetAllAccounts()
        {
            return _accountDataStore.GetAllAccounts();
        }
    }
}
