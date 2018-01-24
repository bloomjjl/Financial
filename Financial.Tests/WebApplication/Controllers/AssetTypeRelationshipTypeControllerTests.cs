using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using Financial.Core.ViewModels.AssetTypeRelationshipType;

namespace Financial.Tests.WebApplication.Controllers
{
    public class AssetTypeRelationshipTypeControllerTestsBase : ControllerTestsBase
    {
        public AssetTypeRelationshipTypeControllerTestsBase()
        {
            // Controller
            _controller = new AssetTypeRelationshipTypeController(_unitOfWork);
        }

        protected AssetTypeRelationshipTypeController _controller;
    }


    [TestClass()]
    public class AssetTypeRelationshipTypeControllerTests : AssetTypeRelationshipTypeControllerTestsBase
    {
        /*
        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedRelatinshipTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeRelationshipTypeController controller = _controller;
            int relationshipTypeId = 1;

            // Act
            var result = controller.IndexLinkedAssetTypes(relationshipTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_IndexLinkedAssetTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexLinkedAssetTypesViewModel>));
        }
        */

        /*
        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedParentChildRelationshipTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
                { Id = 1, ParentAssetTypeId = 3, ChildAssetTypeId = 4, ParentChildRelationshipTypeId = 6, ChildParentRelationshipTypeId = 7, IsActive = true }); // count
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
                { Id = 2, ParentAssetTypeId = 3, ChildAssetTypeId = 5, ParentChildRelationshipTypeId = 6, ChildParentRelationshipTypeId = 8, IsActive = false }); // NOT active
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Parent AssetType", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 4, Name = "Child AssetType 1", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 5, Name = "Child AssetType 2", IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 6, Name = "Parent-Child RelationshipType", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Child-Parent RelationshipType 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 8, Name = "Child-Parent RelationshipType 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 6;
            int expectedCount = 1;

            // Act
            var result = controller.IndexLinkedAssetTypes(relationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedAssetTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }
        */

        /*
        [TestMethod()]
        public void IndexLinkedAssetTypes_Child_WhenProvidedChildParentRelationshipTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
            { Id = 1, ParentAssetTypeId = 4, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 7, ChildParentRelationshipTypeId = 6, IsActive = true }); // count
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
            { Id = 2, ParentAssetTypeId = 5, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 8, ChildParentRelationshipTypeId = 6, IsActive = false }); // NOT active
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Child AssetType", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 4, Name = "Parent AssetType 1", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 5, Name = "Parent AssetType 2", IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 6, Name = "Child-Parent RelationshipType", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Parent-Child RelationshipType 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 8, Name = "Parent-Child RelationshipType 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int relationshipTypeId = 6;
            int expectedCount = 1;

            // Act
            var result = controller.IndexLinkedAssetTypes(relationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedAssetTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }
        */


        
        [TestMethod()]
        public void IndexLinkedRelationshipTypes_Child_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeRelationshipTypeController controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.IndexLinkedRelationshipTypes(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_IndexLinkedRelationshipTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexLinkedRelationshipTypesViewModel>));
        }

        [TestMethod()]
        public void IndexLinkedRelationshipTypes_Child_WhenProvidedParentAssetTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 1, ParentAssetTypeId = 3, ChildAssetTypeId = 4, ParentChildRelationshipTypeId = 6, IsActive = true }); // count
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 2, ParentAssetTypeId = 3, ChildAssetTypeId = 5, ParentChildRelationshipTypeId = 6, IsActive = false }); // NOT active
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Parent AssetType", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 4, Name = "Child AssetType 1", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 5, Name = "Child AssetType 2", IsActive = true });
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 6, ParentRelationshipTypeId = 7, ChildRelationshipTypeId = 8, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Parent-Child RelationshipType", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 8, Name = "Child-Parent RelationshipType", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 3;
            int expectedCount = 1;

            // Act
            var result = controller.IndexLinkedRelationshipTypes(assetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedRelationshipTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }
        
        [TestMethod()]
        public void IndexLinkedRelationshipTypes_Child_WhenProvidedChildAssetTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 1, ParentAssetTypeId = 4, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 6, IsActive = true }); // count
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 2, ParentAssetTypeId = 5, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 6, IsActive = false }); // NOT active
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Child AssetType", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 4, Name = "Parent AssetType 1", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 5, Name = "Parent AssetType 2", IsActive = true });
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 6, ParentRelationshipTypeId = 7, ChildRelationshipTypeId = 8, IsActive = true });
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Parent-Child RelationshipType", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 8, Name = "Child-Parent RelationshipType", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 3;
            int expectedCount = 1;

            // Act
            var result = controller.IndexLinkedRelationshipTypes(assetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexLinkedRelationshipTypesViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeRelationshipTypeController controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel));
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetTypeIdIsValid_ReturnSuppliedAssetValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "AssetType", IsActive = true });
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 1;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(assetTypeId, vmResult.SuppliedAssetTypeId, "Id");
            Assert.AreEqual("AssetType", vmResult.SuppliedAssetTypeName, "Name");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetTypeIdIsValid_ReturnRelationshipLevels_Test()
        {
            // Arrange
            AssetTypeRelationshipTypeController controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevels Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedParentAssetTypeIdIsValid_ReturnActiveLinkedRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // no linked records
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 4, IsActive = true }); // count
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 5, IsActive = false }); // NOT active
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Child 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.ParentRelationshipTypes.Count(), "Parent RelationshipTypes Count");
            Assert.AreEqual(expectedCount, vmResult.ChildRelationshipTypes.Count(), "Child RelationshipTypes Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedChildAssetTypeIdIsValid_ReturnActiveLinkedRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // no linked records
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 3, IsActive = true }); // count
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 3, IsActive = false }); // NOT active
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Parent 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 1;
            int expectedCount = 1;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.ParentRelationshipTypes.Count(), "Parent RelationshipTypes Count");
            Assert.AreEqual(expectedCount, vmResult.ChildRelationshipTypes.Count(), "Child RelationshipTypes Count");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedParentAssetTypeIdIsValid_ReturnActiveAssetTyepsNotLinkedFromDatabase_Test()
        {
            // Arrange
            IList<AssetType> _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 1, Name = "Supplied Parent AssetType", IsActive = true }); // NOT in list: supplied Id
            _assetTypes.Add(new AssetType() { Id = 2, Name = "Linked Child AssetType", IsActive = true }); // NOT in list: already linked
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Link Not Active Child AssetType", IsActive = true }); // in list
            _assetTypes.Add(new AssetType() { Id = 4, Name = "New Active Child AssetType", IsActive = false }); // NOT active
            _assetTypes.Add(new AssetType() { Id = 5, Name = "New Link Child AssetType", IsActive = true }); // in list
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 6, ParentAssetTypeId = 1, ChildAssetTypeId = 2, ParentChildRelationshipTypeId = 9, IsActive = true });
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 7, ParentAssetTypeId = 2, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 9, IsActive = true });
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 8, ParentAssetTypeId = 1, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 10, IsActive = false });
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 9, ParentRelationshipTypeId = 1, ChildRelationshipTypeId = 2, IsActive = true });
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 10, ParentRelationshipTypeId = 1, ChildRelationshipTypeId = 4, IsActive = true });
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 1;
            int expectedCount = 2;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(expectedCount, vmResult.LinkAssetTypes.Count(), "Link AssetTypes Count");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedParentViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear values
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 6,
                SelectedRelationshipLevel = "Parent",
                SelectedRelationshipType = "2",
                SelectedLinkAssetType = "5"
            };
            int newId = 1;

            // Act
            controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbAssetTypeRelationshipType = _assetTypesRelationshipTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, dbAssetTypeRelationshipType.ParentAssetTypeId, "Parent AssetType Id");
            Assert.AreEqual(vmExpected.SelectedLinkAssetType, dbAssetTypeRelationshipType.ChildAssetTypeId.ToString(), "Child AssetType Id");
            Assert.AreEqual(vmExpected.SelectedRelationshipType, dbAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), "ParentChildRelationshipType Id");
            Assert.IsTrue(dbAssetTypeRelationshipType.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedChildViewModelIsValid_UpdateDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear values
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 6,
                SelectedRelationshipLevel = "Child",
                SelectedRelationshipType = "2",
                SelectedLinkAssetType = "5"
            };
            int newId = 1;

            // Act
            controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbAssetTypeRelationshipType = _assetTypesRelationshipTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.SelectedLinkAssetType, dbAssetTypeRelationshipType.ParentAssetTypeId.ToString(), "Parent AssetType Id");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, dbAssetTypeRelationshipType.ChildAssetTypeId, "Child AssetType Id");
            Assert.AreEqual(vmExpected.SelectedRelationshipType, dbAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), "ParentChildRelationshipType Id");
            Assert.IsTrue(dbAssetTypeRelationshipType.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear values
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 6,
                SelectedRelationshipLevel = "Child",
                SelectedRelationshipType = "2",
                SelectedLinkAssetType = "5"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, routeResult.RouteValues["id"], "assetTypeId");
            Assert.IsNull(controller.TempData["SuccessMessage"], "Success Message");
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
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", routeResult.RouteValues["action"], "Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Controller");
            Assert.AreEqual("Encountered a problem. Try again.", controller.TempData["ErrorMessage"].ToString(), "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedLinkedRelationshipIsDuplicated_ReturnRouteValues_Test()
        {
            // Arrange
            var _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 1, ParentAssetTypeId = 6, ChildAssetTypeId = 5, ParentChildRelationshipTypeId = 2, IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 6,
                SelectedRelationshipLevel = "Parent",
                SelectedRelationshipType = "2",
                SelectedLinkAssetType = "5"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "Relationship Levels");
            Assert.IsNotNull(vmResult.ParentRelationshipTypes, "Parent Relationship Types");
            Assert.IsNotNull(vmResult.ChildRelationshipTypes, "Child Relationship Types");
            Assert.IsNotNull(vmResult.LinkAssetTypes, "Linked AssetTypes");
            Assert.AreEqual("Record already exists", controller.ViewData["ErrorMessage"], "Message");
        }


        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 1;
            int assetTypeId = 2;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValid_ReturnSuppliedAssetValuesFromDatabase_Test()
        {
            // Arrange
            var _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _assetTypesRelationshipTypes.Add(new AssetTypeRelationshipType() { Id = 1, ParentAssetTypeId = 2, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 5, IsActive = true });
            var _assetTypes = new List<AssetType>();
            _assetTypes.Add(new AssetType() { Id = 2, Name = "Parent AssetType", IsActive = true });
            _assetTypes.Add(new AssetType() { Id = 3, Name = "Child AssetType", IsActive = true });

            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_assetTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int id = 1;
            int assetTypeId = 2;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(assetTypeId, vmResult.SuppliedAssetTypeId, "Id");
            Assert.AreEqual("Parent AssetType", vmResult.SuppliedAssetTypeName, "Name");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValid_ReturnRelationshipLevels_Test()
        {
            // Arrange
            var controller = _controller;
            int id = 1;
            int assetTypeId = 1;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevels Count"); // parent / child
        }



    }
}