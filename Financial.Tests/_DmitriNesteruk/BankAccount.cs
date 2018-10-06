using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Financial.Tests._DmitriNesteruk
{
    public class BankAccount
    {
        public int Balance { get; private set; }

        public BankAccount(int startingBalance)
        {
            Balance = startingBalance;
        }

        public void Deposit(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    "Deposit amount must be positive",
                    nameof(amount));

            Balance += amount;
        }

        public bool Withdraw(int amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }

            return false;
        }
    }


    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount ba;

        [SetUp]
        public void SetUp()
        {
            // https://github.com/nunit
            ba = new BankAccount(100);
        }

        [Test]
        public void WhenWarn_ReturnWarningMessages()
        {
            Warn.If(2 + 2 != 5);
            Warn.If(2 + 2, Is.Not.EqualTo(5));
            Warn.If(() => 2 + 2, Is.Not.EqualTo(5).After(2000));

            Warn.Unless(2 + 2 == 5);
            Warn.Unless(2 + 2, Is.EqualTo(5));
            Warn.Unless(() => 2 + 2, Is.EqualTo(5).After(2000));

            Assert.Warn("This is a warning");
        }

        [Test]
        public void WhenMultipleAsserts_ReturnAllFailedMessages()
        {
            Assert.Multiple(() =>
            {
                Assert.That(2 + 2, Is.EqualTo(4));
                Assert.That(3 + 3, Is.EqualTo(6));
            });
        }

        [Test]
        public void Deposit_WhenAmountIsPositiveNumber_BalanceIncreases()
        {
            ba.Deposit(100);

            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void Deposit_WhenAmountIsNotPositive_ThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => ba.Deposit(-1)
                );
            StringAssert.StartsWith("Deposit amount must be positive",
                ex.Message);
        }

        [Test]
        [TestCase(50, true, 50)]
        [TestCase(100, true, 0)]
        [TestCase(1000, false, 100)]
        public void Withdraw_WhenAmountToWithdrawShouldSucceed_UpdateBalance(
            int amountToWithdraw, bool shouldSucceed, int expectedBalance)
        {
            var result = ba.Withdraw(amountToWithdraw);
            
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(shouldSucceed));
                Assert.That(expectedBalance, Is.EqualTo(ba.Balance));
            });
        }
    }
}
