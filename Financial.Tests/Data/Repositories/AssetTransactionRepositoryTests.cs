using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Financial.Core.Models;
using System.Data.Entity;
using Moq;
using Financial.Core;
using Financial.Data.Repositories;
using Financial.Tests.Mocks;

namespace Financial.Tests.Data.Repositories
{
    [TestFixture]
    public class AssetTransactionRepositoryTests
    {
        private Asset _dbAsset;
        private AssetSetting _dbAssetSetting;
        private AssetTransaction _dbAssetTransaction;
        private AssetType _dbAssetType;
        private SettingType _dbSettingType;
        private TransactionCategory _dbTransactionCategory;
        private TransactionDescription _dbTransactionDescription;
        private TransactionType _dbTransactionType;
        private DbSet<AssetTransaction> _mockAssetTransactionDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private AssetTransactionRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // setup fake model
            _dbAssetType = new AssetType { Id = 1, Name = "a", IsActive = true };
            _dbAsset = new Asset { Id = 2, AssetTypeId = _dbAssetType.Id, Name = "b", IsActive = true };
            _dbSettingType = new SettingType { Id = 3, Name = "c", IsActive = true };
            _dbAssetSetting = new AssetSetting { Id = 4, AssetId = _dbAsset.Id, SettingTypeId = _dbSettingType.Id, Value = "d", IsActive = true };
            _dbTransactionCategory = new TransactionCategory { Id = 5, Name = "e", IsActive = true };
            _dbTransactionDescription = new TransactionDescription { Id = 6, Name = "f", IsActive = true };
            _dbTransactionType = new TransactionType { Id = 7, Name = "g", IsActive = true };
            _dbAssetTransaction = new AssetTransaction
            {
                Id = 8,
                TransactionCategoryId = _dbTransactionCategory.Id,
                TransactionDescriptionId = _dbTransactionDescription.Id,
                TransactionTypeId = _dbTransactionType.Id,
                CheckNumber = "123",
                DueDate = new DateTime(1234, 5, 6),
                ClearDate = new DateTime(1234, 7, 8),
                Amount = 123.45M,
                Note = "abcdef",
                IsActive = true
            };

            // setup DbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new AssetTransactionRepository(_fakeDbContext);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void GetActive_WhenCalled_ReturnAssetTransaction_Test()
        {
            var result = _repository.GetAllActiveByDueDate();

            Assert.That(result, Is.InstanceOf<List<AssetTransaction>>());
        }



        // private methods


        private void Setup_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext(
                new List<Asset> { _dbAsset },
                new List<AssetSetting> { _dbAssetSetting },
                new List<AssetTransaction> { _dbAssetTransaction },
                new List<AssetType> { _dbAssetType },
                new List<SettingType> { _dbSettingType },
                new List<TransactionCategory> { _dbTransactionCategory },
                new List<TransactionDescription> { _dbTransactionDescription },
                new List<TransactionType> { _dbTransactionType });
        }

        private void Setup_FakeDbContext(
            List<Asset> fakeAssetList,
            List<AssetSetting> fakeAssetSettingList,
            List<AssetTransaction> fakeAssetTransactionList,
            List<AssetType> fakeAssetTypeList,
            List<SettingType> fakeSettingTypeList,
            List<TransactionCategory> fakeTransactionCategoryList,
            List<TransactionDescription> fakeTransactionDescriptionList,
            List<TransactionType> fakeTransactionTypeList)
        {
            // setup dbContext
            _fakeDbContext = MockFinancialDbContext.Create(
                assets: fakeAssetList,
                assetSettings: fakeAssetSettingList,
                assetTransactions: fakeAssetTransactionList,
                assetTypes: fakeAssetTypeList,
                settingTypes: fakeSettingTypeList,
                transactionCategories: fakeTransactionCategoryList,
                transactionDescriptions: fakeTransactionDescriptionList,
                transactionTypes: fakeTransactionTypeList);
        }

    }
}