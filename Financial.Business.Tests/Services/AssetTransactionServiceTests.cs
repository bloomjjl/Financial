using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Tests.Fakes.Repositories;
using System.Web.Mvc;

namespace Financial.Business.Tests.Services
{
    public class AssetTransactionServiceTestsBase : ServiceTestsBase
    {
        public AssetTransactionServiceTestsBase()
        {
            _service = new AssetTransactionService(_unitOfWork);
        }

        protected AssetTransactionService _service;
    }

    public static class AssetTransactionServiceObjectMother
    {
        public static int AssetTypeIdForCreditCard = 3;
        public static int SettingTypeIdForAccountNumber = 1;
    }


    [TestClass()]
    public class AssetTransactionServiceTests : AssetTransactionServiceTestsBase
    {
        [TestMethod()]
        public void GetListOfActiveTransactions_WhenNoInputValues_ReturnList_Test()
        {
            // Arrange
            var sut = _service;

            // Act
            var result = sut.GetListOfActiveTransactions();

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Business.Models.AssetTransaction>), "Result Type");
            Assert.IsNotNull(result, "Asset Transaction List");
        }



        [TestMethod()]
        public void GetTransactionOptions_WhenAssetIdExists_ReturnAssetTransaction_Test()
        {
            // Arrange
            var expAssetId = 1;
            var sut = _service;

            // Act
            var result = sut.GetTransactionOptions(expAssetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Business.Models.AssetTransaction), "Result Type");
            Assert.IsNotNull(result, "Asset Transaction");
        }

        [TestMethod()]
        public void GetTransactionOptions_WhenAssetIdNotFound_ReturnNull_Test()
        {
            // Arrange
            int? expAssetId = 99;
            var sut = _service;

            // Act
            var result = sut.GetTransactionOptions(expAssetId);

            // Assert
            Assert.IsNull(result, "Asset Transaction");
        }

        [TestMethod()]
        public void GetTransactionOptions_WhenAssetIdIsNull_ReturnNull_Test()
        {
            // Arrange
            int? expAssetId = null;
            var sut = _service;

            // Act
            var result = sut.GetTransactionOptions(expAssetId);

            // Assert
            Assert.IsNull(result, "Asset Transaction");
        }



        [TestMethod()]
        public void AddTransaction_WhenAssetTransactionIsValid_ReturnTrue_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetId = 1,
                TransactionTypeId = 2,
                TransactionCategoryId = 3
            };
            var sut = _service;

            // Act
            var result = sut.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsTrue(result, "Result");
        }

        [TestMethod()]
        public void AddTransaction_WhenAssetIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetId = 99,
                TransactionTypeId = 2,
                TransactionCategoryId = 3
            };
            var sut = _service;

            // Act
            var result = sut.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [TestMethod()]
        public void AddTransaction_WhenTransactionTypeIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetId = 1,
                TransactionTypeId = 99,
                TransactionCategoryId = 3
            };
            var sut = _service;

            // Act
            var result = sut.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [TestMethod()]
        public void AddTransaction_WhenTransactionCateogryIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetId = 1,
                TransactionTypeId = 2,
                TransactionCategoryId = 99
            };
            var sut = _service;

            // Act
            var result = sut.AddTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }



        [TestMethod()]
        public void GetTransactionToEdit_WhenAssetTransactionIdIsValid_ReturnAssetTransaction_Test()
        {
            // Arrange
            var expAssetTransactionId = 1;
            var sut = _service;

            // Act
            var result = sut.GetTransactionToEdit(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Business.Models.AssetTransaction), "Result Type");
            Assert.IsNotNull(result, "Asset Transaction List");
            Assert.IsNotNull(result.TransactionTypeSelectList, "Transaction Type List");
            Assert.IsNotNull(result.TransactionCategorySelectList, "Transaction Category List");
        }

        [TestMethod()]
        public void GetTransactionToEdit_WhenAssetTransactionIdNotFound_ReturnNull_Test()
        {
            // Arrange
            var expAssetTransactionId = 99;
            var sut = _service;

            // Act
            var result = sut.GetTransactionToEdit(expAssetTransactionId);

            // Assert
            Assert.IsNull(result, "Result");
        }



        [TestMethod()]
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
            var sut = _service;

            // Act
            var result = sut.GetAssetIdentificationInformation(expAsset);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.IsNotNull(result, "Formatted Asset Name");
        }

        [TestMethod()]
        public void GetAssetIdentificationInformation_WhenAssetIsNull_ReturnEmptyString_Test()
        {
            // Arrange
            var sut = _service;

            // Act
            var result = sut.GetAssetIdentificationInformation(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual(string.Empty, result, "Formatted Asset Name");
        }

        [TestMethod()]
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
            var sut = new AssetTransactionService(_unitOfWork);

            // Act
            var result = sut.GetAssetIdentificationInformation(expAsset);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string), "Result Type");
            Assert.AreEqual(expAssetName, result, "Asset Name");
        }



        [TestMethod()]
        public void GetTransactionTypeSelectList_WhenSuccess_ReturnSelectList_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = null;

            // Act
            var result = sut.GetTransactionTypeSelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.IsNotNull(result, "Result");
            Assert.AreNotEqual(0, result.Count, "Result Count");
        }

        [TestMethod()]
        public void GetTransactionTypeSelectList_WhenSelectedValueIsNull_ReturnNoSelectedValue_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = null;

            // Act
            var result = sut.GetTransactionTypeSelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [TestMethod()]
        public void GetTransactionTypeSelectList_WhenSelectedValueIsFound_ReturnSelectedValue_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = "1";

            // Act
            var result = sut.GetTransactionTypeSelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.AreEqual(expSelectedValue, result.FirstOrDefault(r => r.Selected).Value, "Result Selected Value");
        }

        [TestMethod()]
        public void GetTransactionTypeSelectList_WhenSelectedValueIsNotFound_ReturnNoSelectedValue_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = "99";

            // Act
            var result = sut.GetTransactionTypeSelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [TestMethod()]
        public void GetTransactionTypeSelectList_WhenSuccess_ReturnActiveTransactionTypes_Test()
        {
            // Arrange
            var _dataTransactionTypes = new List<Core.Models.TransactionType>()
            {
                new Core.Models.TransactionType() {Id = 10, Name = "Active", IsActive = true },
                new Core.Models.TransactionType() {Id = 11, Name = "Not Active", IsActive = false },
            };
            _unitOfWork.TransactionTypes = new InMemoryTransactionTypeRepository(_dataTransactionTypes);
            var sut = new AssetTransactionService(_unitOfWork);
            string expSelectedValue = null;

            // Act
            var result = sut.GetTransactionTypeSelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.AreEqual(1, result.Count, "Result Count");
        }

        [TestMethod()]
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
            var sut = new AssetTransactionService(_unitOfWork);
            string expSelectedValue = "99";

            // Act
            var result = sut.GetTransactionTypeSelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.AreEqual("A", result[0].Text, "Result Name A");
            Assert.AreEqual("B", result[1].Text, "Result Name B");
            Assert.AreEqual("Z", result[2].Text, "Result Name Z");
        }



        [TestMethod()]
        public void GetTransactionCategorySelectList_WhenSuccess_ReturnSelectList_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = null;

            // Act
            var result = sut.GetTransactionCategorySelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.IsNotNull(result, "Result");
            Assert.AreNotEqual(0, result.Count, "Result Count");
        }

        [TestMethod()]
        public void GetTransactionCategorySelectList_WhenSelectedValueIsNull_ReturnNoSelectedValue_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = null;

            // Act
            var result = sut.GetTransactionCategorySelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [TestMethod()]
        public void GetTransactionCategorySelectList_WhenSelectedValueIsFound_ReturnSelectedValue_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = "1";

            // Act
            var result = sut.GetTransactionCategorySelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.AreEqual(expSelectedValue, result.FirstOrDefault(r => r.Selected).Value, "Result Selected Value");
        }

        [TestMethod()]
        public void GetTransactionCategorySelectList_WhenSelectedValueIsNotFound_ReturnNoSelectedValue_Test()
        {
            // Arrange
            var sut = _service;
            string expSelectedValue = "99";

            // Act
            var result = sut.GetTransactionCategorySelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.IsFalse(result.Any(r => r.Selected), "Result Selected Value");
        }

        [TestMethod()]
        public void GetTransactionCategorySelectList_WhenSuccess_ReturnActiveTransactionTypes_Test()
        {
            // Arrange
            var _dataTransactionCategories = new List<Core.Models.TransactionCategory>()
            {
                new Core.Models.TransactionCategory() {Id = 10, Name = "Active", IsActive = true },
                new Core.Models.TransactionCategory() {Id = 11, Name = "Not Active", IsActive = false },
            };
            _unitOfWork.TransactionCategories = new InMemoryTransactionCategoryRepository(_dataTransactionCategories);
            var sut = new AssetTransactionService(_unitOfWork);
            string expSelectedValue = null;

            // Act
            var result = sut.GetTransactionCategorySelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.AreEqual(1, result.Count, "Result Count");
        }

        [TestMethod()]
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
            var sut = new AssetTransactionService(_unitOfWork);
            string expSelectedValue = "99";

            // Act
            var result = sut.GetTransactionCategorySelectList(expSelectedValue);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<SelectListItem>), "Result Type");
            Assert.AreEqual("A", result[0].Text, "Result Name A");
            Assert.AreEqual("B", result[1].Text, "Result Name B");
            Assert.AreEqual("Z", result[2].Text, "Result Name Z");
        }



        [TestMethod()]
        public void UpdateTransaction_WhenAssetTransactionIsValid_ReturnTrue_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 2,
                TransactionTypeId = 3,
                TransactionCategoryId = 4,
            };
            var sut = _service;

            // Act
            var result = sut.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsTrue(result, "Result");
        }

        [TestMethod()]
        public void UpdateTransaction_WhenAssetTransactionIsNull_ReturnFalse_Test()
        {
            // Arrange
            var sut = _service;

            // Act
            var result = sut.UpdateTransaction(null);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [TestMethod()]
        public void UpdateTransaction_WhenAssetTransactionIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetTransactionId = 99,
                AssetId = 2,
                TransactionTypeId = 3,
                TransactionCategoryId = 4,
            };
            var sut = _service;

            // Act
            var result = sut.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [TestMethod()]
        public void UpdateTransaction_WhenAssetIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 99,
                TransactionTypeId = 3,
                TransactionCategoryId = 4,
            };
            var sut = _service;

            // Act
            var result = sut.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [TestMethod()]
        public void UpdateTransaction_WhenTransactionTypeIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 2,
                TransactionTypeId = 99,
                TransactionCategoryId = 4,
            };
            var sut = _service;

            // Act
            var result = sut.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }

        [TestMethod()]
        public void UpdateTransaction_WhenTransactionCateogryIdIsNotFound_ReturnFalse_Test()
        {
            // Arrange
            var expAssetTransaction = new Business.Models.AssetTransaction()
            {
                AssetTransactionId = 1,
                AssetId = 2,
                TransactionTypeId = 3,
                TransactionCategoryId = 99,
            };
            var sut = _service;

            // Act
            var result = sut.UpdateTransaction(expAssetTransaction);

            // Assert
            Assert.IsFalse(result, "Result");
        }



        [TestMethod()]
        public void GetTransactionToDelete_WhenAssetTransactionIdIsValid_ReturnAssetTransaction_Test()
        {
            // Arrange
            var sut = _service;
            var expAssetTransactionId = 1;

            // Act
            var result = sut.GetTransactionToDelete(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Business.Models.AssetTransaction), "Result Type");
            Assert.IsNotNull(result, "Asset Transaction List");
        }

        [TestMethod()]
        public void GetTransactionToDelete_WhenAssetTransactionIdNotFound_ReturnNull_Test()
        {
            // Arrange
            var sut = _service;
            var expAssetTransactionId = 99;

            // Act
            var result = sut.GetTransactionToDelete(expAssetTransactionId);

            // Assert
            Assert.IsNull(result, "Result");
        }



        [TestMethod()]
        public void DeleteTransaction_WhenAssetTransactionIdIsValid_ReturnTrue_Test()
        {
            // Arrange
            var sut = _service;
            var expAssetTransactionId = 1;

            // Act
            var result = sut.DeleteTransaction(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(bool), "Result Type");
            Assert.IsTrue(result, "Result");
        }

        [TestMethod()]
        public void DeleteTransaction_WhenAssetTransactionIdNotFound_ReturnFalse_Test()
        {
            // Arrange
            var sut = _service;
            var expAssetTransactionId = 99;

            // Act
            var result = sut.DeleteTransaction(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(bool), "Result Type");
            Assert.IsFalse(result, "Result");
        }


    }
}