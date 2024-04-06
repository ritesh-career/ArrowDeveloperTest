using System.ComponentModel.DataAnnotations;

namespace Arrow.DeveloperTest.Types
{
    public class Account
    {
        [Key]
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }
    }
}
