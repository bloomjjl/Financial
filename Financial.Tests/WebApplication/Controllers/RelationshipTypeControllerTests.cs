using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using Financial.Tests.Data.Repositories;
using Financial.Tests.Data.Fakes;
using Financial.Tests.Data;
using System.Web.Mvc;
using Financial.Core.ViewModels.RelationshipType;

namespace Financial.Tests.WebApplication.Controllers
{
    public class RelationshipTypeControllerTestsBase : ControllerTestsBase
    {
        public RelationshipTypeControllerTestsBase()
        {
            _controller = new RelationshipTypeController(_unitOfWork);
        }

        protected RelationshipTypeController _controller;
    }

    [TestClass()]
    public class RelationshipTypeControllerTests : RelationshipTypeControllerTestsBase
    {
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
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 10, Name = "Name 1", IsActive = true }, // count
                new RelationshipType() { Id = 11, Name = "Name 2", IsActive = false }}; // count
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            RelationshipTypeController controller = new RelationshipTypeController(_unitOfWork);
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
            Assert.AreEqual("Test Message", viewResult.ViewData["SuccessMessage"], "Message");
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
            Assert.AreEqual("Test Message", viewResult.ViewData["ErrorMessage"], "Message");
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
        public void Create_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["SuccessMessage"] = "Test Message";

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.ViewData["SuccessMessage"], "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataRelationshipTypes = new List<RelationshipType>(); // clear records
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new RelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataRelationshipTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            var vmExpected = new CreateViewModel()
            {
                Name = "New Name"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Route Controller");
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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 10, Name = "Existing Name", IsActive = true } }; 
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new RelationshipTypeController(_unitOfWork);
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
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 10, Name = "Name", IsActive = true } }; 
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            RelationshipTypeController controller = new RelationshipTypeController(_unitOfWork);
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
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 10, Name = "Old Name", IsActive = true } };
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new RelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                Name = "Updated Name",
                IsActive = false
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
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
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Route Controller");
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
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 10, Name = "Update Name", IsActive = true },
                new RelationshipType() { Id = 11, Name = "Existing Name", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new RelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
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
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 10, Name = "Name", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new RelationshipTypeController(_unitOfWork);
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

        [TestMethod()]
        public void Details_Get_WhenProvidedErrorMessage_ReturnViewData_Test()
        {
            // Arrange
            var controller = _controller;
            controller.TempData["ErrorMessage"] = "Test Message";
            int id = 1;

            // Act
            var result = controller.Details(id);

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Test Message", viewResult.ViewData["ErrorMessage"].ToString(), "Message");
        }


    }
}