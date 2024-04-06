using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
