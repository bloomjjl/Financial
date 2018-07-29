using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Financial.Core.Models;
using Financial.WebApplication.Tests.Fakes.Repositories;
using Financial.WebApplication.Models.ViewModels.AssetTransaction;
using Financial.Business.ServiceInterfaces;
using System.ComponentModel.DataAnnotations;

namespace Financial.WebApplication.Tests.WebApplication.Controllers
{
    public class AssetTransactionControllerTestsBase : ControllerTestsBase
    {
        public AssetTransactionControllerTestsBase()
        {
            _controller = new AssetTransactionController(_unitOfWork, _assetTransactionService);
        }

        protected AssetTransactionController _controller;
        
    }

    public class AssetTransactionControllerTestObjectsMother
    {

    }

    [TestClass()]
    public class AssetTransactionControllerTests : AssetTransactionControllerTestsBase
    {
        [TestMethod()]
        public void Index_Get_WhenNoInput_ReturnIndexView_Test()
        {
            // Arrange
            var sut = _controller;

            // Act
            var result = sut.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result Type");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
            var viewModel = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreNotEqual(0, viewModel.Count, "View Model");
        }

        [TestMethod()]
        public void Index_Get_WhenErrorMessageNotNull_ReturnMessage_Test()
        {
            // Arrange
            var sut = _controller;

            // Act
            sut.TempData["ErrorMessage"] = "Test Message";
            var result = sut.Index();

            // Assert
            Assert.IsNotNull(sut.ViewData["ErrorMessage"], "Result Message");
        }

        [TestMethod()]
        public void Index_Get_WhenSuccessMessageNotNull_ReturnMessage_Test()
        {
            // Arrange
            var sut = _controller;

            // Act
            sut.TempData["SuccessMessage"] = "Test Message";
            var result = sut.Index();

            // Assert
            Assert.IsNotNull(sut.ViewData["SuccessMessage"], "Result Message");
        }



        [TestMethod()]
        public void Create_Get_WhenAssetIdIsValid_ReturnCreateView_Test()
        {
            // Arrange
            int assetId = 1;
            var sut = _controller;

            // Act
            var result = sut.Create(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result Type");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(CreateViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Get_WhenAssetIdNotFound_ReturnIndexView_Test()
        {
            // Arrange
            int assetId = 99;
            var sut = _controller;

            // Act
            var result = sut.Create(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Result Type");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Result Action");
            Assert.AreEqual("AssetTransaction", routeResult.RouteValues["controller"], "Result Controller");
            Assert.IsNotNull(sut.TempData["ErrorMessage"], "Result Message");
        }

        [TestMethod()]
        public void Create_Get_WhenAssetIdIsNull_ReturnIndexView_Test()
        {
            // Arrange
            int? assetId = null;
            var sut = _controller;

            // Act
            var result = sut.Create(assetId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Result Type");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Result Action");
            Assert.AreEqual("AssetTransaction", routeResult.RouteValues["controller"], "Result Controller");
            Assert.IsNotNull(sut.TempData["ErrorMessage"], "Result Message");
        }



        [TestMethod()]
        public void Create_Post_WhenSuccess_ReturnAssetDetailsView_Test()
        {
            // Arrange
            var expViewModel = new CreateViewModel()
            {
                DueDate = DateTime.Now,
                Amount = 99,
                SelectedAssetId = "1",
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "3",
            };
            var sut = _controller;

            // Act
            var result = sut.Create(expViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Result Type");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Result Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Result Controller");
            Assert.AreEqual(expViewModel.AssetId, routeResult.RouteValues["id"], "Result id");
            Assert.IsNotNull(sut.TempData["SuccessMessage"], "Result Message");
        }

        [TestMethod()]
        public void Create_Post_WhenViewModelIsValid_ReturnTrue_Test()
        {
            // Arrange
            var expViewModel = new CreateViewModel()
            {
                DueDate = DateTime.Now,
                Amount = 99,
                SelectedAssetId = "1",
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "3",
            };
            var expValidationContext = new ValidationContext(expViewModel, null, null);
            var expValidationResult = new List<ValidationResult>();

            // Act
            var modelValidation = Validator.TryValidateObject(expViewModel, expValidationContext, expValidationResult, true);

            // Assert
            Assert.IsTrue(modelValidation);
        }

        [TestMethod()]
        public void Create_Post_WhenViewModelIsNotValid_ReturnAssetDetailsView_Test()
        {
            // Arrange
            var viewModel = new CreateViewModel();
            var context = new ValidationContext(viewModel, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(viewModel, context, result, true);

            // Assert
            Assert.IsFalse(valid);
        }

        [TestMethod()]
        public void Create_Post_WhenModelStateHasError_ReturnIndexView_Test()
        {
            // Arrange
            var expViewModel = new CreateViewModel();
            var sut = _controller;
            sut.ModelState.AddModelError("test", "test");

            // Act
            var result = sut.Create(expViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Result Type");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Result Action");
            Assert.AreEqual("AssetTransaction", routeResult.RouteValues["controller"], "Result Controller");
            Assert.IsNotNull(sut.TempData["ErrorMessage"], "Result Message");
        }

        [TestMethod()]
        public void Create_Post_WhenSelectedAssetIdIsNotValid_ReturnCreateView_Test()
        {
            // Arrange
            var expViewModel = new CreateViewModel()
            {
                SelectedAssetId = string.Empty,
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "3",
                DueDate = DateTime.Now,
            };
            var sut = _controller;

            // Act
            var result = sut.Create(expViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result Type");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(CreateViewModel), "View Model");
            Assert.IsNotNull(sut.ViewData["ErrorMessage"], "Result Message");
        }

        [TestMethod()]
        public void Create_Post_WhenSelectedTransactionTypeIsNotValid_ReturnCreateView_Test()
        {
            // Arrange
            var expViewModel = new CreateViewModel()
            {
                SelectedAssetId = "1",
                SelectedTransactionTypeId = string.Empty,
                SelectedTransactionCategoryId = "3",
                DueDate = DateTime.Now,
            };
            var sut = _controller;

            // Act
            var result = sut.Create(expViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result Type");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(CreateViewModel), "View Model");
            Assert.IsNotNull(sut.ViewData["ErrorMessage"], "Result Message");
        }

        [TestMethod()]
        public void Create_Post_WhenSelectedTransactionCategoryIdIsNotValid_ReturnCreateView_Test()
        {
            // Arrange
            var expViewModel = new CreateViewModel()
            {
                SelectedAssetId = "1",
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = string.Empty,
                DueDate = DateTime.Now,
            };
            var sut = _controller;

            // Act
            var result = sut.Create(expViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result Type");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(CreateViewModel), "View Model");
            Assert.IsNotNull(sut.ViewData["ErrorMessage"], "Result Message");
        }



        [TestMethod()]
        public void Edit_Get_WhenAssetTransactionIdIsValid_ReturnEditView_Test()
        {
            // Arrange
            int expAssetTransactionId = 1;
            var sut = _controller;

            // Act
            var result = sut.Edit(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Result Type");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenAssetTransactionIdNotFound_ReturnEditView_Test()
        {
            // Arrange
            int expAssetTransactionId = 99;
            var sut = _controller;

            // Act
            var result = sut.Edit(expAssetTransactionId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Result Type");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Result Action");
            Assert.AreEqual("AssetTransaction", routeResult.RouteValues["controller"], "Result Controller");
            Assert.IsNotNull(sut.TempData["ErrorMessage"], "Result Message");
        }




        [TestMethod()]
        public void Edit_Post_WhenSuccess_ReturnAssetDetailsView_Test()
        {
            // Arrange
            var expViewModel = new EditViewModel()
            {
                DueDate = DateTime.Now,
                Amount = 99,
                AssetId = 1,
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "3",
            };
            var sut = _controller;

            // Act
            var result = sut.Edit(expViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Result Type");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Result Action");
            Assert.AreEqual("Asset", routeResult.RouteValues["controller"], "Result Controller");
            Assert.AreEqual(expViewModel.AssetId, routeResult.RouteValues["id"], "Result id");
            Assert.IsNotNull(sut.TempData["SuccessMessage"], "Result Message");
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
            var controller = new AssetTransactionController(_unitOfWork, _assetTransactionService);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                AssetId = 1,
                SelectedTransactionTypeId = "2",
                SelectedTransactionCategoryId = "4",
                DueDate = DateTime.Now,
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
            var controller = new AssetTransactionController(_unitOfWork, _assetTransactionService);
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


    }
}