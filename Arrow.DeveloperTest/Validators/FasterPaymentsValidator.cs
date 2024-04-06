using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Validators
{
    public class FasterPaymentsValidator : IPaymentSchemeValidator
    {
        public bool IsPaymentValid(Account account, MakePaymentRequest request)
        {
            if (account == null || 
                !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) || 
                account.Balance < request.Amount)
            {
                return false;
            }

            return true;
        }
    }
}
