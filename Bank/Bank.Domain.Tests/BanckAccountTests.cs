using Bank.Domain;
using System;
using Xunit;

namespace Bank.Domain.Tests;

public class BankAccountTests
{
    [Theory]
    [InlineData(11.99, 4.55, 7.44)]
    [InlineData(12.3, 5.2, 7.1)]
    public void MultiDebit_WithValidAmount_UpdatesBalance(
        double beginningBalance, double debitAmount, double expected)
    {
        // Arrange
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
        // Act
        account.Debit(debitAmount);
        // Assert
        double actual = account.Balance;
        Assert.Equal(Math.Round(expected, 2), Math.Round(actual, 2));
    }

    [Fact]
    public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        double beginningBalance = 11.99;
        double debitAmount = -100.00;
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
        // Act and assert
        Assert.Throws<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
    }

    [Fact]
    public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        double beginningBalance = 11.99;
        double debitAmount = 20.0;
        BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
        // Act
        try
        {
            account.Debit(debitAmount);
        }
        catch (System.ArgumentOutOfRangeException e)
        {
            // Assert
            Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, e.Message);
        }
    }

    [Fact]
    public void Credit_WithValidAmount_UpdatesBalance()
    {
        BankAccount account = new BankAccount("Mr. Bryan Walton", 50.0);
        account.Credit(25.0);
        Assert.Equal(75.0, account.Balance);
    }

    [Fact]
    public void Debit_AmountExceedsBalance_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        double initialBalance = 100.0;
        var account = new BankAccount("Mr. Bryan Walton", initialBalance);

        // Act
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(150.0));

        // Assert
        Assert.Equal("amount", ex.ParamName); 
        Assert.Contains("exceeds balance", ex.Message, StringComparison.OrdinalIgnoreCase); 
    }

    [Fact]
    public void Debit_AmountEqualsBalance_AllowsDebit()
    {
        // Arrange
        double initialBalance = 100.0;
        var account = new BankAccount("Mr. Bryan Walton", initialBalance);

        // Act
        account.Debit(100.0);

        // Assert
        Assert.Equal(0.0, account.Balance, 2); 
    }

    [Fact]
    public void Debit_AmountIsNegative_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        double initialBalance = 100.0;
        var account = new BankAccount("Mr. Bryan Walton", initialBalance);

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(-10));

        Assert.Equal("amount", ex.ParamName);
        Assert.Contains("Debit amount is less than zero", ex.Message);
    }

    [Fact]
    public void Debit_AmountIsZero_AllowsDebit()
    {
        // Arrange
        double initialBalance = 100.0;
        var account = new BankAccount("Mr. Bryan Walton", initialBalance);

        // Act
        account.Debit(0);

        // Assert
        Assert.Equal(initialBalance, account.Balance);
    }

    [Fact]
    public void Credit_NegativeAmount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var account = new BankAccount("Mr. Bryan Walton", 100.0);

        // Act & Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(-50));

        Assert.Equal("amount", ex.ParamName); 
        Assert.Contains("Credit amount must be positive", ex.Message); 
    }

    [Fact]
    public void Credit_ZeroAmount_AddsNothing()
    {
        // Arrange
        var account = new BankAccount("Mr. Bryan Walton", 100.0);

        // Act
        account.Credit(0);

        // Assert
        Assert.Equal(100.0, account.Balance); 
    }


}