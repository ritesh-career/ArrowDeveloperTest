# Arrow Developer Test Instructions

In the 'PaymentService.cs' file you will find a method for making a payment. At a high level the steps for making a payment are:

 1. Lookup the account the payment is being made from.
 2. Check that the account is in a valid state to make the payment.
 3. Deduct the payment amount from the account’s balance and update the account in the database.

What we’d like you to do is refactor the code with the following things in mind:

 - Adherence to SOLID principals
 - Testability
 - Readability

We’d also like you to add some unit tests to the Arrow.DeveloperTest.Tests project to show how you would test the code that you’ve produced and run the PaymentService from the Arrow.DeveloperTest.Runner console application.

The only specific 'rules' are:

- The solution should build
- The tests should all pass
- You should **not** change the method signature of the MakePayment method.

You are free to use any frameworks/NuGet packages that you see fit. You should plan to spend around an hour completing the exercise.

## Solution to developer test

- Created validator classes for each payment scheme.
- Created a payment scheme factory to get the apppropriate payment scheme validator.
- Created an Account Service class to handle account related operations.
- Created an account repository to handle account related database operations. This uses Entity Framework with an in-memory database configured in the console application.
- Created a test project to unit test the service classes and validators. Used Moq and MSTest for testing.
- Created a basic console UI using Spectre for some colour.

## Assumptions

- Provision made for adding an account. I thought this was necessary to test the payment service.
- No db implementation was specified so I have setup a simple EF Core based in-memory store.
- No spec to see if account should have a minimum amount so I have chosen to assume account can be overdrawn with the exception of FasterPayments validation which checks for a minimum balance of 0 based on existing code.