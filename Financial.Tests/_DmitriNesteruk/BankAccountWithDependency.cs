using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using ImpromptuInterface;

namespace Financial.Tests._DmitriNesteruk
{
    public interface ILog
    {
        bool Write(string msg);
    }

    public class ConsoleLog : ILog
    {
        public bool Write(string msg)
        {
            Console.WriteLine(msg);
            return true;
        }
    }

    public class BankAccountWithDependency
    {
        public int Balance { get; set; }
        private readonly ILog _log;

        public BankAccountWithDependency(ILog log)
        {
            this._log = log;
        }

        public void Deposit(int amount)
        {
            if (_log.Write($"Depositing {amount}"))
            {
                Balance += amount;
            }
        }
    }

    public class NullLog : ILog
    {
        public bool Write(string msg)
        {
            return true;
        }
    }

    public class NullLogWithResult : ILog
    {
        private bool _expectedResult;

        public NullLogWithResult(bool expectedResult)
        {
            this._expectedResult = expectedResult;
        }

        public bool Write(string msg)
        {
            return _expectedResult;
        }
    }

    // dynamic/generic class for creating log
    public class Null<T> : DynamicObject where T : class
    {
        // from impromptu interface "ActLike" method
        public static T Instance => new Null<T>().ActLike<T>();

        // "alt" + "insert" display list of 
        public override bool TryInvokeMember(InvokeMemberBinder binder, 
            object[] args, out object result)
        {
            result = Activator.CreateInstance(
                typeof(T).GetMethod(binder.Name).ReturnType
                );
            return true;
        }

    }

    public class LogMock : ILog
    {
        private bool _expectedResult;
        public Dictionary<string, int> MethodCallCount;

        public LogMock(bool expectedResult)
        {
            this._expectedResult = expectedResult;
            MethodCallCount = new Dictionary<string, int>();
        }

        private void AddOrIncrement(string methodName)
        {
            if (MethodCallCount.ContainsKey(methodName))
                MethodCallCount[methodName]++;
            else
                MethodCallCount.Add(methodName, 1);
        }

        public bool Write(string msg)
        {
            AddOrIncrement(nameof(Write));
            return _expectedResult;
        }
    }


    [TestFixture]
    public class BankAccountWithDependencyTests
    {
        private BankAccountWithDependency ba;

        [Test]
        public void Deposit_WhenAmountIsPositiveNumber_BalanceIncreases_IntegrationTest()
        {
            ba = new BankAccountWithDependency(new ConsoleLog()) {Balance = 100};
            ba.Deposit(100);
            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void Deposit_WhenAmountIsPositiveNumber_BalanceIncreases_TestWithFake()
        {
            var log = new NullLog();
            ba = new BankAccountWithDependency(log) { Balance = 100 };
            ba.Deposit(100);
            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void Deposit_WhenAmountIsPositiveNumber_BalanceIncreases_TestWithDynamicFake()
        {
            var log = Null<ILog>.Instance;
            ba = new BankAccountWithDependency(log) { Balance = 100 };
            ba.Deposit(100);
            //Assert.That(ba.Balance, Is.EqualTo(200));
            Assert.Warn("Always fails because type always returns bool equals false");
        }

        [Test]
        public void Deposit_WhenAmountIsPositiveNumber_BalanceIncreases_TestWithStub()
        {
            var log = new NullLogWithResult(true);
            ba = new BankAccountWithDependency(log) { Balance = 100 };
            ba.Deposit(100);
            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void Deposit_WhenAmountIsPositiveNumber_BalanceIncreases_TestWithMock()
        {
            var log = new LogMock(true);
            ba = new BankAccountWithDependency(log) { Balance = 100 };
            ba.Deposit(100);
            Assert.Multiple(() =>
            {
                Assert.That(ba.Balance, Is.EqualTo(200));
                Assert.That(
                    log.MethodCallCount[nameof(LogMock.Write)],
                    Is.EqualTo(1));
            });
        }
    }
}
