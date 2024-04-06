using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Validators
{
    public class BacsValidator : IPaymentSchemeValidator
    {
        public bool IsPaymentValid(Account account, MakePaymentRequest request)
        {
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                return false;
            }

            return true;            
        }
    }
}
