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
    public class RelationshipTypeControllerTestsBase
    {
        public RelationshipTypeControllerTestsBase()
        {
            // Fake Data
            _relationshipTypes = FakeRelationshipTypes.InitialFakeRelationshipTypes().ToList();
            _assetTypesRelationshipTypes = FakeAssetTypesRelationshipTypes.InitialFakeAssetTypesRelationshipTypes().ToList();
            // Fake Repositories
            _repositoryRelationshipType = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            _repositoryAssetTypeRelationshipType = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            // Fake Unit of Work
            _unitOfWork.RelationshipTypes = _repositoryRelationshipType;
            _unitOfWork.AssetTypesRelationshipTypes = _repositoryAssetTypeRelationshipType;
            // Controller
            _controller = new RelationshipTypeController(_unitOfWork);
        }

        protected IList<RelationshipType> _relationshipTypes;
        protected IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes;
        protected InMemoryRelationshipTypeRepository _repositoryRelationshipType;
        protected InMemoryAssetTypeRelationshipTypeRepository _repositoryAssetTypeRelationshipType;
        protected InMemoryUnitOfWork _unitOfWork = new InMemoryUnitOfWork();
        protected RelationshipTypeController _controller;
    }

    [TestClass()]
    public class RelationshipTypeControllerTests : RelationshipTypeControllerTestsBase
    {
        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnRouteValues_Test()
        {
            // Arrange
            RelationshipTypeController controller = _controller;

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
        }

        [TestMethod()]
        public void Index_Get_WhenProvidedNoInputValues_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 1, Name = "Name 1", IsActive = true }); // count
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Name 2", IsActive = false }); // count
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
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
            RelationshipTypeController controller = _controller;
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
            RelationshipTypeController controller = _controller;
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
            RelationshipTypeController controller = _controller;

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
            RelationshipTypeController controller = _controller;
            CreateViewModel vmexpected = new CreateViewModel()
            {
                Name = "New Name"
            };
            int newId = _relationshipTypes.Count() + 1;

            // Act
            var result = controller.Create(vmexpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _relationshipTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmexpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            RelationshipTypeController controller = _controller;
            var vmCreate = new CreateViewModel()
            {
                Name = "New Name"
            };

            // Act
            var result = controller.Create(vmCreate);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Record created", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            RelationshipTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new CreateViewModel()
            {
                Name = "Existing Name"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Encountered a problem. Try again.", controller.TempData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 1, Name = "Existing Name", IsActive = true }); // duplicated name
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            RelationshipTypeController controller = new RelationshipTypeController(_unitOfWork);
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
            RelationshipTypeController controller = _controller;
            int id = 2;

            // Act
            var result = controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedIdIsValid_ReturnRecordFromDatabase_Test()
        {
            // Arrange
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 1, Name = "Name", IsActive = true }); // return values
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            RelationshipTypeController controller = new RelationshipTypeController(_unitOfWork);
            int id = 1;

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
            RelationshipTypeController controller = _controller;
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
            var dbResult = _relationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(vmExpected.Name, dbResult.Name, "Name");
            Assert.AreEqual(vmExpected.IsActive, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            RelationshipTypeController controller = _controller;
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
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"]);
            Assert.AreEqual("Record updated", controller.TempData["SuccessMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            RelationshipTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            EditViewModel vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "Existing Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Encountered a problem. Try again.", controller.TempData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedNameIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 1, Name = "Name", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Existing Name", IsActive = true }); // duplicated name
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            RelationshipTypeController controller = new RelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                Name = "Existing Name",
                IsActive = true
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }



        [TestMethod()]
        public void Details_Get_WhenProvidedIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            RelationshipTypeController controller = _controller;
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
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 1, Name = "Name", IsActive = true });
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            RelationshipTypeController controller = new RelationshipTypeController(_unitOfWork);
            int id = 1;

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