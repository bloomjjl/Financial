using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using Financial.Tests.Data.Repositories;
using Financial.Tests.Data;
using Financial.Tests.Data.Fakes;
using System.Web.Mvc;
using Financial.WebApplication.Models.ViewModels.AssetSetting;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetSettingControllerTestsBase : ControllerTestsBase
    {
        public AssetSettingControllerTestsBase()
        {
            _controller = new AssetSettingController(_unitOfWork);
        }

        protected AssetSettingController _controller;
    }

    [TestClass()]
    public class AssetSettingControllerTests : AssetSettingControllerTestsBase
    {
        [TestMethod()]
        public void Index_Get_WhenProvidedAssetIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 1, SettingTypeId = 2, Value = "Setting Value", IsActive = true } }; 
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 1;

            // Act
            var result = controller.Index(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "View Result");
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedAssetIdIsValid_ReturnActiveAssetSettings_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Setting Value 1", IsActive = true }, // count
                new AssetSetting() { Id = 11, AssetId = 20, SettingTypeId = 31, Value = "Setting Value 2", IsActive = false } }; // NOT active
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true },
                new AssetTypeSettingType() { Id = 51, AssetTypeId = 40, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;
            var expectedSettingType = "SettingType 1";
            var expectedAssetSettingValue = "Setting Value 1";

            // Act
            var result = controller.Index(assetId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "Count");
            Assert.AreEqual(expectedSettingType, vmResult[0].SettingTypeName, "SettingType Name");
            Assert.AreEqual(expectedAssetSettingValue, vmResult[0].AssetSettingValue, "AssetSetting Value");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int assetId = 1;

            // Act
            var result = controller.Create(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(CreateLinkedSettingTypesViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnActiveSettingTypes_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>(); // clear records
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = false } }; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true },
                new AssetTypeSettingType() { Id = 51, AssetTypeId = 40, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;
            var expectedAsset = "Asset";
            var expectedAssetType = "AssetType";

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedAsset, vmResult.AssetName, "Asset Name");
            Assert.AreEqual(expectedAssetType, vmResult.AssetTypeName, "AssetType Name");
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnLinkedSettingTypes_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>(); // clear records
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = true } }; // NOT linked
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;
            var expectedAsset = "Asset";
            var expectedAssetType = "AssetType";

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedAsset, vmResult.AssetName, "Asset Name");
            Assert.AreEqual(expectedAssetType, vmResult.AssetTypeName, "AssetType Name");
            Assert.AreEqual(expectedCount, vmResult.CreateViewModels.Count(), "Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["SuccessMessage"] = "Test Message";
            int assetId = 1;

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>(); // clear records
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = false }, // NOT active
                new SettingType() { Id = 32, Name = "SettingType 3", IsActive = true } }; // NOT linked
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true },
                new AssetTypeSettingType() { Id = 51, AssetTypeId = 40, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            var vmExpected = new CreateLinkedSettingTypesViewModel() {
                AssetId = 20,
                CreateViewModels = new List<CreateViewModel>() {
                    new CreateViewModel() { AssetId = 20, SettingTypeId = 30, Value = "new value" } }
            };
            int expectedCount = 1;
            int expectedAssetId = 20;
            int expectedSettingTypeId = 30;
            var expectedValue = "new value";

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataAssetSettings.Where(r => r.AssetId == expectedAssetId).Where(r => r.SettingTypeId == expectedSettingTypeId).ToList();
            Assert.AreEqual(expectedCount, dbResult.Count, "Count");
            Assert.AreEqual(expectedAssetId, dbResult[0].AssetId, "Asset Id");
            Assert.AreEqual(expectedSettingTypeId, dbResult[0].SettingTypeId, "SettingType Id");
            Assert.AreEqual(expectedValue, dbResult[0].Value, "Value");
            Assert.IsTrue(dbResult[0].IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>(); // clear records
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType", IsActive = true }}; // NOT linked
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            var vmExpected = new CreateLinkedSettingTypesViewModel()
            {
                AssetId = 20,
                CreateViewModels = new List<CreateViewModel>() {
                    new CreateViewModel() { AssetId = 20, SettingTypeId = 30, Value = "new value" } }
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.AssetId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Records created", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        // Create_Post_WhenLinkExists_UpdateDatabase_Test
        // do not create if link already exists between Asset & SettingType



        [TestMethod()]
        public void Edit_Get_WhenProvidedAssetIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Value 1", IsActive = true } }; 
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;

            // Act
            var result = controller.Edit(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(EditLinkedSettingTypesViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedAssetIdIsValid_ReturnActiveAssetSettingsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Value 1", IsActive = true }, // count
                new AssetSetting() { Id = 11, AssetId = 20, SettingTypeId = 31, Value = "Value 2", IsActive = false } }; // NOT active
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true },
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true },
                new AssetTypeSettingType() { Id = 51, AssetTypeId = 40, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Edit(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "Count");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedAssetIdIsValid_ReturnActiveSettingTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Value 1", IsActive = true }, 
                new AssetSetting() { Id = 11, AssetId = 20, SettingTypeId = 31, Value = "Value 2", IsActive = true } }; 
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = false }}; // NOT active
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true },
                new AssetTypeSettingType() { Id = 51, AssetTypeId = 40, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Edit(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "Count");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedAssetIdIsValid_ReturnLinkedAssetSettingsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Value 1", IsActive = true } };
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType 1", IsActive = true }, // count
                new SettingType() { Id = 31, Name = "SettingType 2", IsActive = true }}; // NOT linked
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true },
                new AssetTypeSettingType() { Id = 51, AssetTypeId = 40, SettingTypeId = 31, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Edit(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as EditLinkedSettingTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.EditViewModels.Count(), "Count");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Value", IsActive = true } };
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true } };
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            var vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetId = 20,
                EditViewModels = new List<EditViewModel>() {
                    new EditViewModel() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "new value" } }
            };
            int expectedId = 10;
            int expectedAssetId = 20;
            int expectedSettingTypeId = 30;
            var expectedValue = "new value";

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetSettings.FirstOrDefault(r => r.Id == expectedId);
            Assert.AreEqual(expectedAssetId, dtoResult.AssetId, "Asset Id");
            Assert.AreEqual(expectedSettingTypeId, dtoResult.SettingTypeId, "SettingType Id");
            Assert.AreEqual(expectedValue, dtoResult.Value, "Value");
            Assert.IsTrue(dtoResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetSettings = new List<AssetSetting>() {
                new AssetSetting() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "Value", IsActive = true } };
            _unitOfWork.AssetSettings = new InMemoryAssetSettingRepository(_dataAssetSettings);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 40, IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 30, Name = "SettingType", IsActive = true }}; // NOT linked
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var _dataAssetTypes = new List<AssetType>(){
                new AssetType() { Id = 40, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataAssetTypesSettingTypes = new List<AssetTypeSettingType>() {
                new AssetTypeSettingType() { Id = 50, AssetTypeId = 40, SettingTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesSettingTypes = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            var controller = new AssetSettingController(_unitOfWork);
            var vmExpected = new EditLinkedSettingTypesViewModel()
            {
                AssetId = 20,
                EditViewModels = new List<EditViewModel>() {
                    new EditViewModel() { Id = 10, AssetId = 20, SettingTypeId = 30, Value = "new value" } }
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.AssetId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Records updated", controller.TempData["SuccessMessage"].ToString(), "Message");
        }


    }
}