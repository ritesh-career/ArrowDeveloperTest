using Arrow.DeveloperTest.Types;
using Arrow.DeveloperTest.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrow.DeveloperTest.Tests
{
    [TestClass]
    public class FasterPaymentsValidatorTests
    {
        [TestMethod]
        public void IsPaymentValid_WhenAccountIsFasterPayments_And_Balance_GreaterThan_RequestAmount_ReturnsTrue()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 20 };
            var request = new MakePaymentRequest() { Amount = 10 };

            // Act
            var result = new FasterPaymentsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsFasterPayments_And_Balance_Equals_RequestAmount_ReturnsTrue()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 20 };
            var request = new MakePaymentRequest() { Amount = 20 };

            // Act
            var result = new FasterPaymentsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsFasterPayments_And_Balance_LesserThan_RequestAmount_ReturnsFalse()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 20 };
            var request = new MakePaymentRequest() { Amount = 21 };

            // Act
            var result = new FasterPaymentsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsFasterPayments_And_PaymentScheme_IsNot_FasterPayments_ReturnsFalse()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 20 };
            var request = new MakePaymentRequest() { Amount = 10 };

            // Act
            var result = new FasterPaymentsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsNull_ReturnsFalse()
        {
            // Arrange
            Account account = null;
            var request = new MakePaymentRequest() { Amount = 10 };

            // Act
            var result = new FasterPaymentsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }        
    }
}
