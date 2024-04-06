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
    public class ChapsValidatorTests
    {        
        [TestMethod]
        public void IsPaymentValid_WhenAccountIsChaps_StatusLive_ReturnsTrue()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Status = AccountStatus.Live };
            var request = new MakePaymentRequest();

            // Act
            var result = new ChapsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsChaps_StatusNotLive_ReturnsFalse()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Status = AccountStatus.Disabled };
            var request = new MakePaymentRequest();

            // Act
            var result = new ChapsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsNotChaps_ReturnsFalse()
        {
            // Arrange
            var account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Status = AccountStatus.Live };
            var request = new MakePaymentRequest();

            // Act
            var result = new ChapsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPaymentValid_WhenAccountIsNull_ReturnsFalse()
        {
            // Arrange
            Account account = null;
            var request = new MakePaymentRequest();

            // Act
            var result = new ChapsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }        
    }
}
