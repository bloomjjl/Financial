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
    public class TransactionCategoryRepositoryTests
    {
        private TransactionCategory _dbTransactionCategory;
        private DbSet<TransactionCategory> _mockTransactionTypeDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private TransactionCategoryRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // setup fake model
            _dbTransactionCategory = new TransactionCategory { Id = 1, Name = "a", IsActive = true };

            // setup DbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new TransactionCategoryRepository(_fakeDbContext);
        }

        [TearDown]
        public void TearDown()
        {

        }





        [Test]
        public void GetAllActiveOrderedByName_WhenCalled_ReturnTransactionCategoryIEnumerable_Test()
        {
            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result, Is.InstanceOf<IEnumerable<TransactionCategory>>());
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenCalled_ReturnTransactionCategoryValues_Test()
        {
            var result = _repository.GetAllActiveOrderedByName().ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Id, Is.EqualTo(_dbTransactionCategory.Id), "Id");
                Assert.That(result[0].Name, Is.EqualTo(_dbTransactionCategory.Name), "Name");
                Assert.That(result[0].IsActive, Is.EqualTo(_dbTransactionCategory.IsActive), "IsActive");
            });
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenMultipleTransactionCategoriesFound_ReturnListSortedAscendingByName_Test()
        {
            var fakeTransactionCategories = new List<TransactionCategory>
            {
                new TransactionCategory { Id = 1, Name = "z", IsActive = true },
                new TransactionCategory { Id = 2, Name = "a", IsActive = true }
            };

            Setup_Repository_FakeDbContext(fakeTransactionCategories);

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
            _dbTransactionCategory.IsActive = false;

            Setup_Repository_FakeDbContext();

            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result.Count(), Is.EqualTo(0));
        }





        // private methods


        private void Setup_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext(new List<TransactionCategory> {_dbTransactionCategory});
        }

        private void Setup_FakeDbContext(List<TransactionCategory> fakeTransactionCategoryList)
        {
            // setup dbContext
            _fakeDbContext = MockFinancialDbContext.Create(transactionCategories: fakeTransactionCategoryList);
        }

        private void Setup_Repository_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new TransactionCategoryRepository(_fakeDbContext);
        }

        private void Setup_Repository_FakeDbContext(List<TransactionCategory> fakeTransactionCategoryList)
        {
            // setup dbContext
            Setup_FakeDbContext(fakeTransactionCategoryList);

            // set up repository
            _repository = new TransactionCategoryRepository(_fakeDbContext);
        }

    }
}
