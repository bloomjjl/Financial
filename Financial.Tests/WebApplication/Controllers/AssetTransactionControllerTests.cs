﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using System.Web.Mvc;
using Financial.Tests.Data.Repositories;
using Financial.Core.ViewModels.AssetTransaction;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetTransactionControllerTestsBase : ControllerTestsBase
    {
        public AssetTransactionControllerTestsBase()
        {
            _controller = new AssetTransactionController(_unitOfWork);
        }

        protected AssetTransactionController _controller;
    }

    [TestClass()]
    public class AssetTransactionControllerTests : AssetTransactionControllerTestsBase
    {

        [TestMethod()]
        public void Index_Get_WhenProvidedAssetIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
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
        public void Index_Get_WhenProvidedAssetIdIsValid_ReturnActiveRecordsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>() {
                new AssetTransaction() {
                    Id = 10, AssetId = 1, TransactionDate = new DateTime(2018, 1, 15), Amount = 111.11M,  
                    TransactionTypeId = 2, TransactionCategoryId = 4, TransactionDescriptionId = 5, IsActive = true }, // count
                new AssetTransaction() {
                    Id = 11, AssetId = 1, TransactionDate = new DateTime(2018, 2, 16), Amount = 222.22M, 
                    TransactionTypeId = 2, TransactionCategoryId = 4, TransactionDescriptionId = 5, IsActive = false } }; // NOT active
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var controller = new AssetTransactionController(_unitOfWork);
            int assetId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.Index(assetId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "Count");
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
            Assert.IsInstanceOfType(viewResult.Model, typeof(CreateViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnAssetValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>(); // clear records
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 30, Name = "Asset", IsActive = true } }; // NOT active
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType", IsActive = true } }; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTransactionController(_unitOfWork);
            int assetId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateViewModel;
            Assert.AreEqual(assetId, vmResult.Id, "Asset Id");
            Assert.AreEqual("Asset", vmResult.AssetName, "Asset Name");
            Assert.AreEqual("AssetType", vmResult.AssetTypeName, "AssetType Name");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnActiveTransactionTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>(); // clear records
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var _dataTransactionTypes = new List<TransactionType>() {
                new TransactionType() { Id = 30, Name = "TransactionType 1", IsActive = true }, // count
                new TransactionType() { Id = 31, Name = "TransactionType 2", IsActive = false } }; // NOT active
            _unitOfWork.TransactionTypes = new InMemoryTransactionTypeRepository(_dataTransactionTypes);
            var controller = new AssetTransactionController(_unitOfWork);
            int assetId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.TransactionTypes.Count(), "Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnActiveTransactionCategoriesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>(); // clear records
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var _dataTransactionCategories = new List<TransactionCategory>() {
                new TransactionCategory() { Id = 40, Name = "TransactionCategory 1", IsActive = true }, // count
                new TransactionCategory() { Id = 41, Name = "TransactionCategory 2", IsActive = false } }; // NOT active
            _unitOfWork.TransactionCategories = new InMemoryTransactionCategoryRepository(_dataTransactionCategories);
            var controller = new AssetTransactionController(_unitOfWork);
            int assetId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.TransactionCategories.Count(), "Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetIdIsValid_ReturnActiveTransactionDescriptionsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>(); // clear records
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var _dataTransactionDescriptions = new List<TransactionDescription>() {
                new TransactionDescription() { Id = 50, Name = "TransactionDescription 1", IsActive = true }, // count
                new TransactionDescription() { Id = 51, Name = "TransactionDescription 2", IsActive = false } }; // NOT active
            _unitOfWork.TransactionDescriptions= new InMemoryTransactionDescriptionRepository(_dataTransactionDescriptions);
            var controller = new AssetTransactionController(_unitOfWork);
            int assetId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.Create(assetId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.TransactionDescriptions.Count(), "Count");
        }


    }
}