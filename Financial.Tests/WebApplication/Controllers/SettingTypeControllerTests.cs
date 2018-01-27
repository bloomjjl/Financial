using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Tests.Data.Fakes;
using Financial.Tests.Data.Repositories;
using Financial.Tests.Data;
using Financial.Core.Models;
using Financial.Core.ViewModels.SettingType;
using System.Web.Mvc;

namespace Financial.Tests.WebApplication.Controllers
{
    public class SettingTypeControllerTestsBase : ControllerTestsBase
    {
        public SettingTypeControllerTestsBase()
        {
            _controller = new SettingTypeController(_unitOfWork);
        }

        protected SettingTypeController _controller;
    }

    public static class SettingTypeObjectMother
    {
    }

    [TestClass()]
    public class SettingTypeControllerTests : SettingTypeControllerTestsBase
    {
        // *** GET - SUCCESS ***
        // Valid Returned Views 
        // Valid Returned View Models
        // Valid Returned Success Messages

        // *** GET - ERROR ***
        // Invalid Returned Error Messages
        // Invalid Input Values
        // Invalid Returned View Models
        // Invalid Retrieved Database Values 
        // Invalid Retrieved Database Objects

        // *** POST - SUCCESS ***
        // Valid Redirected Actions 
        // Valid Output Values
        // Valid Updated Database Values
        // Valid Returned Success Messages

        // *** POST - ERROR ***
        // Invalid Input Values
        // Invalid Database Values 
        // Invalid Database Objects
        // Invalid Output Values
        // Invalid Returned Error Messages



        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnRouteValues_Test()
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
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 10, Name = "Name 1", IsActive = true }, // count
                new SettingType() { Id = 11, Name = "Name 2", IsActive = false }}; // count
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new SettingTypeController(_unitOfWork);
            int expectedCount = 2;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
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
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataSettingTypes = new List<SettingType>(); // clear records
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new SettingTypeController(_unitOfWork);
            var vmexpected = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = 1;

            // Act
            var result = controller.Create(vmexpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataSettingTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmexpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataSettingTypes = new List<SettingType>(); // clear records
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new SettingTypeController(_unitOfWork);
            var vmCreate = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = 1;

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("CreateLinkedAssetTypes", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetTypeSettingType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(newId, routeResult.RouteValues["settingTypeId"], "Route settingTypeId");
            Assert.AreEqual("Record created", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new CreateViewModel();

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 10, Name = "Existing Name", IsActive = true } }; 
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            SettingTypeController controller = new SettingTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                Name = "Existing Name"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }



        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 2;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRecordFromDatabase_Test()
        {
            // Arrange
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 10, Name = "Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new SettingTypeController(_unitOfWork);
            int id = 10;

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual("Name", vmResult.Name, "Name");
            Assert.AreEqual(true, vmResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var controller = _controller;
            var vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "Updated Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataSettingTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "Updated Name",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual("Record updated", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new EditViewModel();

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 10, Name = "Update Name", IsActive = true },
                new SettingType() { Id = 11, Name = "Existing Name", IsActive = true }}; 
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            SettingTypeController controller = new SettingTypeController(_unitOfWork);
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 10,
                Name = "Existing Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }



        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 2;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DetailsViewModel), "View Model");
        }

        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnRecordFromDatabase_Test()
        {
            // Arrange
            var _dataSettingTypes = new List<SettingType>() {
                new SettingType() { Id = 10, Name = "Name", IsActive = true }};
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_dataSettingTypes);
            var controller = new SettingTypeController(_unitOfWork);
            int id = 10;

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual("Name", vmResult.Name, "Name");
            Assert.AreEqual(true, vmResult.IsActive, "IsActive");
        }

    }
}