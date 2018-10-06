using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Financial.Data;
using Financial.WebApplication.Controllers;
using Moq;
using Financial.Business;
using Financial.Business.Models;
using Financial.WebApplication.Models.ViewModels.AccountTransaction;
using System.Web.Mvc;
using Financial.Core.Models;
using Financial.Tests.Mocks;
using Financial.Core;

namespace Financial.Tests.WebApplication.Controllers
{
    [TestFixture]
    public class AccountTransactionControllerTests
    {
        private Asset _dbAsset;
        private AssetSetting _dbAssetSetting;
        private AssetTransaction _dbAssetTransaction;
        private AssetType _dbAssetType;
        private SettingType _dbSettingType;
        private TransactionCategory _dbTransactionCategory;
        private TransactionDescription _dbTransactionDescription;
        private TransactionType _dbTransactionType;
        private FinancialDbContext _fakeDbContext;
        private IUnitOfWork _fakeUnitOfWork;

        private Account _bmAccount;
        private AccountTransaction _bmAccountTransaction;
        private Mock<IBusinessService> _mockBusinessService;
        private IBusinessService _fakeBusinessService;
        private AccountTransactionController _controller;



        [SetUp]
        public void SetUp()
        {
            // setup fake model
            _bmAccount = new Account
            {
                AssetId = 1,
                AssetName = "a",
                AssetTypeId = 2,
                AssetTypeName = "b",
            };
            _bmAccountTransaction = new AccountTransaction
            {
                AssetTransactionId = 3,
                AssetId = _bmAccount.AssetId,
                AssetName = _bmAccount.AssetName,
                AssetTypeId = _bmAccount.AssetTypeId,
                AssetTypeName = _bmAccount.AssetTypeName,
                DueDate = new DateTime(2018, 1, 2),
                ClearDate = new DateTime(2018, 3, 4),
                Amount = 123.45M,
                Note = "abc",
            };

            // setup DbContext
            Setup_FakeDbContext();

            // setup uow
            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);

            // setup Service
            _fakeBusinessService = new BusinessService(_fakeUnitOfWork);

            // setup controller
            _controller = new AccountTransactionController(_fakeBusinessService);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void Index_WhenCalled_ReturnIndexView_Test()
        {
            var result = _controller.Index();

            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Index_WhenCalled_ReturnsIndexViewModelList_Test()
        {
            var result = _controller.Index();

            Assert.That(result.ViewData.Model, Is.TypeOf<List<IndexViewModel>>());
        }

        [Test]
        public void Index_WhenCalled_ShouldCallOneTimeAccountTransactionServiceMethodGetListOfActiveTransactions_Test()
        {
            _mockBusinessService = new Mock<IBusinessService>();
            _mockBusinessService.Setup(bs => bs.AccountTransactionService.GetListOfActiveTransactions())
                .Returns(It.IsAny<List<AccountTransaction>>);

            _controller = new AccountTransactionController(_mockBusinessService.Object);

            _controller.Index();

            _mockBusinessService.Verify(bs => bs.AccountTransactionService.GetListOfActiveTransactions(),
                Times.Once);
        }

        [Test]
        public void Index_WhenAccountTransactionsFound_ReturnAccountTransactionsOrderedDescendingByDueDate_Test()
        {
            _mockBusinessService = new Mock<IBusinessService>();

            var olderDueDate = new DateTime(2018, 1, 2);
            var newerDueDate = new DateTime(2018, 3, 4);
            SetUp_Service_AccountTransactions_OrderedAscendingByDueDate(olderDueDate, newerDueDate);

            var result = _controller.Index();

            var vmActual = (List<IndexViewModel>)result.Model;

            Assert.Multiple(() =>
            {
                Assert.That(vmActual.Count, Is.EqualTo(2), "Count");
                Assert.That(vmActual[0].DueDate, Is.EqualTo(newerDueDate), "First Index");
                Assert.That(vmActual[1].DueDate, Is.EqualTo(olderDueDate), "Second Index");
            });
        }

        [Test]
        public void Index_WhenTempDataSuccessMessageIsNotNull_ReturnViewDataSuccessMessage_Test()
        {
            var expectedMessage = "test message";
            _controller.TempData["SuccessMessage"] = expectedMessage;

            var result = _controller.Index();

            var vResult = (ViewResult)result;

            Assert.That(vResult.ViewData["SuccessMessage"].ToString(), Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Index_WhenTempDataErrorMessageIsNotNull_ReturnViewDataErrorMessage_Test()
        {
            var expectedMessage = "test message";
            _controller.TempData["ErrorMessage"] = expectedMessage;

            var result = _controller.Index();

            var vResult = (ViewResult)result;

            Assert.That(vResult.ViewData["ErrorMessage"].ToString(), Is.EqualTo(expectedMessage));
        }



        // Create

        [Test]
        public void Create_WhenCalled_ReturnCreateView_Test()
        {
            var result = (ViewResult)_controller.Create(assetId: null);

            Assert.That(result.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void Create_WhenCalled_ReturnCreateViewModel_Test()
        {
            var result = (ViewResult)_controller.Create(assetId: null);

            Assert.That(result.Model, Is.InstanceOf<CreateViewModel>());
        }

        [Test]
        public void Create_WhenCalled_ShouldCallOneTimeGetAccountForTransaction_Test()
        {
            _mockBusinessService = new Mock<IBusinessService>();
            _mockBusinessService.Setup(bs => bs.AccountTransactionService.GetAccountForTransaction(null))
                .Returns(It.IsAny<Account>());

            _controller = new AccountTransactionController(_mockBusinessService.Object);

            _controller.Create(assetId: null);

            _mockBusinessService.Verify(bs => bs.AccountTransactionService.GetAccountForTransaction(It.IsAny<int?>()),
                Times.Once);
        }

        [Test]
        public void Create_WhenCalled_ShouldCallOneTimeGetAccountSelectList_Test()
        {
            _mockBusinessService = new Mock<IBusinessService>();
            _mockBusinessService.Setup(bs => bs.AccountTransactionService.GetAccountSelectList(null))
                .Returns(It.IsAny<List<SelectListItem>>());

            _controller = new AccountTransactionController(_mockBusinessService.Object);

            _controller.Create(assetId: null);

            _mockBusinessService.Verify(bs => bs.AccountTransactionService.GetAccountSelectList(It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void Create_WhenCalled_ReturnAccountSelectList_Test()
        {
            Setup_FakeService();

            _controller = new AccountTransactionController(_fakeBusinessService);

            var result = (ViewResult)_controller.Create(assetId: null);

            var vmResult = (CreateViewModel)result.ViewData.Model;

            Assert.That(vmResult.Accounts.Count(), Is.Not.EqualTo(null));
        }

        [Test]
        public void Create_WhenCalled_ShouldCallOneTimeGetTransactionTypeSelectList_Test()
        {
            _mockBusinessService = new Mock<IBusinessService>();
            _mockBusinessService.Setup(bs => bs.AccountTransactionService.GetTransactionTypeSelectList(null))
                .Returns(It.IsAny<List<SelectListItem>>());

            _controller = new AccountTransactionController(_mockBusinessService.Object);

            _controller.Create(assetId: null);

            _mockBusinessService.Verify(bs => bs.AccountTransactionService.GetTransactionTypeSelectList(It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void Create_WhenCalled_ReturnTransactionTypeSelectList_Test()
        {
            Setup_FakeService();

            _controller = new AccountTransactionController(_fakeBusinessService);

            var result = (ViewResult)_controller.Create(assetId: null);

            var vmResult = (CreateViewModel)result.ViewData.Model;

            Assert.That(vmResult.TransactionTypes.Count(), Is.Not.EqualTo(null));
        }

        [Test]
        public void Create_WhenCalled_ShouldCallOneTimeGetTransactionCategorySelectList_Test()
        {
            _mockBusinessService = new Mock<IBusinessService>();
            _mockBusinessService.Setup(bs => bs.AccountTransactionService.GetTransactionCategorySelectList(null))
                .Returns(It.IsAny<List<SelectListItem>>());

            _controller = new AccountTransactionController(_mockBusinessService.Object);

            _controller.Create(assetId: null);

            _mockBusinessService.Verify(bs => bs.AccountTransactionService.GetTransactionCategorySelectList(It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void Create_WhenCalled_ReturnTransactionCategorySelectList_Test()
        {
            Setup_FakeService();

            _controller = new AccountTransactionController(_fakeBusinessService);

            var result = (ViewResult)_controller.Create(assetId: null);

            var vmResult = (CreateViewModel)result.ViewData.Model;

            Assert.That(vmResult.TransactionCategories, Is.Not.EqualTo(null));
        }



        // private


        private void Setup_FakeDb()
        {
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
        }

        private void Setup_FakeDbContext()
        {
            // setup db
            Setup_FakeDb();

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

        private void Setup_FakeUnitOfWork()
        {
            Setup_FakeDbContext();

            _fakeUnitOfWork = new UnitOfWork(_fakeDbContext);
        }

        private void Setup_FakeService()
        {
            Setup_FakeUnitOfWork();

            _fakeBusinessService = new BusinessService(_fakeUnitOfWork);
        }

        private void SetUp_Service_AccountTransactions_OrderedAscendingByDueDate(DateTime olderDueDate, DateTime newerDueDate)
        {
            _mockBusinessService.Setup(bs => bs.AccountTransactionService.GetListOfActiveTransactions())
                .Returns(new List<AccountTransaction>
                {
                    new AccountTransaction
                    {
                        AssetTransactionId = 1, AssetId = 2, AssetName = "a", AssetTypeId = 3, AssetTypeName = "b",
                        DueDate = olderDueDate, ClearDate = new DateTime(2018, 5, 6), Amount = 123.45M, Note = "abc",
                    },
                    new AccountTransaction
                    {
                        AssetTransactionId = 1, AssetId = 2, AssetName = "a", AssetTypeId = 3, AssetTypeName = "b",
                        DueDate = newerDueDate, ClearDate = new DateTime(2018, 5, 6), Amount = 123.45M, Note = "abc",
                    },
                });

            _controller = new AccountTransactionController(_mockBusinessService.Object);
        }
    }
}
