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
using Financial.Core.ViewModels.ParentChildRelationshipType;

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
        [TestMethod()]
        public void Index_Child_WhenProvidedRelationshipTypeIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Index(relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_Index", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>));
        }

        [TestMethod()]
        public void Index_Child_WhenProvidedParentRelationshipTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 4, IsActive = true }); // count
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 5, IsActive = false }); // NOT active
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Child 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 1;
            int relationshipTypeId = 3;

            // Act
            var result = controller.Index(relationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
            Assert.AreEqual(1, vmReturned[0].Id, "Id");
            Assert.AreEqual("Parent", vmReturned[0].ParentRelationshipTypeName, "Parent Name");
            Assert.AreEqual("Child 1", vmReturned[0].ChildRelationshipTypeName, "Child Name");
        }

        [TestMethod()]
        public void Index_Child_WhenProvidedChildRelationshipTypeIdIsValid_ReturnAllValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 3, IsActive = true }); // count
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 3, IsActive = false }); // NOT active
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Parent 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 1;
            int relationshipTypeId = 3;

            // Act
            var result = controller.Index(relationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmReturned = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmReturned.Count(), "Number of records");
            Assert.AreEqual(1, vmReturned[0].Id, "Id");
            Assert.AreEqual("Parent 1", vmReturned[0].ParentRelationshipTypeName, "Parent Name");
            Assert.AreEqual("Child", vmReturned[0].ChildRelationshipTypeName, "Child Name");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedRelationshipTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedRelationshipTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // no linked values
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 1, Name = "Relationship Type 1", IsActive = true }); // NOT counted: Supplied ID
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Relationship Type 2", IsActive = true }); // count
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Relationship Type 3", IsActive = false }); // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 1;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(1, vmResult.SuppliedRelationshipTypeId, "Id");
            Assert.AreEqual("Relationship Type 1", vmResult.SuppliedRelationshipTypeName, "Name");
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevel List"); // Parent-Child, Child-Parent
            Assert.AreEqual(expectedCount, vmResult.LinkedRelationshipTypes.Count(), "RelationshipType List count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedParentRelationshipTypeIdIsValid_ReturnActiveRelationshipTypesNotLinkedFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 6, IsActive = true }); 
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 6, IsActive = true }); 
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 3, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 4, IsActive = false }); 
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Relationship Type 1", IsActive = true }); // counted (link not active)
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Relationship Type 2", IsActive = true }); // NOT counted: supplied Id
            _relationshipTypes.Add(new RelationshipType() { Id = 6, Name = "Relationship Type 3", IsActive = true }); // NOT counted: already linked
            _relationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Relationship Type 4", IsActive = true }); // counted (not linked)
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 2;
            int relationshipTypeId = 5;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedRelationshipTypes.Count(), "RelationshipType List count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedChildRelationshipTypeIdIsValid_ReturnActiveRelationshipTypesNotLinkedFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 5, IsActive = false });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 6, ChildRelationshipTypeId = 5, IsActive = true });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 3, ParentRelationshipTypeId = 6, ChildRelationshipTypeId = 4, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Relationship Type 1", IsActive = true }); // counted (link not active)
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Relationship Type 2", IsActive = true }); // NOT counted: supplied Id
            _relationshipTypes.Add(new RelationshipType() { Id = 6, Name = "Relationship Type 3", IsActive = true }); // NOT counted: already linked
            _relationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Relationship Type 4", IsActive = true }); // counted (not linked)
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 2;
            int relationshipTypeId = 5;

            // Act
            var result = controller.Create(relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkedRelationshipTypes.Count(), "RelationshipType List count");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedParentChildRelationshipViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // clear records
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent Relationship Type", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child Relationship Type", IsActive = true }); 
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 2,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedLinkedRelationshipType = "3"
            };
            int expectedNewId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _parentChildRelationshipTypes.FirstOrDefault(r => r.Id == expectedNewId);
            Assert.AreEqual(2, dbResult.ParentRelationshipTypeId, "Parent Id");
            Assert.AreEqual(3, dbResult.ChildRelationshipTypeId, "Child Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedChildParentRelationshipViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent Relationship Type", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child Relationship Type", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 3,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "2"
            };
            int expectedNewId = 1;

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _parentChildRelationshipTypes.FirstOrDefault(r => r.Id == expectedNewId);
            Assert.AreEqual(2, dbResult.ParentRelationshipTypeId, "Parent Id");
            Assert.AreEqual(3, dbResult.ChildRelationshipTypeId, "Child Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _parentChildRelationshipTypes = new List<ParentChildRelationshipType>(); // clear records
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            var controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 3,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "2"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(vmExpected.SuppliedRelationshipTypeId, routeResult.RouteValues["id"], "Id");
        }

        [TestMethod()]
        public void Create_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 3,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "2"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Encountered a problem. Try again.", controller.TempData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedParentLinkIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Relationship Type 1", IsActive = true }); 
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Relationship Type 2", IsActive = true }); 
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 2,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedLinkedRelationshipType = "3"
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
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Relationship Type 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Relationship Type 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedRelationshipTypeId = 3,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedLinkedRelationshipType = "2"
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
            ParentChildRelationshipTypeController controller = _controller;
            int id = 2;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Edit(id, relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedParentRelationshipTypeValuesAreValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent", IsActive = true }); // NOT counted: supplied Id
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child Selected", IsActive = true }); // counted: Selected in list
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child 2", IsActive = true }); // counted
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 2;
            int id = 1;
            int relationshipTypeId = 2;

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
            Assert.AreEqual("3", vmResult.SelectedRelationshipType, "Selected RelationshipType");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedChildRelationshipTypeValuesAreValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 2, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Child", IsActive = true }); // NOT counted: supplied Id
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent Selected", IsActive = true }); // counted: Selected in list
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent 2", IsActive = true }); // counted
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int expectedCount = 2;
            int id = 1;
            int relationshipTypeId = 2;

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
            Assert.AreEqual("3", vmResult.SelectedRelationshipType, "Selected RelationshipType");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedParentChildViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent", IsActive = true }); 
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child Original", IsActive = true }); 
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child Updated", IsActive = true }); 
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 2,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "4" // updated selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _parentChildRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(1, dbResult.Id, "Id");
            Assert.AreEqual(2, dbResult.ParentRelationshipTypeId, "Parent RelationshipType Id");
            Assert.AreEqual(4, dbResult.ChildRelationshipTypeId, "Child RelationshipType Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedChildParentViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 2, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Child", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent Original", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent Updated", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 2,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "4" // updated selection
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _parentChildRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(1, dbResult.Id, "Id");
            Assert.AreEqual(4, dbResult.ParentRelationshipTypeId, "Parent RelationshipType Id");
            Assert.AreEqual(2, dbResult.ChildRelationshipTypeId, "Child RelationshipType Id");
            Assert.AreEqual(true, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 2,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "3" 
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(2, routeResult.RouteValues["Id"], "RelationshipType Id");
        }

        [TestMethod()]
        public void Edit_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 2,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "3"
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
        public void Edit_Post_WhenProvidedParentChildRelationshipIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 4, IsActive = true });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 5, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Child 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 3,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "5" // Duplicate selection
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
        public void Edit_Post_WhenProvidedChildParentRelationshipIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 3, IsActive = true });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Parent 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 2,
                RelationshipTypeId = 3,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "4" // Duplicate selection
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
        public void Edit_Post_WhenProvidedParentChildRelationshipIsNotChanged_ReturnRouteValues_Test()
        {
            // Arrange
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 4, IsActive = true });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 5, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Child 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 3,
                SelectedRelationshipLevel = "Parent-Child",
                SelectedRelationshipType = "4" // Not changed
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(3, routeResult.RouteValues["Id"], "RelationshipType Id");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedChildParentRelationshipIsNotChanged_ReturnRouteValues_Test()
        {
            // Arrange
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 3, IsActive = true });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Parent 2", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 1,
                RelationshipTypeId = 3,
                SelectedRelationshipLevel = "Child-Parent",
                SelectedRelationshipType = "4" // Not changed
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(3, routeResult.RouteValues["Id"], "RelationshipType Id");
        }



        [TestMethod()]
        public void Delete_Get_WhenProvidedValuesAreValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            int id = 2;
            int relationshipTypeId = 1;

            // Act
            var result = controller.Delete(id, relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Delete", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(DeleteViewModel), "View Model");
        }

        [TestMethod()]
        public void Delete_Get_WhenProvidedParentChildRelationshipTypeValuesAreValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int id = 1;
            int relationshipTypeId = 2;

            // Act
            var result = controller.Delete(id, relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DeleteViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual(relationshipTypeId, vmResult.RelationshipTypeId, "RelationshipType Id");
            Assert.AreEqual("Parent", vmResult.RelationshipTypeName, "RelationshipType Name");
            Assert.AreEqual("Parent", vmResult.ParentRelationshipTypeName, "Parent RelationshipType Name");
            Assert.AreEqual("Child", vmResult.ChildRelationshipTypeName, "Child RelationshipType Name");
        }

        [TestMethod()]
        public void Delete_Get_WhenProvidedChildParentRelationshipTypeValuesAreValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            int id = 1;
            int relationshipTypeId = 3;

            // Act
            var result = controller.Delete(id, relationshipTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as DeleteViewModel;
            Assert.AreEqual(id, vmResult.Id, "Id");
            Assert.AreEqual(relationshipTypeId, vmResult.RelationshipTypeId, "RelationshipType Id");
            Assert.AreEqual("Child", vmResult.RelationshipTypeName, "RelationshipType Name");
            Assert.AreEqual("Parent", vmResult.ParentRelationshipTypeName, "Parent RelationshipType Name");
            Assert.AreEqual("Child", vmResult.ChildRelationshipTypeName, "Child RelationshipType Name");
        }

        [TestMethod()]
        public void Delete_Post_WhenProvidedViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 2, ChildRelationshipTypeId = 3, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 2, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            ParentChildRelationshipTypeController controller = new ParentChildRelationshipTypeController(_unitOfWork);
            var vmExpected = new DeleteViewModel()
            {
                Id = 1
            };

            // Act
            var result = controller.Delete(vmExpected);

            // Assert
            Assert.AreEqual(true, _unitOfWork.Committed, "Transaction Committed");
            var dbResult = _parentChildRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(1, dbResult.Id, "Id");
            Assert.AreEqual(2, dbResult.ParentRelationshipTypeId, "Parent RelationshipType Id");
            Assert.AreEqual(3, dbResult.ChildRelationshipTypeId, "Child RelationshipType Id");
            Assert.AreEqual(false, dbResult.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Delete_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            var vmExpected = new DeleteViewModel()
            {
                Id = 1,
                RelationshipTypeId = 2
            };

            // Act
            var result = controller.Delete(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(2, routeResult.RouteValues["Id"], "RelationshipType Id");
        }

        [TestMethod()]
        public void Delete_Post_WhenModelStateNotValid_ReturnRouteValues_Test()
        {
            // Arrange
            ParentChildRelationshipTypeController controller = _controller;
            controller.ModelState.AddModelError("", "mock error message");
            var vmExpected = new DeleteViewModel()
            {
                Id = 1,
                RelationshipTypeId = 2
            };

            // Act
            var result = controller.Delete(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("RelationshipType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Encountered a problem. Try again.", controller.TempData["ErrorMessage"].ToString(), "Message");
        }

    }
}