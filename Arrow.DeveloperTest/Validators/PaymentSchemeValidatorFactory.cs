using Arrow.DeveloperTest.Types;
using System;

namespace Arrow.DeveloperTest.Validators
{
    public class PaymentSchemeValidatorFactory : IPaymentSchemeValidatorFactory
    {
        public IPaymentSchemeValidator GetPaymentSchemeValidator(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentsValidator();                    
                case PaymentScheme.Bacs:
                    return new BacsValidator();                    
                case PaymentScheme.Chaps:
                    return new ChapsValidator();                    
                default:
                    throw new InvalidOperationException($"Validator not implemented for {paymentScheme.ToString()}");
            }
        }
    }
}
