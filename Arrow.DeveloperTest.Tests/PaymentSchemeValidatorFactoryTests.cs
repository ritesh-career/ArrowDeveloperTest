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
    public class PaymentSchemeValidatorFactoryTests
    {
        private PaymentSchemeValidatorFactory _validatorFactory;

        [TestInitialize]
        public void Initialize()
        {
            _validatorFactory = new PaymentSchemeValidatorFactory();
        }

        [TestMethod]
        public void GetPaymentSchemeValidator_WhenCaseIs_FasterPayments_ThenValidatorIs_FasterPaymentsValidator()
        {
            // Arrange            
            // Act
            var validator = _validatorFactory.GetPaymentSchemeValidator(PaymentScheme.FasterPayments);

            // Assert
            Assert.IsInstanceOfType<FasterPaymentsValidator>(validator);
        }

        [TestMethod]
        public void GetPaymentSchemeValidator_WhenCaseIs_Bacs_ThenValidatorIs_BacsValidator()
        {
            // Arrange            
            // Act
            var validator = _validatorFactory.GetPaymentSchemeValidator(PaymentScheme.Bacs);

            // Assert
            Assert.IsInstanceOfType<BacsValidator>(validator);
        }

        [TestMethod]
        public void GetPaymentSchemeValidator_WhenCaseIs_Chaps_ThenValidatorIs_ChapsValidator()
        {
            // Arrange            
            // Act
            var validator = _validatorFactory.GetPaymentSchemeValidator(PaymentScheme.Chaps);

            // Assert
            Assert.IsInstanceOfType<ChapsValidator>(validator);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPaymentSchemeValidator_WhenInvalidPaymentScheme_Throws_InvalidOperationException()
        {
            // Arrange
            var paymentScheme = (PaymentScheme)99;

            // Act
            _validatorFactory.GetPaymentSchemeValidator(paymentScheme);
        }
    }
}
