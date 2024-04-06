using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
