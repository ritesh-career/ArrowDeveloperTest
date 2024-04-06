using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Validators
{
    public interface IPaymentSchemeValidatorFactory
    {
        IPaymentSchemeValidator GetPaymentSchemeValidator(PaymentScheme paymentScheme);
    }
}
