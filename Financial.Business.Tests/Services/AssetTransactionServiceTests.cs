using Financial.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Tests.Fakes.Repositories;
using System.Web.Mvc;
using NUnit.Framework;

namespace Financial.Business.Tests.Services
{
    /*
    public class AssetTransactionServiceTestsBase : ServiceTestsBase
    {
        public AssetTransactionServiceTestsBase()
        {
            _service = new AssetTransactionService(_unitOfWork);
        }

        protected AssetTransactionService _service;
    }
    */

    public static class AssetTransactionServiceObjectMother
    {
        public static int AssetTypeIdForCreditCard = 3;
        public static int SettingTypeIdForAccountNumber = 1;
    }


    [TestFixture]
    public class AssetTransactionServiceTests : ServiceTestsBase
    {
        private AccountTransactionService _service;

        // SetUp Setting
        // TearDown Setting

        [SetUp]
        public void SetUp()
        {
            ResetUnitOfWork();
            _service = new AccountTransactionService(_unitOfWork);
        }


        [Test]
        public void GetListOfActiveTransactions_WhenNoInputValues_ReturnList_Test()
        {
            // Arrange

            // Act
            var result = _service.GetListOfActiveTransactions();

            // Assert
            Assert.IsInstanceOf(typeof(List<Business.Models.AccountTransaction>), result, "Result Type");
            Assert.That(result, Is.TypeOf<Business.Models.AccountTransaction>());
            Assert.IsNotNull(result, "Asset Transaction List");
        }


        /*
        [Test]
        public void GetTransactionOptions_WhenValidAssetIdProvided_ReturnAssetId_Test()
        {
            // Arrange

            // Act
            var result = _service.GetTransactionOptions(1);

            // Assert
            Assert.That(result.AssetId, Is.EqualTo(1));
        }
        */
        /*
        [Test]
        [TestCase(99, 0)]
        [TestCase(null, 0)]
        public void GetTransactionOptions_WhenInvalidAssetIdProvided_ReturnAssetIdEqualsZero_Test(int? assetId, int expectedResult)
        {
            // Arrange
            //var expAssetId = 1;
            //var sut = _service;

            // Act
            var result = _service.GetTransactionOptions(assetId);

            // Assert
            Assert.That(result.AssetId, Is.EqualTo(0));
        }
        */
        /*
        [Test]
        public void GetTransactionOptions_WhenAssetIdExists_ReturnAssetTransaction_Test()
        {
            // Arrange
            var expAssetId = 1;
            //var sut = _service;

            // Act
            var result = _service.GetTransactionOptions(expAssetId);

            // Assert
            Assert.IsInstanceOf(typeof(Business.Models.AssetTransaction), result, "Result Type");
            Assert.IsNotNull(result, "Asset Transaction");
        }

        [Test]
        public void GetTransactionOptions_WhenAssetIdNotFound_ReturnNull_Test()
        {
            // Arrange
            int? expAssetId = 99;
            //var sut = _service;

            // Act
            var result = _service.GetTransactionOptions(expAssetId);

            // Assert
            Assert.IsNull(result, "Asset Transaction");
        }

        [Test]
        public void GetTransactionOptions_WhenAssetIdIsNull_ReturnNull_Test()
        {
            // Arrange
            int? expAssetId = null;
            //var sut = _service;

            // Act
            var result = _service.GetTransactionOptions(expAssetId);

            // Assert
            Assert.IsNull(result, "Asset Transaction");
        }
        */
        /*
        [Test]
        public void GetTransactionOptions_WhenAssetIdNotFound_ThrowException()
        {
            var result = _service.GetTransactionOptions(99);

            Assert.That(() => _service.GetTransactionOptions(99), Throws.ArgumentNullException);
        }
        */
        /*
        [Test]
        [TestCase("1", true)]
        [TestCase("0", false)]
        [TestCase("99", false)]
        [TestCase(null, false)]
        public void GetAssetSelectList_WhenCalled_ReturnSelectListWithSelectedValue_Test(string selectedId, bool expectedResult)
        {
            // Arrange

            // Act
            var result = _service.GetAssetSelectList(selectedId);

            // Assert
            Assert.That(result.Any(r => r.Selected), Is.EqualTo(expectedResult));
        }
        */
        /*
        [Test]
        public void GetAssetSelectList_WhenSuccess_ReturnSelectList_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = null;

            // Act
            var result = _service.GetAssetSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsNotNull(result, "Result");
            Assert.AreNotEqual(0, result.Count, "Result Count");
        }

        [Test]
        public void GetAssetSelectList_WhenSelectedValueIsNull_ReturnNoSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = null;

            // Act
            var result = _service.GetAssetSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [Test]
        public void GetAssetSelectList_WhenSelectedValueIsFound_ReturnSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = "1";

            // Act
            var result = _service.GetAssetSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual(expSelectedId, result.FirstOrDefault(r => r.Selected).Value, "Result Selected Value");
        }

        [Test]
        public void GetAssetSelectList_WhenSelectedValueIsNotFound_ReturnNoSelectedValue_Test()
        {
            // Arrange
            // sut = _service;
            string expSelectedId = "99";

            // Act
            var result = _service.GetAssetSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [Test]
        public void GetAssetSelectList_WhenSuccess_ReturnActiveAssets_Test()
        {
            // Arrange
            var _dataAssets = new List<Core.Models.Asset>()
            {
                new Core.Models.Asset() {Id = 10, Name = "Active", IsActive = true },
                new Core.Models.Asset() {Id = 11, Name = "Not Active", IsActive = false },
            };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            _service = new AssetTransactionService(_unitOfWork);
            string expSelectedId = null;

            // Act
            var result = _service.GetAssetSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual(1, result.Count, "Result Count");
        }

        [Test]
        public void GetAssetSelectList_WhenSuccess_ReturnOrderedByTransactionTypeName_Test()
        {
            // Arrange
            var _dataAssets = new List<Core.Models.Asset>()
            {
                new Core.Models.Asset() {Id = 10, Name = "Z", IsActive = true },
                new Core.Models.Asset() {Id = 11, Name = "A", IsActive = true },
                new Core.Models.Asset() {Id = 12, Name = "B", IsActive = true },
            };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            _service = new AssetTransactionService(_unitOfWork);
            string expSelectedId = "99";

            // Act
            var result = _service.GetAssetSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual("A", result[0].Text, "Result Name A");
            Assert.AreEqual("B", result[1].Text, "Result Name B");
            Assert.AreEqual("Z", result[2].Text, "Result Name Z");
        }
        */



        [Test]
        [Ignore("Because I am testing this feature")]
        public void AddTransaction_WhenAssetTransactionIsValid_ReturnTrue_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetId = 1,
                TransactionTypeId = 2,
                TransactionCategoryId = 3
            };
            //var sut = _service;

            // Act
            var result = _service.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsTrue(result, "Result");
        }

        [Test]
        public void AddTransaction_WhenAssetIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetId = 99,
                TransactionTypeId = 2,
                TransactionCategoryId = 3
            };
            //var sut = _service;

            // Act
            var result = _service.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [Test]
        public void AddTransaction_WhenTransactionTypeIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetId = 1,
                TransactionTypeId = 99,
                TransactionCategoryId = 3
            };
            //var sut = _service;

            // Act
            var result = _service.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [Test]
        public void AddTransaction_WhenTransactionCateogryIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetId = 1,
                TransactionTypeId = 2,
                TransactionCategoryId = 99
            };
            //var sut = _service;

            // Act
            var result = _service.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }



        [Test]
        public void GetTransactionToEdit_WhenAssetTransactionIdIsValid_ReturnAssetTransaction_Test()
        {
            // Arrange
            var expAssetTransactionId = 1;
            //var sut = _service;

            // Act
            var result = _service.GetTransactionToEdit(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOf(typeof(Business.Models.AccountTransaction), result, "Result Type");
            Assert.IsNotNull(result, "Asset Transaction List");
            //Assert.IsNotNull(result.TransactionTypeSelectList, "Transaction Type List");
            //Assert.IsNotNull(result.TransactionCategorySelectList, "Transaction Category List");
        }

        [Test]
        public void GetTransactionToEdit_WhenAssetTransactionIdNotFound_ReturnNull_Test()
        {
            // Arrange
            var expAssetTransactionId = 99;
            //var sut = _service;

            // Act
            var result = _service.GetTransactionToEdit(expAssetTransactionId);

            // Assert
            Assert.IsNull(result, "Result");
        }



        [Test]
        public void GetAssetIdentificationInformation_WhenAssetIsValid_ReturnAssetName_Test()
        {
            // Arrange
            var expAsset = new Core.Models.Asset()
            {
                Id = 1,
                AssetTypeId = 2,
                Name = "Asset Name",
                IsActive = true,
            };
           // var sut = _service;

            // Act
            var result = _service.GetAssetIdentificationInformation(expAsset);

            // Assert
            Assert.IsInstanceOf(typeof(string), result, "Result Type");
            Assert.IsNotNull(result, "Formatted Asset Name");
        }

        [Test]
        public void GetAssetIdentificationInformation_WhenAssetIsNull_ReturnEmptyString_Test()
        {
            // Arrange
            //var sut = _service;

            // Act
            var result = _service.GetAssetIdentificationInformation(null);

            // Assert
            Assert.IsInstanceOf(typeof(string), result, "Result Type");
            Assert.AreEqual(string.Empty, result, "Formatted Asset Name");
        }

        [Test]
        public void GetAssetIdentificationInformation_WhenAssetTypeIsCreditCard_ReturnUpdatedAssetName_Test()
        {
            // Arrange
            var expAsset = new Core.Models.Asset()
            {
                Id = 1,
                AssetTypeId = AssetTransactionServiceObjectMother.AssetTypeIdForCreditCard,
                Name = "Asset Name",
                IsActive = true,
            };
            var expSettingTypeId = AssetTransactionServiceObjectMother.SettingTypeIdForAccountNumber;
            var _dataSettingTypes = new List<Core.Models.SettingType>()
            {
                new Core.Models.SettingType()
                {
                    Id = expSettingTypeId,
                    IsActive = true,
                }
            };
            var _dataAssetSettings = new List<Core.Models.AssetSetting>()
            {
                new Core.Models.AssetSetting() {
                    Id = 10,
                    AssetId = expAsset.Id,
                    SettingTypeId = expSettingTypeId,
                    Value = "1234",
                    IsActive = true,
                }
            };
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var expAssetName = expAsset.Name + " (1234)";
            _service = new AccountTransactionService(_unitOfWork);

            // Act
            var result = _service.GetAssetIdentificationInformation(expAsset);

            // Assert
            Assert.IsInstanceOf(typeof(string), result, "Result Type");
            Assert.AreEqual(expAssetName, result, "Asset Name");
        }



        [Test]
        public void GetTransactionTypeSelectList_WhenSuccess_ReturnSelectList_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = null;

            // Act
            var result = _service.GetTransactionTypeSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsNotNull(result, "Result");
            Assert.AreNotEqual(0, result.Count, "Result Count");
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenSelectedValueIsNull_ReturnNoSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = null;

            // Act
            var result = _service.GetTransactionTypeSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenSelectedValueIsFound_ReturnSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = "1";

            // Act
            var result = _service.GetTransactionTypeSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual(expSelectedId, result.FirstOrDefault(r => r.Selected).Value, "Result Selected Value");
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenSelectedValueIsNotFound_ReturnNoSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = "99";

            // Act
            var result = _service.GetTransactionTypeSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenSuccess_ReturnActiveTransactionTypes_Test()
        {
            // Arrange
            var _dataTransactionTypes = new List<Core.Models.TransactionType>()
            {
                new Core.Models.TransactionType() {Id = 10, Name = "Active", IsActive = true },
                new Core.Models.TransactionType() {Id = 11, Name = "Not Active", IsActive = false },
            };
            _unitOfWork.TransactionTypes = new InMemoryTransactionTypeRepository(_dataTransactionTypes);
            _service = new AccountTransactionService(_unitOfWork);
            string expSelectedId = null;

            // Act
            var result = _service.GetTransactionTypeSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual(1, result.Count, "Result Count");
        }

        [Test]
        public void GetTransactionTypeSelectList_WhenSuccess_ReturnOrderedByTransactionTypeName_Test()
        {
            // Arrange
            var _dataTransactionTypes = new List<Core.Models.TransactionType>()
            {
                new Core.Models.TransactionType() {Id = 10, Name = "Z", IsActive = true },
                new Core.Models.TransactionType() {Id = 11, Name = "A", IsActive = true },
                new Core.Models.TransactionType() {Id = 12, Name = "B", IsActive = true },
            };
            _unitOfWork.TransactionTypes = new InMemoryTransactionTypeRepository(_dataTransactionTypes);
            _service = new AccountTransactionService(_unitOfWork);
            string expSelectedId = "99";

            // Act
            var result = _service.GetTransactionTypeSelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual("A", result[0].Text, "Result Name A");
            Assert.AreEqual("B", result[1].Text, "Result Name B");
            Assert.AreEqual("Z", result[2].Text, "Result Name Z");
        }



        [Test]
        public void GetTransactionCategorySelectList_WhenSuccess_ReturnSelectList_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = null;

            // Act
            var result = _service.GetTransactionCategorySelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsNotNull(result, "Result");
            Assert.AreNotEqual(0, result.Count, "Result Count");
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenSelectedValueIsNull_ReturnNoSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = null;

            // Act
            var result = _service.GetTransactionCategorySelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenSelectedValueIsFound_ReturnSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = "1";

            // Act
            var result = _service.GetTransactionCategorySelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual(expSelectedId, result.FirstOrDefault(r => r.Selected).Value, "Result Selected Value");
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenSelectedValueIsNotFound_ReturnNoSelectedValue_Test()
        {
            // Arrange
            //var sut = _service;
            string expSelectedId = "99";

            // Act
            var result = _service.GetTransactionCategorySelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenSuccess_ReturnActiveTransactionTypes_Test()
        {
            // Arrange
            var _dataTransactionCategories = new List<Core.Models.TransactionCategory>()
            {
                new Core.Models.TransactionCategory() {Id = 10, Name = "Active", IsActive = true },
                new Core.Models.TransactionCategory() {Id = 11, Name = "Not Active", IsActive = false },
            };
            _unitOfWork.TransactionCategories = new InMemoryTransactionCategoryRepository(_dataTransactionCategories);
            _service = new AccountTransactionService(_unitOfWork);
            string expSelectedId = null;

            // Act
            var result = _service.GetTransactionCategorySelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual(1, result.Count, "Result Count");
        }

        [Test]
        public void GetTransactionCategorySelectList_WhenSuccess_ReturnOrderedByTransactionTypeName_Test()
        {
            // Arrange
            var _dataTransactionCategories = new List<Core.Models.TransactionCategory>()
            {
                new Core.Models.TransactionCategory() {Id = 10, Name = "Z", IsActive = true },
                new Core.Models.TransactionCategory() {Id = 11, Name = "A", IsActive = true },
                new Core.Models.TransactionCategory() {Id = 12, Name = "B", IsActive = true },
            };
            _unitOfWork.TransactionCategories = new InMemoryTransactionCategoryRepository(_dataTransactionCategories);
            _service = new AccountTransactionService(_unitOfWork);
            string expSelectedId = "99";

            // Act
            var result = _service.GetTransactionCategorySelectList(expSelectedId);

            // Assert
            Assert.IsInstanceOf(typeof(List<SelectListItem>), result, "Result Type");
            Assert.AreEqual("A", result[0].Text, "Result Name A");
            Assert.AreEqual("B", result[1].Text, "Result Name B");
            Assert.AreEqual("Z", result[2].Text, "Result Name Z");
        }



        [Test]
        public void UpdateTransaction_WhenAssetTransactionIsValid_ReturnTrue_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 2,
                TransactionTypeId = 3,
                TransactionCategoryId = 4,
            };
            //var sut = _service;

            // Act
            var result = _service.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsTrue(result, "Result");
        }

        [Test]
        public void UpdateTransaction_WhenAssetTransactionIsNull_ReturnFalse_Test()
        {
            // Arrange
            //var sut = _service;

            // Act
            var result = _service.UpdateTransaction(null);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [Test]
        public void UpdateTransaction_WhenAssetTransactionIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetTransactionId = 99,
                AssetId = 2,
                TransactionTypeId = 3,
                TransactionCategoryId = 4,
            };
            //var sut = _service;

            // Act
            var result = _service.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [Test]
        public void UpdateTransaction_WhenAssetIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 99,
                TransactionTypeId = 3,
                TransactionCategoryId = 4,
            };
            //var sut = _service;

            // Act
            var result = _service.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [Test]
        public void UpdateTransaction_WhenTransactionTypeIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 2,
                TransactionTypeId = 99,
                TransactionCategoryId = 4,
            };
            //var sut = _service;

            // Act
            var result = _service.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [Test]
        public void UpdateTransaction_WhenTransactionCateogryIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AccountTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 2,
                TransactionTypeId = 3,
                TransactionCategoryId = 99,
            };
            //var sut = _service;

            // Act
            var result = _service.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }



        [Test]
        public void GetTransactionToDelete_WhenAssetTransactionIdIsValid_ReturnAssetTransaction_Test()
        {
            // Arrange
            //var sut = _service;
            var expAssetTransactionId = 1;

            // Act
            var result = _service.GetTransactionToDelete(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOf(typeof(Business.Models.AccountTransaction), result, "Result Type");
            Assert.IsNotNull(result, "Asset Transaction List");
        }

        [Test]
        public void GetTransactionToDelete_WhenAssetTransactionIdNotFound_ReturnNull_Test()
        {
            // Arrange
            //var sut = _service;
            var expAssetTransactionId = 99;

            // Act
            var result = _service.GetTransactionToDelete(expAssetTransactionId);

            // Assert
            Assert.IsNull(result, "Result");
        }



        [Test]
        public void DeleteTransaction_WhenAssetTransactionIdIsValid_ReturnTrue_Test()
        {
            // Arrange
            //var sut = _service;
            var expAssetTransactionId = 1;

            // Act
            var result = _service.DeleteTransaction(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOf(typeof(bool), result, "Result Type");
            Assert.IsTrue(result, "Result");
        }

        [Test]
        public void DeleteTransaction_WhenAssetTransactionIdNotFound_ReturnFalse_Test()
        {
            // Arrange
            //var sut = _service;
            var expAssetTransactionId = 99;

            // Act
            var result = _service.DeleteTransaction(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOf(typeof(bool), result, "Result Type");
            Assert.IsFalse(result, "Result");
        }


    }
}