using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrow.DeveloperTest.Validators
{
    public interface IPaymentSchemeValidatorFactory
    {
        IPaymentSchemeValidator GetPaymentSchemeValidator(PaymentScheme paymentScheme);
    }
}
