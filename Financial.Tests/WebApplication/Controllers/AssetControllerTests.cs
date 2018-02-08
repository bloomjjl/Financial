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
using Financial.Core.ViewModels.Asset;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetControllerTestsBase : ControllerTestsBase
    {
        public AssetControllerTestsBase()
        {
            _controller = new AssetController(_unitOfWork);
        }

        protected AssetController _controller;
    }

    [TestClass()]
    public class AssetControllerTests : AssetControllerTestsBase
    {
        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputVaues_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 10, AssetTypeId = 20, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Asset Type", IsActive = true }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            int expectedCount = 1;
            int expectedAssetId = 10;
            var expectedAssetName = "Asset";
            var expectedAssetTypeName = "Asset Type";

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "Number of records");
            Assert.IsNotNull(vmResult.FirstOrDefault(r => r.Id == expectedAssetId), "Asset Id");
            Assert.IsNotNull(vmResult.FirstOrDefault(r => r.AssetName == expectedAssetName), "Asset Name");
            Assert.IsNotNull(vmResult.FirstOrDefault(r => r.AssetTypeName == expectedAssetTypeName), "AssetType Name");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["SuccessMessage"] = "Test Message";

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedErrorMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Message";

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["ErrorMessage"].ToString(), "Message");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(CreateViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>(); // clear records
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Asset Type 1", IsActive = true }, // count
                new AssetType() { Id = 21, Name = "Asset Type 2", IsActive = false }}; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            int expectedCount = 1;
            var expectedValue = "20";
            var expectedText = "Asset Type 1";

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "AssetType List Count");
            Assert.IsNotNull(vmResult.AssetTypes.FirstOrDefault(r => r.Value == expectedValue), "AssetType Value");
            Assert.IsNotNull(vmResult.AssetTypes.FirstOrDefault(r => r.Text == expectedText), "AssetType Text");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "AssetType NOT Selected");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>(); // clear records
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Asset Type", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SelectedAssetTypeId = "20",
                AssetName = "New Name"
            };
            int newId = 1; // first record

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataAssets.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.AssetName, dbResult.Name, "Name");
            Assert.AreEqual(vmExpected.SelectedAssetTypeId, dbResult.AssetTypeId.ToString(), "Id");
            Assert.IsTrue(dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>(); // clear records
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Asset Type", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SelectedAssetTypeId = "20",
                AssetName = "New Name"
            };
            int newId = 1; // first record

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Create", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetSetting", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(newId, routeResult.RouteValues["assetId"], "Asset Id");
            Assert.AreEqual("Asset Created", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        // Create_Post_WhenNameIsDuplicated_ReturnRouteValues_Test
        // do not duplicated Asset.Name & AssetType.Id


        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 1;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 10, AssetTypeId = 20, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType", IsActive = true }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            int id = 10;
            var expectedName = "Asset";
            var expectedAssetTypeId = "20";

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "Asset Id");
            Assert.AreEqual(expectedName, vmResult.Name, "Asset Name");
            Assert.AreEqual(expectedAssetTypeId, vmResult.SelectedAssetTypeId, "AssetType Id");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 10, AssetTypeId = 20, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType 1", IsActive = true }, // count: Selected
                new AssetType() { Id = 21, Name = "AssetType 2", IsActive = false }, // NOT active
                new AssetType() { Id = 22, Name = "AssetType 3", IsActive = true }}; // count
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            int id = 10;
            int count = 2;
            var expectedAssetTypeId = "20";

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(count, vmResult.AssetTypes.Count(), "Count");
            Assert.AreEqual(expectedAssetTypeId, vmResult.SelectedAssetTypeId, "Selected AssetType Id");
        }



        [TestMethod()]
        public void Details_Get_WhenProvidedAssetIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 1;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DetailsViewModel), "View Model");
        }

        [TestMethod()]
        public void Details_Get_WhenProvidedAssetIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 10, AssetTypeId = 20, Name = "Asset", IsActive = true } };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Asset Type", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetController(_unitOfWork);
            int id = 10;
            var expectedAssetName = "Asset";
            var expectedAssetTypeName = "Asset Type";

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            Assert.AreEqual(id, vmResult.Id, "Asset Id");
            Assert.AreEqual(expectedAssetName, vmResult.Name, "Asset Name");
            Assert.AreEqual(expectedAssetTypeName, vmResult.AssetTypeName, "AssetType Name");
        }

    }
}