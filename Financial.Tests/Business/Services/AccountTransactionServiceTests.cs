using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;
using Financial.Business.Services;
using Financial.Data;
using Moq;
using Financial.Core.Models;
using Financial.Business.Models;
using Financial.Core;
using Financial.Data.Repositories;
using Financial.Tests.Mocks;

namespace Financial.Tests.Business.Services
{
    [TestFixture]
    public class AccountTransactionServiceTests
    {
        private Asset _dbAsset;
        private AssetType _dbAssetType;
        private AssetSetting _dbAssetSetting;
        private AssetTransaction _dbAssetTransaction;
        private SettingType _dbSettingType;
        private TransactionType _dbTransactionType;
        private TransactionCategory _dbTransactionCategory;
        private TransactionDescription _dbTransactionDescription;
        private AccountTransactionService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IUnitOfWork _fakeUnitOfWork;
        private FinancialDbContext _fakeDbContext;


        [SetUp]
        public void SetUp()
        {
            // setup db models
            _dbAssetType = new AssetType { Id = 1, Name = "a", IsActive = true };
            _dbAsset = new Asset
            {
                Id = 2,
                AssetTypeId = _dbAssetType.Id,
                AssetType = _dbAssetType, // setup include
                Name = "b",
                IsActive = true
            };
            _dbSettingType = new SettingType { Id = 3, Name = "c", IsActive = true };
            _dbAssetSetting = new AssetSetting
            {
                Id = 4,
                AssetId = _dbAsset.Id,
                Asset = _dbAsset, // setup include
                SettingTypeId = _dbSettingType.Id,
                SettingType = _dbSettingType, // setup include
                Value = "d",
                IsActive = true
            };
            _dbTransactionType = new TransactionType { Id = 5, Name = "e", IsActive = true };
            _dbTransactionCategory = new TransactionCategory {Id = 6, Name = "f", IsActive = true};
            _dbTransactionDescription = new TransactionDescription {Id = 7, Name = "g", IsActive = true};
            _dbAssetTransaction = new AssetTransaction
            {
                Id = 8,
                AssetId = _dbAsset.Id,
                Asset = _dbAsset,  // setup include
                TransactionTypeId = _dbTransactionType.Id,
                TransactionType = _dbTransactionType, // setup include
                TransactionCategoryId = _dbTransactionCategory.Id,
                TransactionCategory = _dbTransactionCategory, // setup include
                TransactionDescriptionId = _dbTransactionDescription.Id,
                TransactionDescription = _dbTransactionDescription, // setup include
                DueDate = new DateTime(1234, 5, 6),
                ClearDate = new DateTime(1234, 7, 8),
                Amount = 1.23M,
                Note = "abc",
                IsActive = true
            };

            // setup DbContext
            Setup_FakeDbContext();

            // setup uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // setup service
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void GetListOfActiveTransactions_WhenCalled_ReturnAccountTransactionList_Test()
        {
            var result = _service.GetListOfActiveTransactions();

            Assert.That(result, Is.TypeOf<List<AccountTransaction>>());
        }

        [Test]
        public void GetListOfActiveTransactions_WhenCalled_ShouldCallOneTimeUnitOfWorkRepositoryAssetTransactionsMethodGetAllActiveByDueDate_Test()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            Setup_MockUnitOfWork_AssetTransaction_GetAllActiveByDueDate();
            Setup_MockUnitOfWork_Assets_Get();
            Setup_MockUnitOfWork_AssetTypes_Get();
            Setup_MockUnitOfWork_AssetSettings_GetActive();

            _service = new AccountTransactionService(_mockUnitOfWork.Object);

            _service.GetListOfActiveTransactions();

            _mockUnitOfWork.Verify(uow => uow.AssetTransactions.GetAllActiveByDueDate(),
                Times.Once);
        }

        [Test]
        public void GetListOfActiveTransactions_WhenAccountTransactionListHasAccountTransactions_ShouldCallUnitOfWorkRepositoryAssetsMethodGet_Test()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            Setup_MockUnitOfWork_AssetTransaction_GetAllActiveByDueDate();
            Setup_MockUnitOfWork_Assets_Get();
            Setup_MockUnitOfWork_AssetTypes_Get();
            Setup_MockUnitOfWork_AssetSettings_GetActive();

            _service = new AccountTransactionService(_mockUnitOfWork.Object);

            _service.GetListOfActiveTransactions();

            _mockUnitOfWork.Verify(uow => uow.Assets.Get(It.IsAny<int>()),
                Times.AtLeastOnce);
        }

        [Test]
        public void GetListOfActiveTransactions_WhenAccountTransactionListHasAccountTransactions_ShouldCallUnitOfWorkRepositoryAssetSettingsMethodGetActive_Test()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            Setup_MockUnitOfWork_AssetTransaction_GetAllActiveByDueDate();
            Setup_MockUnitOfWork_Assets_Get();
            Setup_MockUnitOfWork_AssetTypes_Get();
            Setup_MockUnitOfWork_AssetSettings_GetActive();

            _service = new AccountTransactionService(_mockUnitOfWork.Object);

            _service.GetListOfActiveTransactions();

            _mockUnitOfWork.Verify(uow => uow.AssetSettings.GetActive(It.IsAny<int>(), It.IsAny<int>()),
                Times.AtLeastOnce);
        }

        [Test]
        public void GetListOfActiveTransactions_WhenAccountListHasAccount_ReturnAccountTransactionValues_Test()
        {
            var result = _service.GetListOfActiveTransactions();

            Assert.Multiple(() =>
            {
                Assert.That(result.Count, Is.EqualTo(1), "Count");
                Assert.That(result[0].AssetTransactionId, Is.EqualTo(_dbAssetTransaction.Id), "AssetTransaction Id");
                Assert.That(result[0].AssetId, Is.EqualTo(_dbAssetTransaction.AssetId), "Asset Id");
                Assert.That(result[0].AssetName, Is.EqualTo(_dbAsset.Name), "Asset Name");
                Assert.That(result[0].AssetTypeId, Is.EqualTo(_dbAsset.AssetTypeId), "AssetType Id");
                Assert.That(result[0].AssetTypeName, Is.EqualTo(_dbAssetType.Name), "AssetType Name");
                Assert.That(result[0].DueDate, Is.EqualTo(_dbAssetTransaction.DueDate), "DueDate");
                Assert.That(result[0].ClearDate, Is.EqualTo(_dbAssetTransaction.ClearDate), "ClearDate");
                Assert.That(result[0].Amount, Is.EqualTo(_dbAssetTransaction.Amount), "Amount");
                Assert.That(result[0].Note, Is.EqualTo(_dbAssetTransaction.Note), "Note");
            });
        }

        [Test]
        public void GetListOfActiveTransactions_WhenAccountTypeEqualsCreditCard_ReturnNameWithAccountNumber_Test()
        {
            Setup_Service_FakeUnitOfWork_AssetType_CreditCard(assetName: "a", assetSettingValue: "1234");

            var expectedAssetName = "a (1234)";

            var result = _service.GetListOfActiveTransactions();

            Assert.That(result[0].AssetName, Is.EqualTo(expectedAssetName));
        }

        [Test]
        public void GetListOfActiveTransactions_WhenTransactionTypeEqualsIncome_ReturnPositiveAmount_Test()
        {
            Setup_Service_FakeUnitOfWork_TransactionType_Income(transactionAmount: 1.25M);

            var result = _service.GetListOfActiveTransactions();

            Assert.That(result[0].Amount, Is.EqualTo(1.25));
        }

        [Test]
        public void GetListOfActiveTransactions_WhenTransactionTypeEqualsExpense_ReturnNegativeAmount_Test()
        {
            Setup_Service_FakeUnitOfWork_TransactionType_Expense(transactionAmount: 1.25M);

            var result = _service.GetListOfActiveTransactions();

            Assert.That(result[0].Amount, Is.EqualTo(-1.25));
        }

        [Test]
        public void GetListOfActiveTransactions_WhenAccountIdEqualsZero_ReturnEmptyAccountList_Test()
        {
            _dbAssetTransaction.AssetId = 0;

            var result = _service.GetListOfActiveTransactions();

            Assert.That(result, Is.EquivalentTo(new List<AccountTransaction>()));
        }



        [Test]
        public void GetAccountForTransaction_WhenCalled_ReturnAccount_Test()
        {
            var result = _service.GetAccountForTransaction(assetId: _dbAsset.Id);

            Assert.That(result, Is.InstanceOf<Account>());
        }

        [Test]
        public void GetAccountForTransaction_WhenAssetIdEqualsNull_ReturnNull_Test()
        {
            var result = _service.GetAccountForTransaction(assetId: null);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void GetAccountForTransaction_WhenAssetIdEqualsZero_ReturnNull_Test()
        {
            var result = _service.GetAccountForTransaction(assetId: 0);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void GetAccountForTransaction_WhenAssetIdProvided_ReturnAccountValues_Test()
        {
            var result = _service.GetAccountForTransaction(_dbAsset.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result.AssetId, Is.EqualTo(_dbAsset.Id), "Asset Id");
                Assert.That(result.AssetName, Is.EqualTo(_dbAsset.Name), "Asset Name");
                Assert.That(result.AssetTypeId, Is.EqualTo(_dbAsset.AssetTypeId), "AssetType Id");
                Assert.That(result.AssetTypeName, Is.EqualTo(_dbAssetType.Name), "AssetType Name");
            });
        }

        [Test]
        public void GetAccountForTransaction_WhenAssetTypeEqualsCreditCard_ReturnFormattedAccountName_Test()
        {
            Setup_Service_FakeUnitOfWork_AssetType_CreditCard(assetName: "a", assetSettingValue: "1234");

            var expectedFormattedAssetName = "a (1234)";

            var result = _service.GetAccountForTransaction(assetId: _dbAsset.Id);

            Assert.That(result.AssetName, Is.EqualTo(expectedFormattedAssetName));
        }

        [Test]
        public void GetAccountForTransaction_WhenUnitOfWorkReturnsAssetEqualsNull_ReturnNull_Test()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(uow => uow.Assets.Get(It.IsAny<int>()));

            _service = new AccountTransactionService(_mockUnitOfWork.Object);

            var result = _service.GetAccountForTransaction(assetId: 1);

            Assert.That(result, Is.EqualTo(null));
        }




        [Test]
        public void GetAccountSelectList_WhenCalled_ReturnSelectListItems_Test()
        {
            var result = _service.GetAccountSelectList(selectedId: null);

            Assert.That(result, Is.InstanceOf<List<SelectListItem>>());
        }

        [Test]
        public void GetAccountSelectList_WhenCalled_ReturnAssetValues_Test()
        {
            var result = _service.GetAccountSelectList(selectedId: null);

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Value, Is.EqualTo(_dbAsset.Id.ToString()));
                Assert.That(result[0].Text, Is.EqualTo(_dbAsset.Name));
            });
        }

        [Test]
        public void GetAccountSelectList_WhenAssetTypeEqualsCreditCard_ReturnFormattedAssetName_Test()
        {
            Setup_Service_FakeUnitOfWork_AssetType_CreditCard(assetName: "a", assetSettingValue: "1234");

            var expectedFormattedAssetName = "a (1234)";

            var result = _service.GetAccountSelectList(selectedId: null);

            Assert.That(result[0].Text, Is.EqualTo(expectedFormattedAssetName));
        }

        [Test]
        public void GetAccountSelectList_WhenSelectedIdProvided_ReturnSelectedIndex_Test()
        {
            var result = _service.GetAccountSelectList(selectedId: _dbAsset.Id.ToString());

            Assert.That(result[0].Selected, Is.EqualTo(true));
        }

        [Test]
        public void GetAccountSelectList_WhenMultipleAssetsFound_ReturnAssetsOrderedAscendingByName_Test()
        {
            var fakeAssetTypeList = new List<AssetType> {_dbAssetType};
            var asset1 = new Asset { Id = 1, Name = "z", AssetTypeId = _dbAssetType.Id, AssetType = _dbAssetType, IsActive = true };
            var asset2 = new Asset { Id = 2, Name = "a", AssetTypeId = _dbAssetType.Id, AssetType = _dbAssetType, IsActive = true };
            var fakeAssetList = new List<Asset> { asset1, asset2 };
            var fakeSettingTypeList = new List<SettingType> {_dbSettingType};
            var fakeAssetSettingList = new List<AssetSetting>
            {
                new AssetSetting { Id = 3, AssetId = asset1.Id, Asset = asset1, SettingTypeId = _dbSettingType.Id, SettingType = _dbSettingType, Value = "abc", IsActive = true },
                new AssetSetting { Id = 4, AssetId = asset2.Id, Asset = asset2, SettingTypeId = _dbSettingType.Id, SettingType = _dbSettingType, Value = "abc", IsActive = true },
            };

            Setup_Service_FakeUnitOfWork(fakeAssetList, fakeAssetSettingList, fakeAssetTypeList, fakeSettingTypeList);

            var result = _service.GetAccountSelectList(selectedId: null);

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Text, Is.EqualTo("a"), "First Index");
                Assert.That(result[1].Text, Is.EqualTo("z"), "Second Index");
            });
        }



        [Test]
        public void GetTransactionTypeSelectList_WhenCalled_ReturnSelectListItems_Test()
        {
            var result = _service.GetTransactionTypeSelectList(selectedId: null);

            Assert.That(result, Is.InstanceOf<List<SelectListItem>>());
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenCalled_ReturnTransactionTypeValues_Test()
        {
            var result = _service.GetTransactionTypeSelectList(selectedId: null);

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Value, Is.EqualTo(_dbTransactionType.Id.ToString()), "Id");
                Assert.That(result[0].Text, Is.EqualTo(_dbTransactionType.Name), "Name");
            });
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenSelectedIdProvided_ReturnSelectedIndex_Test()
        {
            var result = _service.GetTransactionTypeSelectList(selectedId: _dbTransactionType.Id.ToString());

            Assert.That(result[0].Selected, Is.EqualTo(true));
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenMultipleTransactionTypesFound_ReturnOrderedAscendingByName_Test()
        {
            var fakeTransactionTypeList = new List<TransactionType> 
            {
                new TransactionType { Id = 1, Name = "z", IsActive = true },
                new TransactionType { Id = 2, Name = "a", IsActive = true },
            };

            Setup_Service_FakeUnitOfWork(fakeTransactionTypeList);

            var result = _service.GetTransactionTypeSelectList(selectedId: null);

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Text, Is.EqualTo("a"), "First Index");
                Assert.That(result[1].Text, Is.EqualTo("z"), "Second Index");
            });
        }




        [Test]
        public void GetTransactionCategorySelectList_WhenCalled_ReturnSelectListItems_Test()
        {
            var result = _service.GetTransactionCategorySelectList(selectedId: null);

            Assert.That(result, Is.InstanceOf<List<SelectListItem>>());
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenCalled_ReturnTransactionCategoryValues_Test()
        {
            var result = _service.GetTransactionCategorySelectList(selectedId: null);

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Value, Is.EqualTo(_dbTransactionCategory.Id.ToString()), "Id");
                Assert.That(result[0].Text, Is.EqualTo(_dbTransactionCategory.Name), "Name");
            });
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenSelectedIdProvided_ReturnSelectedIndex_Test()
        {
            var result = _service.GetTransactionCategorySelectList(selectedId: _dbTransactionCategory.Id.ToString());

            Assert.That(result[0].Selected, Is.EqualTo(true));
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenMultipleTransactionCategoriesFound_ReturnOrderedAscendingByName_Test()
        {
            var fakeTransactionCategoryList = new List<TransactionCategory>
            {
                new TransactionCategory { Id = 1, Name = "z", IsActive = true },
                new TransactionCategory { Id = 2, Name = "a", IsActive = true },
            };

            Setup_Service_FakeUnitOfWork(fakeTransactionCategoryList);

            var result = _service.GetTransactionCategorySelectList(selectedId: null);

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Text, Is.EqualTo("a"), "First Index");
                Assert.That(result[1].Text, Is.EqualTo("z"), "Second Index");
            });
        }



        // private


        private void Setup_FakeDbContext()
        {
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
        private void Setup_Service_FakeUnitOfWork(
            List<Asset> fakeAssets, 
            List<AssetSetting> fakeAssetSettings,
            List<AssetType> fakeAssetTypes,
            List<SettingType> fakeSettingTypes)
        {
            // setup DbContext
            _fakeDbContext = MockFinancialDbContext.Create(
                assets: fakeAssets,
                assetSettings: fakeAssetSettings,
                assetTypes: fakeAssetTypes,
                settingTypes: fakeSettingTypes);

            // set up uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // set up repository
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        private void Setup_Service_FakeUnitOfWork(List<TransactionType> fakeTransactionTypes)
        {
            // setup DbContext
            _fakeDbContext = MockFinancialDbContext.Create(transactionTypes: fakeTransactionTypes);

            // set up uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // set up repository
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        private void Setup_Service_FakeUnitOfWork(List<TransactionCategory> fakeTransactionCategories)
        {
            // setup DbContext
            _fakeDbContext = MockFinancialDbContext.Create(transactionCategories: fakeTransactionCategories);

            // set up uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // set up repository
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        private void Setup_Service_FakeUnitOfWork_AssetType_CreditCard(string assetName, string assetSettingValue)
        {
            // setup fake model
            _dbAssetType.Id = AssetType.IdForCreditCard;

            _dbAsset.AssetTypeId = _dbAssetType.Id;
            _dbAsset.AssetType = _dbAssetType;
            _dbAsset.Name = assetName;

            _dbSettingType.Id = SettingType.IdForAccountNumber;

            _dbAssetSetting.SettingTypeId = _dbSettingType.Id;
            _dbAssetSetting.SettingType = _dbSettingType;
            _dbAssetSetting.Asset = _dbAsset;
            _dbAssetSetting.Value = assetSettingValue;

            _dbAssetTransaction.AssetId = _dbAsset.Id;
            _dbAssetTransaction.Asset = _dbAsset;

            // setup DbContext
            Setup_FakeDbContext();

            // set up uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // set up repository
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        private void Setup_Service_FakeUnitOfWork_TransactionType_Income(decimal transactionAmount)
        {
            // setup fake model
            _dbTransactionType.Id = TransactionType.IdForIncome;

            _dbAssetTransaction.TransactionTypeId = _dbTransactionType.Id;
            _dbAssetTransaction.TransactionType = _dbTransactionType;
            _dbAssetTransaction.Amount = transactionAmount;

            // setup DbContext
            Setup_FakeDbContext();

            // set up uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // set up repository
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        private void Setup_Service_FakeUnitOfWork_TransactionType_Expense(decimal transactionAmount)
        {
            // setup fake model
            _dbTransactionType.Id = TransactionType.IdForExpense;

            _dbAssetTransaction.TransactionTypeId = _dbTransactionType.Id;
            _dbAssetTransaction.TransactionType = _dbTransactionType;
            _dbAssetTransaction.Amount = transactionAmount;

            // setup DbContext
            Setup_FakeDbContext();

            // set up uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // set up repository
            _service = new AccountTransactionService(_fakeUnitOfWork);
        }

        private void Setup_MockUnitOfWork_Assets_Get()
        {
            _mockUnitOfWork.Setup(uow => uow.Assets.Get(_dbAsset.Id))
                .Returns(_dbAsset);
        }
        private void Setup_MockUnitOfWork_AssetTypes_Get()
        {
            _mockUnitOfWork.Setup(uow => uow.AssetTypes.Get(_dbAssetType.Id))
                .Returns(_dbAssetType);
        }
        private void Setup_MockUnitOfWork_AssetSettings_GetActive()
        {
            _mockUnitOfWork.Setup(uow => uow.AssetSettings.GetActive(_dbAssetSetting.AssetId, _dbAssetSetting.SettingTypeId))
                .Returns(_dbAssetSetting);
        }
        private void Setup_MockUnitOfWork_AssetTransaction_GetAllActiveByDueDate()
        {
            _mockUnitOfWork.Setup(uow => uow.AssetTransactions.GetAllActiveByDueDate())
                .Returns(new List<AssetTransaction> { _dbAssetTransaction });
        }
    }
}
