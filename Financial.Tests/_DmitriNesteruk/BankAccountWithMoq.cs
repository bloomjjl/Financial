using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Moq;

namespace Financial.Tests._DmitriNesteruk
{
    public interface ILogForMoq
    {
        bool Write(string msg);
    }


    public class BankAccountWithMoq
    {
        public int Balance { get; set; }
        private ILogForMoq _log;

        public BankAccountWithMoq(ILogForMoq log)
        {
            this._log = log;
        }

        public void Deposit(int amount)
        {
            _log.Write($"User has withdrawn {amount}");
            Balance += amount;
        }
    }

    [TestFixture]
    public class BankAccountWithMoqTests
    {
        private BankAccountWithMoq ba;

        [Test]
        public void Deposit_MockTest()
        {
            var log = new Mock<ILogForMoq>();
            ba = new BankAccountWithMoq(log.Object) {Balance = 100};
            ba.Deposit(100);

            Assert.That(ba.Balance, Is.EqualTo(200));
        }
    }
}
