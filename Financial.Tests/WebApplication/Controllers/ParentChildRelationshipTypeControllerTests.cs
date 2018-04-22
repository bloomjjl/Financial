using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Tests.Data.Fakes;
using Financial.Tests.Data.Repositories;
using Financial.Core.Models;
using Financial.Tests.Data;
using System.Web.Mvc;
using Financial.WebApplication.Models.ViewModels.ParentChildRelationshipType;

namespace Financial.Tests.WebApplication.Controllers
{
    public class ParentChildRelationshipTypeControllerTestsBase : ControllerTestsBase
    {
        public ParentChildRelationshipTypeControllerTestsBase()
        {
            _controller = new ParentChildRelationshipTypeController(_unitOfWork);
        }

        protected ParentChildRelationshipTypeController _controller;
    }

    [TestClass()]
    public class ParentChildRelationshipTypeControllerTests : ParentChildRelationshipTypeControllerTestsBase
    {
        /*
        [TestMethod()]
        public void Index_Child_WhenProvidedRelationshipTypeIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Index(relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "View Result");
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
        }

        [TestMethod()]
        public void Index_Child_WhenProvidedRelationshipTypeIdIsValidParent_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }, // count
                new ParentChildRelationshipType() { Id = 11, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 22, IsActive = false }}; // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child 1", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Child 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Index(relationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
        }

        [TestMethod()]
        public void Index_Child_WhenProvidedRelationshipTypeIdIsValidChild_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }, // count
                new ParentChildRelationshipType() { Id = 11, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 22, IsActive = false }}; // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child 1", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Child 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 21;
            int expectedCount = 1;

            // Act
            var result = controller.Index(relationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedRelationshipTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedRelationshipTypeIdIsValid_ReturnActiveRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // no linked values
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Relationship Type 1", IsActive = true }, // NOT counted: Supplied ID
                new RelationshipType() { Id = 21, Name = "Relationship Type 2", IsActive = true }, // count
                new RelationshipType() { Id = 22, Name = "Relationship Type 3", IsActive = false } }; // NOT active
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevel List count"); // Parent-Child, Child-Parent
            Assert.AreEqual(expectedCount, vmResult.LinkedRelationshipTypes.Count(), "RelationshipType List count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedRelationshipTypeIdIsValidParent_ReturnActiveRelationshipTypesNotLinkedFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 22, IsActive = true },
                new ParentChildRelationshipType() { Id = 11, ParentRelationshipTypeId = 21, ChildRelationshipTypeId = 22, IsActive = true },
                new ParentChildRelationshipType() { Id = 12, ParentRelationshipTypeId = 21, ChildRelationshipTypeId = 20, IsActive = false }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Relationship Type 1", IsActive = true }, // counted (link not active)
                new RelationshipType() { Id = 21, Name = "Relationship Type 2", IsActive = true }, // NOT counted: supplied Id
                new RelationshipType() { Id = 22, Name = "Relationship Type 3", IsActive = true }, // NOT counted: already linked
                new RelationshipType() { Id = 23, Name = "Relationship Type 4", IsActive = true }}; // counted (not linked)
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 21;
            int expectedCount = 2;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedRelationshipTypes.Count(), "RelationshipType List count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedRelationshipTypeIdIsValidChild_ReturnActiveRelationshipTypesNotLinkedFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = false },
                new ParentChildRelationshipType() { Id = 11, ParentRelationshipTypeId = 22, ChildRelationshipTypeId = 21, IsActive = true },
                new ParentChildRelationshipType() { Id = 12, ParentRelationshipTypeId = 22, ChildRelationshipTypeId = 20, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            IList<RelationshipType> _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Relationship Type 1", IsActive = true }, // counted (link not active)
                new RelationshipType() { Id = 21, Name = "Relationship Type 2", IsActive = true }, // NOT counted: supplied Id
                new RelationshipType() { Id = 22, Name = "Relationship Type 3", IsActive = true }, // NOT counted: already linked
                new RelationshipType() { Id = 23, Name = "Relationship Type 4", IsActive = true }}; // counted (not linked)
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 21;
            int expectedCount = 2;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedRelationshipTypes.Count(), "RelationshipType List count");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValidParentRelationshipType_UpdateDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // clear records
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent Relationship Type", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child Relationship Type", IsActive = true }}; 
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 20,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedLinkedRelationshipType = "21"
            };
            int expectedNewId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataParentChildRelationshipTypes.FirstOrDefault(r => r.Id == expectedNewId);
            Assert.AreEqual(vmExpected.SuppliedRelationshipTypeId, dbResult.ParentRelationshipTypeId, "Parent Id");
            Assert.AreEqual(vmExpected.SelectedLinkedRelationshipType, dbResult.ChildRelationshipTypeId.ToString(), "Child Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValidChildRelationshipType_UpdateDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // clear records
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent Relationship Type", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child Relationship Type", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 21,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "20"
            };
            int expectedNewId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataParentChildRelationshipTypes.FirstOrDefault(r => r.Id == expectedNewId);
            Assert.AreEqual(vmExpected.SelectedLinkedRelationshipType, dbResult.ParentRelationshipTypeId.ToString(), "Parent Id");
            Assert.AreEqual(vmExpected.SuppliedRelationshipTypeId, dbResult.ChildRelationshipTypeId, "Child Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // clear records
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent Relationship Type", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child Relationship Type", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 21,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "20"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.SuppliedRelationshipTypeId, routeResult.RouteValues["id"], "Route Id");
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
        public void Create_Post_WhenProvidedParentLinkIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Relationship Type 1", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Relationship Type 2", IsActive = true }}; 
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 20,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedLinkedRelationshipType = "21"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "RelationshipLevel DropDownList");
            Assert.IsNotNull(vmResult.LinkedRelationshipTypes, "RelationshipTypes DropDownList");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedChildLinkIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Relationship Type 1", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Relationship Type 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 21,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "20"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "RelationshipLevel DropDownList");
            Assert.IsNotNull(vmResult.LinkedRelationshipTypes, "RelationshipTypes DropDownList");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }



        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 2;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Edit(id, relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedRelationshipTypeIsValidParent_ReturnActiveRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true }, // NOT counted: supplied Id
                new RelationshipType() { Id = 21, Name = "Child Selected", IsActive = true }, // counted: Selected in list
                new RelationshipType() { Id = 22, Name = "Child 1", IsActive = false }, // NOT active
                new RelationshipType() { Id = 23, Name = "Child 2", IsActive = true }}; // counted
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int id = 10;
            int relationshipTypeId = 20;
            int expectedCount = 2;

            // Act
            var result = controller.Edit(id, relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual(relationshipTypeId, vmResult.RelationshipTypeId, "RelationshipType Id");
            Assert.AreEqual("Parent", vmResult.RelationshipTypeName, "RelationshipType Name");
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevel List"); // Parent-Child, Child-Parent
            Assert.AreEqual("Parent-Child", vmResult.SelectedRelationshipLevel, "Selected RelationshipLevel");
            Assert.AreEqual(expectedCount, vmResult.RelationshipTypes.Count(), "RelationshipType List count");
            Assert.AreEqual("21", vmResult.SelectedRelationshipType, "Selected RelationshipType");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedRelationshipTypeIsValidChild_ReturnActiveRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 21, ChildRelationshipTypeId = 20, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Child", IsActive = true }, // NOT counted: supplied Id
                new RelationshipType() { Id = 21, Name = "Parent Selected", IsActive = true }, // counted: Selected in list
                new RelationshipType() { Id = 22, Name = "Parent 1", IsActive = false }, // NOT active
                new RelationshipType() { Id = 23, Name = "Parent 2", IsActive = true }}; // counted
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int id = 10;
            int relationshipTypeId = 20;
            int expectedCount = 2;

            // Act
            var result = controller.Edit(id, relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual(relationshipTypeId, vmResult.RelationshipTypeId, "RelationshipType Name");
            Assert.AreEqual("Child", vmResult.RelationshipTypeName, "RelationshipType Name");
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevel List"); // Parent-Child, Child-Parent
            Assert.AreEqual("Child-Parent", vmResult.SelectedRelationshipLevel, "Selected RelationshipLevel");
            Assert.AreEqual(expectedCount, vmResult.RelationshipTypes.Count(), "RelationshipType List count");
            Assert.AreEqual("21", vmResult.SelectedRelationshipType, "Selected RelationshipType");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValidParent_UpdateDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child Original", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Child Updated", IsActive = true }}; 
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                RelationshipTypeId = 20,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "22" // updated selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataParentChildRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(vmExpected.RelationshipTypeId, dbResult.ParentRelationshipTypeId, "Parent RelationshipType Id");
            Assert.AreEqual(vmExpected.SelectedRelationshipType, dbResult.ChildRelationshipTypeId.ToString(), "Child RelationshipType Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValidChild_UpdateDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 21, ChildRelationshipTypeId = 20, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Child", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Parent Original", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Parent Updated", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                RelationshipTypeId = 20,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "22" // updated selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataParentChildRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(vmExpected.SelectedRelationshipType, dbResult.ParentRelationshipTypeId.ToString(), "Parent RelationshipType Id");
            Assert.AreEqual(vmExpected.RelationshipTypeId, dbResult.ChildRelationshipTypeId, "Child RelationshipType Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child Original", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Child Updated", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                RelationshipTypeId = 20,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "22" // updated selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.RelationshipTypeId, routeResult.RouteValues["Id"], "Route Id");
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
        public void Edit_Post_WhenProvidedRelationshipLinkIsDuplicatedParent_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }, // update
                new ParentChildRelationshipType() { Id = 11, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 22, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child 1", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Child 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                RelationshipTypeId = 20,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "22" // Duplicate selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "RelationshipLevel DropDownList");
            Assert.IsNotNull(vmResult.RelationshipTypes, "RelationshipTypes DropDownList");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedRelationshipLinkIsDuplicatedChild_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 21, ChildRelationshipTypeId = 20, IsActive = true }, 
                new ParentChildRelationshipType() { Id = 11, ParentRelationshipTypeId = 22, ChildRelationshipTypeId = 20, IsActive = true }}; // update
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Child", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Parent 1", IsActive = true },
                new RelationshipType() { Id = 22, Name = "Parent 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 11,
                RelationshipTypeId = 20,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "21" // Duplicate selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "RelationshipLevel DropDownList");
            Assert.IsNotNull(vmResult.RelationshipTypes, "RelationshipTypes DropDownList");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedRelationshipIsNotChangedParent_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                RelationshipTypeId = 20,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "21" // Not changed
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.RelationshipTypeId, routeResult.RouteValues["Id"], "Route Id");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedRelationshipIsNotChangedChild_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                RelationshipTypeId = 21,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "20" // Not changed
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(vmExpected.RelationshipTypeId, routeResult.RouteValues["Id"], "RelationshipType Id");
        }



        [TestMethod()]
        public void Delete_Get_WhenProvidedValuesAreValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 2;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Delete(id, relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Delete", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DeleteViewModel), "View Model");
        }

        [TestMethod()]
        public void Delete_Get_WhenProvidedValuesAreValidParent_ReturnValuesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 20;
            int id = 10;

            // Act
            var result = controller.Delete(id, relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DeleteViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual(relationshipTypeId, vmResult.RelationshipTypeId, "Supplied RelationshipType Id");
            Assert.AreEqual("Parent", vmResult.RelationshipTypeName, "Supplied RelationshipType Name");
            Assert.AreEqual("Parent", vmResult.ParentRelationshipTypeName, "Parent RelationshipType Name");
            Assert.AreEqual("Child", vmResult.ChildRelationshipTypeName, "Child RelationshipType Name");
        }

        [TestMethod()]
        public void Delete_Get_WhenProvidedValuesAreValidChild_ReturnValuesFromDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 21;
            int id = 10;

            // Act
            var result = controller.Delete(id, relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DeleteViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual(relationshipTypeId, vmResult.RelationshipTypeId, "Supplied RelationshipType Id");
            Assert.AreEqual("Child", vmResult.RelationshipTypeName, "Supplied RelationshipType Name");
            Assert.AreEqual("Parent", vmResult.ParentRelationshipTypeName, "Parent RelationshipType Name");
            Assert.AreEqual("Child", vmResult.ChildRelationshipTypeName, "Child RelationshipType Name");
        }

        [TestMethod()]
        public void Delete_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new DeleteViewModel()
            {
                Id = 10                
            };

            // Act
            var result = controller.Delete(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _dataParentChildRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.Id, dbResult.Id, "Id");
            Assert.AreEqual(false, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Delete_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 20, ChildRelationshipTypeId = 21, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 20, Name = "Parent", IsActive = true },
                new RelationshipType() { Id = 21, Name = "Child", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new DeleteViewModel()
            {
                Id = 10,
                RelationshipTypeId = 21
            };

            // Act
            var result = controller.Delete(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.RelationshipTypeId, routeResult.RouteValues["Id"], "Route Id");
        }

        [TestMethod()]
        public void Delete_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new DeleteViewModel();

            // Act
            var result = controller.Delete(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Delete", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DeleteViewModel), "View Model");
        }
        */


    }
}