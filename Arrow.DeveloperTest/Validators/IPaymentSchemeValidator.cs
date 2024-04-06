using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Validators
{
    public interface IPaymentSchemeValidator
    {
        bool IsPaymentValid(Account account, MakePaymentRequest request);
    }
}
