﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class AssetControllerTestsBase
    {
        public AssetControllerTestsBase()
        {
            // Fake Data
            _assets = FakeAssets.InitialFakeAssets().ToList();
            _assetTypes = FakeAssetTypes.InitialFakeAssetTypes().ToList();
            _assetTypesSettingTypes = FakeAssetTypesSettingTypes.InitialFakeAssetTypesSettingTypes().ToList();
            _settingTypes = FakeSettingTypes.InitialFakeSettingTypes().ToList();
            // Fake Repositories
            _repositoryAsset = new InMemoryAssetRepository(_assets);
            _repositoryAssetType = new InMemoryAssetTypeRepository(_assetTypes);
            _repositoryAssetTypeSettingType = new InMemoryAssetTypeSettingTypeRepository(_assetTypesSettingTypes);
            _repositorySettingType = new InMemorySettingTypeRepository(_settingTypes);
            // fake unit of work
            _unitOfWork.Assets = _repositoryAsset;
            _unitOfWork.AssetTypes = _repositoryAssetType;
            _unitOfWork.AssetTypesSettingTypes = _repositoryAssetTypeSettingType;
            _unitOfWork.SettingTypes = _repositorySettingType;
            // fake controller
            _controller = new AssetController(_unitOfWork);
        }

        protected IList<Asset> _assets;
        protected IList<AssetType> _assetTypes;
        protected IList<AssetTypeSettingType> _assetTypesSettingTypes;
        protected IList<SettingType> _settingTypes;
        protected InMemoryAssetRepository _repositoryAsset;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryAssetTypeSettingTypeRepository _repositoryAssetTypeSettingType;
        protected InMemorySettingTypeRepository _repositorySettingType;
        protected InMemoryUnitOfWork _unitOfWork = new InMemoryUnitOfWork();
        protected AssetController _controller;
    }

    [TestClass()]
    public class AssetControllerTests : AssetControllerTestsBase
    {
        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnRouteValues_Test()
        {
            // Arrange
            AssetController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(CreateViewModel));
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnActiveAssetTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Asset Type", IsActive = true }); // count
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Asset Type", IsActive = false }); // NOT active
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Asset Type", IsActive = true }); // count
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetController controller = new AssetController(_unitOfWork);
            int expectedCount = 2;

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "AssetType List Count");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            AssetController controller = _controller;
            CreateViewModel vmExpected = new CreateViewModel()
            {
                SelectedAssetTypeId = "1",
                AssetName = "New Name"
            };
            int newId = _assets.Count() + 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _assets.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.AssetName, dbResult.Name, "Asset Name");
            Assert.AreEqual(vmExpected.SelectedAssetTypeId, dbResult.AssetTypeId.ToString(), "AssetType Id");
            Assert.AreEqual(true, dbResult.IsActive, "Asset IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetController controller = _controller;
            CreateViewModel vmExpected = new CreateViewModel()
            {
                SelectedAssetTypeId = "1",
                AssetName = "New Name"
            };
            int newId = _assets.Count() + 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Asset Created", controller.TempData["SuccessMessage"].ToString(), "Success Message");
        }

    }
}