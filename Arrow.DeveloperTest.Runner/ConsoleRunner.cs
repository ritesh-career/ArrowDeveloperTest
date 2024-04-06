using Arrow.DeveloperTest.Exceptions;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arrow.DeveloperTest.Runner
{
    public class ConsoleRunner
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentService _paymentService;

        public ConsoleRunner(IAccountService accountService, IPaymentService paymentService)
        {
            _accountService = accountService;
            _paymentService = paymentService;
        }

        public void Run()
        {
            while (true)
            {
                AnsiConsole.MarkupLine("");
                var topLevelChoice = AnsiConsole.Ask<string>("Would you like to make a payment ([blue]p[/]), add an account ([green]a[/]), view all accounts ([purple]v[/]) or exit this application ([red]x[/])?");

                if (topLevelChoice == "p")
                {
                    var accountNumber = AnsiConsole.Ask<string>("Please enter the account number from which the payment will be taken:");
                    var paymentAmount = AnsiConsole.Ask<decimal>("Please enter the payment amount: ");

                    var selectedPaymentScheme = SelectPaymentScheme();

                    if (selectedPaymentScheme == null)
                    {
                        AnsiConsole.MarkupLine($"[red]Payment scheme selected {selectedPaymentScheme} is invalid.[/]");
                        continue;
                    }

                    try
                    {
                        var result = _paymentService.MakePayment(new MakePaymentRequest
                        {
                            Amount = paymentAmount,
                            DebtorAccountNumber = accountNumber,
                            PaymentScheme = selectedPaymentScheme.Value
                        });

                        if (result.Success)
                        {
                            AnsiConsole.MarkupLine($"[green]Payment successful.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[red]Unable to make payment.[/]");
                        }                                               
                        
                    }
                    catch (AccountNotFoundException aex)
                    {
                        AnsiConsole.MarkupLine($"[red]Unable to make payment. {aex.Message}[/]");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"[red]Unable to make payment. {ex.Message}[/]");
                        continue;
                    }                                     
                }                
                else if (topLevelChoice == "a")
                {
                    var accountNumber = AnsiConsole.Ask<string>("Please enter the account number: ");                                        
                    var balance = AnsiConsole.Ask<decimal>("Please enter the balance: ");
                    var selectedAccountPaymentScheme = this.SelectAccountPaymentScheme();

                    if (selectedAccountPaymentScheme == null)
                    {
                        AnsiConsole.MarkupLine($"[red]Payment scheme selected {selectedAccountPaymentScheme} is invalid.[/]");
                        continue;
                    }

                    var selectedAccountStatus = this.SelectAccountStatus();

                    if (selectedAccountStatus == null)
                    {
                        AnsiConsole.MarkupLine($"[red]Account status selected {selectedAccountStatus} is invalid.[/]");
                        continue;
                    }

                    var account = new Account
                    {
                        AccountNumber = accountNumber,
                        AllowedPaymentSchemes = selectedAccountPaymentScheme.Value,
                        Balance = balance,
                        Status = selectedAccountStatus.Value
                    };

                    try
                    {
                        _accountService.AddAccount(account);
                    }
                    catch (InvalidOperationException iEx)
                    {
                        AnsiConsole.MarkupLine($"[red]Unable to add account. {iEx.Message}[/]");
                        continue;
                    }
                    

                    AnsiConsole.MarkupLine($"Account added with Account Number: {account.AccountNumber}, Payment Scheme: {account.AllowedPaymentSchemes}, Balance: {account.Balance}");
                }
                else if (topLevelChoice == "v")
                {
                    var accounts  = _accountService.GetAllAccounts();

                    if (!accounts.Any())
                    { 
                        AnsiConsole.MarkupLine("[red]No accounts found.[/]");
                        continue;
                    }

                    foreach (var account in accounts)
                    {
                        AnsiConsole.MarkupLine($"Account Number: {account.AccountNumber}, Balance: {account.Balance}, Payment Scheme: {account.AllowedPaymentSchemes}, Status: {account.Status},");
                    }

                }
                else if (topLevelChoice == "x")
                {
                    AnsiConsole.MarkupLine("[blue]Exiting...[/]");
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Invalid choice[/]");
                }
            }              
        }

        private PaymentScheme? SelectPaymentScheme()
        {
            var selectedPaymentScheme = AnsiConsole.Ask<string>("Please enter the payment scheme: FasterPayments ([green]f[/]), Bacs ([green]b[/]) or Chaps ([green]c[/])");

            switch (selectedPaymentScheme)
            {
                case "f":
                    return PaymentScheme.FasterPayments;                    
                case "b":
                    return PaymentScheme.Bacs;                    
                case "c":
                    return PaymentScheme.Chaps;                    
                default:                    
                    return null;
            }
        }

        private AllowedPaymentSchemes? SelectAccountPaymentScheme()
        {
            var selectedPaymentScheme = AnsiConsole.Ask<string>("Please enter the payment scheme for this account: FasterPayments ([green]f[/]), Bacs ([green]b[/]) or Chaps ([green]c[/])");

            switch (selectedPaymentScheme)
            {
                case "f":
                    return AllowedPaymentSchemes.FasterPayments;
                case "b":
                    return AllowedPaymentSchemes.Bacs;
                case "c":
                    return AllowedPaymentSchemes.Chaps;
                default:
                    return null;
            }
        }

        private AccountStatus? SelectAccountStatus()
        {
            var selectedAccountStatus = AnsiConsole.Ask<string>("Please enter the account status for this account: Live ([green]l[/]), Disabled ([green]d[/]) or Inbound Payments Only ([green]i[/])");

            switch (selectedAccountStatus)
            {
                case "l":
                    return AccountStatus.Live;
                case "d":
                    return AccountStatus.Disabled;
                case "i":
                    return AccountStatus.InboundPaymentsOnly;
                default:
                    return null;
            }
        }
    }
}
