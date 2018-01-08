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
    public class SettingTypeControllerTestsBase
    {
        public SettingTypeControllerTestsBase()
        {
            _settingTypes = FakeSettingTypes.InitialFakeSettingTypes().ToList();
            _repositorySettingType = new InMemorySettingTypeRepository(_settingTypes);
            _unitOfWork = new InMemoryUnitOfWork();
            _unitOfWork.SettingTypes = _repositorySettingType;
            _controller = new SettingTypeController(_unitOfWork);
        }

        protected IList<SettingType> _settingTypes;
        protected InMemorySettingTypeRepository _repositorySettingType;
        protected InMemoryUnitOfWork _unitOfWork;
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
            SettingTypeController controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
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
            SettingTypeController controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Message";

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 1, Name = "Name 1", IsActive = true }); // count
            _settingTypes.Add(new SettingType() { Id = 2, Name = "Name 2", IsActive = false }); // count
            _settingTypes.Add(new SettingType() { Id = 3, Name = "Name 3", IsActive = true }); // count
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            SettingTypeController controller = new SettingTypeController(_unitOfWork);
            int expectedCount = 3;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedNoInputVaues_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            CreateViewModel vmexpected = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = _settingTypes.Count() + 1;

            // Act
            var result = controller.Create(vmexpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _settingTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmexpected.Name, dbResult.Name, "SettingType Name");
            Assert.AreEqual(true, dbResult.IsActive, "SettingType IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            CreateViewModel vmCreate = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = _settingTypes.Count() + 1;

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("CreateLinkedAssetTypes", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetTypeSettingType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(newId, routeResult.RouteValues["settingTypeId"], "settingTypeId");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnSuccessMessage_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            CreateViewModel vmExpected = new CreateViewModel()
            {
                Name = "New Name"
            };

            // Act
            controller.Create(vmExpected);

            // Assert
            Assert.AreEqual("Setting Type Created", controller.TempData["SuccessMessage"].ToString(), "Success Message");
        }



        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            int id = 2;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRecordFromDatabase_Test()
        {
            // Arrange
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 1, Name = "Name 1", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 2, Name = "Name 2", IsActive = true }); // return values
            _settingTypes.Add(new SettingType() { Id = 3, Name = "Name 3", IsActive = true });
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            SettingTypeController controller = new SettingTypeController(_unitOfWork);
            int id = 2;

            // Act
            var result = controller.Edit(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual("Name 2", vmResult.Name, "Name");
            Assert.AreEqual(true, vmResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 2,
                Name = "Updated Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _settingTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "Updated Name",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"]);
            Assert.AreEqual("SettingType", routeResult.RouteValues["controller"]);
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnSuccessMessage_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "Updated Name",
                IsActive = true
            };

            // Act
            controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual("Record updated.", controller.TempData["SuccessMessage"].ToString(), "Success Message");
        }



        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            SettingTypeController controller = _controller;
            int id = 2;

            // Act
            var result = controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Details", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DetailsViewModel));
        }

        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnRecordFromDatabase_Test()
        {
            // Arrange
            IList<SettingType> _settingTypes = new List<SettingType>();
            _settingTypes.Add(new SettingType() { Id = 1, Name = "Name 1", IsActive = true });
            _settingTypes.Add(new SettingType() { Id = 2, Name = "Name 2", IsActive = true }); // return values
            _settingTypes.Add(new SettingType() { Id = 3, Name = "Name 3", IsActive = true });
            _unitOfWork.SettingTypes = new InMemorySettingTypeRepository(_settingTypes);
            SettingTypeController controller = new SettingTypeController(_unitOfWork);
            int id = 2;

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DetailsViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual("Name 2", vmResult.Name, "Name");
            Assert.AreEqual(true, vmResult.IsActive, "IsActive");
        }

    }
}