using Arrow.DeveloperTest.Exceptions;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Arrow.DeveloperTest.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrow.DeveloperTest.Tests
{
    [TestClass]
    public class PaymentServiceTests
    {
        private Mock<IAccountService> _mockAccountService;
        private Mock<IPaymentSchemeValidatorFactory> _mockValidatorFactory;
        private PaymentService _paymentService;

        [TestInitialize]
        public void Initialize()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockValidatorFactory = new Mock<IPaymentSchemeValidatorFactory>();
            _paymentService = new PaymentService(_mockAccountService.Object, _mockValidatorFactory.Object);
        }

        [TestMethod]
        public void MakePayment_ForValidPayment_Calls_UpdateAccount()
        {
            // Arrange
            var account = new Account { AccountNumber = "12", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 100 };
            var request = new MakePaymentRequest { DebtorAccountNumber = "12", Amount = 10, PaymentScheme = PaymentScheme.Bacs };
            
            _mockAccountService.Setup(x => x.GetAccount(account.AccountNumber)).Returns(account);
            _mockValidatorFactory.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.Bacs)).Returns(new BacsValidator());

            // Act
            _paymentService.MakePayment(request);

            // Assert
            _mockAccountService.Verify(x => x.UpdateAccount(account, request.Amount), Times.Once);
        }

        [TestMethod]
        public void MakePayment_ForInvalidPayment_DoesNotCall_UpdateAccount()
        {
            // Arrange
            var account = new Account { AccountNumber = "12", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 100 };
            var request = new MakePaymentRequest { DebtorAccountNumber = "12", Amount = 10, PaymentScheme = PaymentScheme.Bacs };
            
            _mockAccountService.Setup(x => x.GetAccount(account.AccountNumber)).Returns(account);
            _mockValidatorFactory.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.Bacs)).Returns(new BacsValidator());

            // Act
            _paymentService.MakePayment(request);

            // Assert
            _mockAccountService.Verify(x => x.UpdateAccount(account, request.Amount), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void MakePayment_WhenAccountNotFound_Throws_AccountNotFoundException()
        {
            // Arrange
            var accountNumber = "12";
            var request = new MakePaymentRequest { Amount = 12, PaymentScheme = PaymentScheme.FasterPayments };
            
            _mockAccountService.Setup(x => x.GetAccount(accountNumber)).Returns((Account)null);

            // Act            
            _paymentService.MakePayment(request);
        }

        [TestMethod]
        public void MakePayment_WhenPaymentSchemeValidatorReturnsFalse_ShouldNotUpdateAccount()
        {
            // Arrange
            var account = new Account { AccountNumber = "12", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100 };
            var request = new MakePaymentRequest {DebtorAccountNumber = "12", Amount = 200, PaymentScheme = PaymentScheme.FasterPayments };
            
            _mockAccountService.Setup(x => x.GetAccount(account.AccountNumber)).Returns(account);
            _mockValidatorFactory.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.FasterPayments)).Returns(new FasterPaymentsValidator());

            // Act
            _paymentService.MakePayment(request);

            // Assert
            _mockAccountService.Verify(x => x.UpdateAccount(account, request.Amount), Times.Never);
        }
    }
}
