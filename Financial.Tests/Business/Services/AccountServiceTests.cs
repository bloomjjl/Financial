using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using Financial;
using Financial.Business.Models;
using Financial.Business.Services;
using Financial.Business.ServiceInterfaces;
using Financial.Core.Models;
using Financial.Data;
using Financial.Data.RepositoryInterfaces;

namespace Financial.Tests.Business.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private AccountService _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IAccountSettingService> _accountSettingService;
        private Asset _dbAsset;
        private AssetType _dbAssetType;
        private SettingType _dbSettingType;
        private AssetTypeSettingType _dbAssetTypeSettingType;

        [SetUp]
        public void SetUp()
        {
            _dbAsset = new Asset() { Id = 1, AssetTypeId = 2, Name = "a", IsActive = true };
            _dbAssetType = new AssetType() { Id = 2, Name = "b", IsActive = true };
            _dbSettingType = new Core.Models.SettingType() { Id = 3, Name = "c", IsActive = true };
            _dbAssetTypeSettingType = new AssetTypeSettingType() { AssetTypeId = 2, SettingTypeId = 3, IsActive = true };

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uow => uow.Assets.GetAllActiveOrderedByName())
                .Returns(new List<Asset> { _dbAsset });
            _unitOfWork.Setup(uow => uow.AssetTypes.Get(_dbAsset.AssetTypeId))
                .Returns(_dbAssetType);

            _accountSettingService = new Mock<IAccountSettingService>();

            _service = new AccountService(
                _unitOfWork.Object, 
                _accountSettingService.Object);
        }

        [TearDown]
        public void TearDown()
        {

        }




        [Test]
        public void GetListOfAccounts_WhenCalled_ReturnAccountList_Test()
        {
            var result = _service.GetListOfAccounts();

            Assert.That(result, Is.TypeOf<List<Account>>());
        }

        [Test]
        public void GetListOfAccounts_WhenCalled_ShouldCallOneTimeUnitOfWorkRepositoryAssetsMethodGetAllActiveOrderedByName_Test()
        {
            _service.GetListOfAccounts();

            _unitOfWork.Verify(uow => uow.Assets.GetAllActiveOrderedByName(),
                Times.Once);
        }

        [Test]
        public void GetListOfAccounts_WhenAccountListHasAccounts_ShouldCallUnitOfWorkRepositoryAssetTypesMethodGet_Test()
        {
            _service.GetListOfAccounts();

            _unitOfWork.Verify(uow => uow.AssetTypes.Get(_dbAsset.AssetTypeId),
                Times.AtLeastOnce);
        }

        [Test]
        public void GetListOfAccounts_WhenAccountListHasAccount_ReturnAccountValues_Test()
        {
            var result = _service.GetListOfAccounts();

            Assert.Multiple(() =>
            {
                Assert.That(result.Count, Is.EqualTo(1), "Count");
                Assert.That(result[0].AssetId, Is.EqualTo(_dbAsset.Id), "Asset Id");
                Assert.That(result[0].AssetName, Is.EqualTo(_dbAsset.Name), "Asset Name");
                Assert.That(result[0].AssetTypeId, Is.EqualTo(_dbAsset.AssetTypeId), "AssetType Id");
                Assert.That(result[0].AssetTypeName, Is.EqualTo(_dbAssetType.Name), "AssetType Name");
            });
        }

        [Test]
        public void GetListOfAccounts_WhenAccountTypeEqualsCreditCard_ReturnNameWithAccountNumber_Test()
        {
            SetUpForOneAccountWithAccountSettingEqualsCreditCard(accountName: "a", accountSettingValue: "1234");
            var expectedAssetName = "a (1234)";

            var result = (List<Account>)_service.GetListOfAccounts();

            Assert.That(result[0].AssetName, Is.EqualTo(expectedAssetName));
        }

        [Test]
        public void GetListOfAccounts_WhenAccountTypeIdEqualsZero_ReturnEmptyAccountList_Test()
        {
            _dbAsset.AssetTypeId = 0;

            var result = _service.GetListOfAccounts();

            Assert.That(result, Is.EquivalentTo(new List<Account>()));
        }

        [Test]
        public void GetListOfAccounts_WhenAccountListIsEmpty_ReturnEmptyAccountList_Test()
        {
            _unitOfWork.Setup(uow => uow.Assets.GetAllActiveOrderedByName())
                .Returns(new List<Financial.Core.Models.Asset>());

            _service = new AccountService(
                _unitOfWork.Object,
                _accountSettingService.Object);

            var result = _service.GetListOfAccounts();

            Assert.That(result.Count, Is.EqualTo(0));
        }






        [Test]
        public void GetSelectListOfAccounts_WhenCalled_ReturnSelectListItemList_Test()
        {
            var result = _service.GetSelectListOfAccounts(selectedId: null);

            Assert.That(result, Is.TypeOf<List<SelectListItem>>());
        }

        [Test]
        public void GetSelectListOfAccounts_WhenAssetFound_ShouldCallOneTimeAssetSettingServiceMethodGetAccountIdentificationInformation_Test()
        {
            _service.GetSelectListOfAccounts(_dbAsset.Id);

            _accountSettingService.Verify(
                asSvc => asSvc.GetAccountIdentificationInformation(new Account(_dbAsset)),
                Times.Once);
        }

        [Test]
        public void GetSelectListOfAccounts_WhenSelectedAssetIdProvided_ReturnListWithAccountSelected_Test()
        {
            var result = _service.GetSelectListOfAccounts(_dbAsset.Id);

            Assert.IsTrue(result.Any(r => r.Selected));
        }



        // private


        private void SetUpForOneAccountWithAccountSettingEqualsCreditCard(string accountName, string accountSettingValue)
        {
            var assetId = 1;
            var assetSettingId = 2;
            var assetTypeId = AssetType.IdForCreditCard;
            var settingTypeId = Core.Models.SettingType.IdForAccountNumber;

            _unitOfWork.Setup(uow => uow.Assets.GetAllActiveOrderedByName())
                .Returns(new List<Asset> { new Asset { Id = assetId, Name = accountName, AssetTypeId = assetTypeId, IsActive = true } });
            _unitOfWork.Setup(uow => uow.AssetTypes.Get(assetTypeId))
                .Returns(new AssetType { Id = assetTypeId, Name = "b", IsActive = true });
            _unitOfWork.Setup(uow => uow.AssetSettings.GetActive(assetId, settingTypeId))
                .Returns(new AssetSetting { Id = assetSettingId, AssetId = assetId, SettingTypeId = assetTypeId, Value = accountSettingValue, IsActive = true });

            _service = new AccountService(
                _unitOfWork.Object,
                _accountSettingService.Object);
        }

    }
}
