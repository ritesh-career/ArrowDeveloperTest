using Arrow.DeveloperTest.Exceptions;
using Arrow.DeveloperTest.Types;
using Arrow.DeveloperTest.Validators;

namespace Arrow.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentSchemeValidatorFactory _paymentSchemeValidatorFactory;

        public PaymentService(IAccountService accountService, IPaymentSchemeValidatorFactory paymentSchemeValidatorFactory)
        {
            _accountService = accountService;
            _paymentSchemeValidatorFactory = paymentSchemeValidatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult() { Success = false };
            
            var account = _accountService.GetAccount(request.DebtorAccountNumber);

            if (account == null)
            {
                throw new AccountNotFoundException($"Account {request.DebtorAccountNumber} not found.");
            }
            
            var paymentSchemeValidator = _paymentSchemeValidatorFactory.GetPaymentSchemeValidator(request.PaymentScheme);
            
            if (!paymentSchemeValidator.IsPaymentValid(account, request))
            {                
                return result;
            }            

            _accountService.UpdateAccount(account, request.Amount);

            result.Success = true;
            return result; 
        }
    }
}
