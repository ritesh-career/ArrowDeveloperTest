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
    public class BacsValidatorTests
    {        
        [TestMethod]
        public void IsPaymentValid_AccountIsNotNull_SchemeBacs_ReturnsTrue()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };
            var request = new MakePaymentRequest();

            // Act
            var result = new BacsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPaymentValid_AccountIsNotNull_SchemeNotBacs_ReturnsFalse()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };
            var request = new MakePaymentRequest();

            // Act
            var result = new BacsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPaymentValid_AccountIsNull_ReturnsFalse()
        {
            // Arrange
            Account account = null;
            var request = new MakePaymentRequest();

            // Act
            var result = new BacsValidator().IsPaymentValid(account, request);

            // Assert
            Assert.IsFalse(result);
        }        
    }
}
