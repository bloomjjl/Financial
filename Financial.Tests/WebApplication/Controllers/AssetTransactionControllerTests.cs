using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using System.Web.Mvc;
using Financial.Tests.Data.Repositories;
using Financial.WebApplication.Models.ViewModels.AssetTransaction;

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

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedAssetIdIsValid_ReturnActiveRecordsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>() {
                new AssetTransaction() {
                    Id = 10, AssetId = 20, DueDate = new DateTime(2018, 1, 15), Amount = 111.11M,
                    TransactionTypeId = 30, TransactionCategoryId = 4, TransactionDescriptionId = 5, IsActive = true }, // count
                new AssetTransaction() {
                    Id = 11, AssetId = 20, DueDate = new DateTime(2018, 2, 16), Amount = 222.22M, 
                    TransactionTypeId = 30, TransactionCategoryId = 4, TransactionDescriptionId = 5, IsActive = false } }; // NOT active
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 1, Name = "Asset 1", IsActive = true }
            };
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataTransactionTypes = new List<TransactionType>() {
                new TransactionType() { Id = 30, Name = "Income", IsActive = true } };
            _unitOfWork.TransactionTypes = new InMemoryTransactionTypeRepository(_dataTransactionTypes);
            var controller = new AssetTransactionController(_unitOfWork);
            int expectedCount = 1;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "Count");
            Assert.AreEqual(10, vmResult[0].Id, "AssetTransaction Id");
            Assert.AreEqual(20, vmResult[0].AssetId, "Asset Id");
            Assert.AreEqual("Asset 1", vmResult[0].AssetName, "Asset Name");
            Assert.AreEqual(new DateTime(2018, 1, 15).ToString("MM/dd/yyyy"), vmResult[0].DueDate, "AssetTransaction Date");
            Assert.AreEqual(111.11M, vmResult[0].Amount, "AssetTransaction Amount");
            Assert.AreEqual("Income", vmResult[0].TransactionType, "TransactionType");
        }



        [TestMethod()]
        public void DisplayForAsset_Get_WhenProvidedAssetIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int assetId = 1;

            // Act
            var result = controller.DisplayForAsset(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "View Result");
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_DisplayForAsset", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<DisplayForAssetViewModel>), "View Model");
        }

        [TestMethod()]
        public void DisplayForAsset_Get_WhenProvidedAssetIdIsValid_ReturnActiveRecordsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>() {
                new AssetTransaction() {
                    Id = 10, AssetId = 1, DueDate = new DateTime(2018, 1, 15), Amount = 111.11M,  Note = "Test Note 1",
                    TransactionTypeId = 2, TransactionCategoryId = 4, TransactionDescriptionId = 5, IsActive = true }, // count
                new AssetTransaction() {
                    Id = 11, AssetId = 1, DueDate = new DateTime(2018, 2, 16), Amount = 222.22M, Note = "Test Note 2",
                    TransactionTypeId = 2, TransactionCategoryId = 4, TransactionDescriptionId = 5, IsActive = false } }; // NOT active
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var controller = new AssetTransactionController(_unitOfWork);
            int assetId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayForAsset(assetId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.Model as List<DisplayForAssetViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "Count");
            Assert.AreEqual(10, vmResult[0].Id, "AssetTransaction Id");
            Assert.AreEqual(new DateTime(2018, 1, 15).ToString("MM/dd/yyyy"), vmResult[0].DueDate, "AssetTransaction Date");
            Assert.AreEqual(111.11M, vmResult[0].Amount, "AssetTransaction Amount");
            Assert.AreEqual("Test Note 1", vmResult[0].Note, "AssetTransaction Note");
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
            Assert.AreEqual(assetId, vmResult.AssetId, "Asset Id");
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

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>(); // clear records
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var controller = new AssetTransactionController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                AssetId = 5,
                CheckNumber = "123",
                SelectedTransactionTypeId = "1",
                SelectedTransactionCategoryId = "2",
                SelectedTransactionDescriptionId = "4",
                DueDate = DateTime.Now.ToShortDateString(),
                Amount = 9.99M,
                Note = "this is a note"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTransactions.FirstOrDefault(r => r.AssetId == vmExpected.AssetId);
            Assert.AreEqual(vmExpected.CheckNumber, dtoResult.CheckNumber, "CheckNumber");
            Assert.AreEqual(vmExpected.SelectedTransactionTypeId, dtoResult.TransactionTypeId.ToString(), "Type ID");
            Assert.AreEqual(vmExpected.SelectedTransactionCategoryId, dtoResult.TransactionCategoryId.ToString(), "Category ID");
            Assert.AreEqual(vmExpected.SelectedTransactionDescriptionId, dtoResult.TransactionDescriptionId.ToString(), "Description ID");
            Assert.IsNotNull(dtoResult.DueDate, "Date Found");
            Assert.AreNotEqual(new DateTime(), dtoResult.DueDate, "Date Valid");
            Assert.AreEqual(vmExpected.Amount, dtoResult.Amount, "Amount");
            Assert.AreEqual(vmExpected.Note, dtoResult.Note, "Note");
            Assert.IsTrue(dtoResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTransactions = new List<AssetTransaction>(); // clear records
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var controller = new AssetTransactionController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                AssetId = 1,
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "4",
                SelectedTransactionDescriptionId = "5",
                DueDate = DateTime.Now.ToShortDateString(),
                Amount = 20.99M
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.AssetId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Record created", controller.TempData["SuccessMessage"].ToString(), "Message");
        }



        [TestMethod()]
        public void Edit_Get_WhenProvidedAssetTransactionIdIsValid_ReturnRouteValues_Test()
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
            Assert.IsInstanceOfType(viewResult.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedAssetIdIsValid_ReturnValuesFromDatabase_Test()
        {
            // Arrange
            var dueDate = DateTime.Now;
            var clearDate = dueDate.AddMonths(2);
            var _dataAssetTransactions = new List<AssetTransaction>() {
                new AssetTransaction() {
                    Id = 10, AssetId = 20, TransactionTypeId = 1, TransactionCategoryId = 2, TransactionDescriptionId = 4,
                    CheckNumber = "1234", DueDate = dueDate, ClearDate = clearDate, Amount = 199.99M, Note = "Test Note", IsActive = true }}; 
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var _dataAssets = new List<Asset>() {
                new Asset() { Id = 20, AssetTypeId = 30, Name = "Asset", IsActive = true } }; // NOT active
            _unitOfWork.Assets = new InMemoryAssetRepository(_dataAssets);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 30, Name = "AssetType", IsActive = true } }; // NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTransactionController(_unitOfWork);
            int id = 10;

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "AssetTransaction Id");
            Assert.AreEqual(20, vmResult.AssetId, "Asset Id");
            Assert.AreEqual("Asset", vmResult.AssetName, "Asset Name");
            Assert.AreEqual("AssetType", vmResult.AssetTypeName, "AssetType Name");
            Assert.AreEqual("1", vmResult.SelectedTransactionTypeId, "Selected TransactionType Id");
            Assert.AreEqual("2", vmResult.SelectedTransactionCategoryId, "Selected TransactionCategory Id");
            Assert.AreEqual(dueDate.ToString("MM/dd/yyyy"), vmResult.DueDate, "DueDate");
            Assert.AreEqual(clearDate.ToString("MM/dd/yyyy"), vmResult.ClearDate, "ClearDate");
            Assert.AreEqual("1234", vmResult.CheckNumber, "CheckNumber");
            Assert.AreEqual(199.99M, vmResult.Amount, "Ammount");
            Assert.AreEqual("Test Note", vmResult.Note, "Note");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var dueDate = DateTime.Now;
            var clearDate = dueDate.AddMonths(2);
            var _dataAssetTransactions = new List<AssetTransaction>() {
                new AssetTransaction() {
                    Id = 10, AssetId = 5, TransactionTypeId = 1, TransactionCategoryId = 2, TransactionDescriptionId = 4,
                    CheckNumber = "1234", DueDate = dueDate, Amount = 199.99M, Note = "Test Note", IsActive = true }};
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var controller = new AssetTransactionController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                AssetId = 5,
                CheckNumber = "123", // updated
                SelectedTransactionTypeId = "2", // updated
                SelectedTransactionCategoryId = "4", // updated
                DueDate = dueDate.AddMonths(2).ToString("MM/dd/yyyy"), // updated
                ClearDate = clearDate.ToString("MM/dd/yyyy"), // added
                Amount = 9.99M, // updated
                Note = "this is a note" // updated
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dtoResult = _dataAssetTransactions.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.AssetId, dtoResult.AssetId, "Asset Id");
            Assert.AreEqual(vmExpected.CheckNumber, dtoResult.CheckNumber, "CheckNumber");
            Assert.AreEqual(vmExpected.SelectedTransactionTypeId, dtoResult.TransactionTypeId.ToString(), "Type ID");
            Assert.AreEqual(vmExpected.SelectedTransactionCategoryId, dtoResult.TransactionCategoryId.ToString(), "Category ID");
            Assert.AreEqual(vmExpected.DueDate, dtoResult.DueDate.ToString("MM/dd/yyyy"), "DueDate");
            Assert.AreEqual(vmExpected.ClearDate, dtoResult.ClearDate.ToString("MM/dd/yyyy"), "ClearDate");
            Assert.AreEqual(vmExpected.Amount, dtoResult.Amount, "Amount");
            Assert.AreEqual(vmExpected.Note, dtoResult.Note, "Note");
            Assert.IsTrue(dtoResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var dueDate = DateTime.Now;
            var _dataAssetTransactions = new List<AssetTransaction>() {
                new AssetTransaction() {
                    Id = 10, AssetId = 5, TransactionTypeId = 1, TransactionCategoryId = 2, TransactionDescriptionId = 4,
                    CheckNumber = "1234", DueDate = dueDate, Amount = 199.99M, Note = "Test Note", IsActive = true }};
            _unitOfWork.AssetTransactions = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            var controller = new AssetTransactionController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                AssetId = 1,
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "4",
                DueDate = DateTime.Now.ToString("MM/dd/yyyy"),
                Amount = 20.99M,
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.AssetId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Record updated", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

    }
}