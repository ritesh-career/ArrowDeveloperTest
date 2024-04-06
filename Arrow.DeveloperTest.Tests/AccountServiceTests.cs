using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Arrow.DeveloperTest.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        private Mock<IAccountDataStore> _mockAccountDataStore;
        private AccountService _accountService;

        [TestInitialize]
        public void Initialize()
        {
            _mockAccountDataStore = new Mock<IAccountDataStore>();
            _accountService = new AccountService(_mockAccountDataStore.Object);
        }

        [TestMethod]
        public void UpdateAccount_SubtractsAmount_From_Balance_CallsDataStore_UpdateAccount_And_SaveChanges()
        {
            // Arrange
            var account = new Account { Balance = 10 };
            var amount = 4;

            // Act
            _accountService.UpdateAccount(account, amount);

            // Assert
            Assert.AreEqual(6, account.Balance);
            _mockAccountDataStore.Verify(ds => ds.UpdateAccount(account), Times.Once);
            _mockAccountDataStore.Verify(ds => ds.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetAccount_Calls_DataStore_GetAccount()
        {
            // Arrange
            var accountNumber = "1";            
            _mockAccountDataStore.Setup(ds => ds.GetAccount(accountNumber)).Returns(It.IsAny<Account>());

            // Act
            _accountService.GetAccount(accountNumber);

            // Assert
            _mockAccountDataStore.Verify(ds => ds.GetAccount(accountNumber), Times.Once);
        }

        [TestMethod]
        public void AddAccount_Calls_Add_AndCalls_SavesChanges()
        {
            // Arrange
            var newAccount = new Account { AccountNumber = "12" };
            _mockAccountDataStore.Setup(ds => ds.GetAccount(newAccount.AccountNumber)).Returns(null as Account);

            // Act
            _accountService.AddAccount(newAccount);

            // Assert
            _mockAccountDataStore.Verify(ds => ds.Add(newAccount), Times.Once);
            _mockAccountDataStore.Verify(ds => ds.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddAccount_WhenAdding_ExistingAccount_Throws_InvalidOperationException()
        {
            // Arrange
            var existingAccount = new Account { AccountNumber = "12" };
            _mockAccountDataStore.Setup(ds => ds.GetAccount(existingAccount.AccountNumber)).Returns(existingAccount);

            // Act
            _accountService.AddAccount(existingAccount);
        }

        [TestMethod]
        public void GetAllAccounts_Returns_AllAccounts()
        {
            // Arrange
            var expectedAccounts = new List<Account> { new Account() { AccountNumber = "1" }, new Account() { AccountNumber = "2" } };
            _mockAccountDataStore.Setup(ds => ds.GetAllAccounts()).Returns(expectedAccounts.AsQueryable());

            // Act
            var result = _accountService.GetAllAccounts();

            // Assert
            CollectionAssert.AreEqual(expectedAccounts, result.ToList());
        }
    }
}