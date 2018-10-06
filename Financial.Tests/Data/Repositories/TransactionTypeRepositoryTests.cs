using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Financial.Core.Models;
using System.Data.Entity;
using Financial.Core;
using Moq;
using Financial.Data.Repositories;
using Financial.Tests.Mocks;

namespace Financial.Tests.Data.Repositories
{
    [TestFixture]
    public class TransactionTypeRepositoryTests
    {
        private TransactionType _dbTransactionType;
        private DbSet<TransactionType> _mockTransactionTypeDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private TransactionTypeRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // setup fake model
            _dbTransactionType = new TransactionType { Id = 1, Name = "a", IsActive = true };

            // setup DbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new TransactionTypeRepository(_fakeDbContext);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void GetAllActiveOrderedByName_WhenCalled_ReturnTransactionTypeIEnumerable_Test()
        {
            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result, Is.InstanceOf<IEnumerable<TransactionType>>());
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenCalled_ReturnTransactionTypeValues_Test()
        {
            var result = _repository.GetAllActiveOrderedByName().ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Id, Is.EqualTo(_dbTransactionType.Id), "Id");
                Assert.That(result[0].Name, Is.EqualTo(_dbTransactionType.Name), "Name");
                Assert.That(result[0].IsActive, Is.EqualTo(_dbTransactionType.IsActive), "IsActive");
            });
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenMultipleTransactionTypesFound_ReturnListSortedAscendingByName_Test()
        {
            var fakeTransactionTypes = new List<TransactionType>
            {
                new TransactionType { Id = 1, Name = "z", IsActive = true },
                new TransactionType { Id = 2, Name = "a", IsActive = true }
            };

            Setup_Repository_FakeDbContext(fakeTransactionTypes);

            var result = _repository.GetAllActiveOrderedByName().ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("a"), "First Index");
                Assert.That(result[1].Name, Is.EqualTo("z"), "Second Index");
            });
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenIsActiveEqualsFalse_DoNotReturnRecord_Test()
        {
            _dbTransactionType.IsActive = false;

            Setup_Repository_FakeDbContext();

            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result.Count(), Is.EqualTo(0));
        }



        // private methods

        private void Setup_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext(new List<TransactionType> { _dbTransactionType });
        }

        private void Setup_FakeDbContext(List<TransactionType> fakeTransactionTypeList)
        {
            // setup dbContext
            _fakeDbContext = MockFinancialDbContext.Create(
                transactionTypes: fakeTransactionTypeList);
        }

        private void Setup_Repository_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new TransactionTypeRepository(_fakeDbContext);
        }

        private void Setup_Repository_FakeDbContext(List<TransactionType> fakeTransactionTypes)
        {
            // setup dbContext
            Setup_FakeDbContext(fakeTransactionTypes);

            // set up repository
            _repository = new TransactionTypeRepository(_fakeDbContext);
        }
    }
}
