using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Validators
{
    public class ChapsValidator : IPaymentSchemeValidator
    {
        public bool IsPaymentValid(Account account, MakePaymentRequest request)
        {
            if (account == null ||
                !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) ||
                account.Status != AccountStatus.Live
                )
            {
                return false;                           
            }

            return true;
        }
    }
}
