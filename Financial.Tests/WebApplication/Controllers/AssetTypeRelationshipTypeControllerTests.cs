﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            IList<AssetTypeRelationshipType> _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _dataAssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
                { Id = 1, ParentAssetTypeId = 3, ChildAssetTypeId = 4, ParentChildRelationshipTypeId = 6, ChildParentRelationshipTypeId = 7, IsActive = true }); // count
            _dataAssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
                { Id = 2, ParentAssetTypeId = 3, ChildAssetTypeId = 5, ParentChildRelationshipTypeId = 6, ChildParentRelationshipTypeId = 8, IsActive = false }); // NOT active
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 3, Name = "Parent AssetType", IsActive = true });
            _dataAssetTypes.Add(new AssetType() { Id = 4, Name = "Child AssetType 1", IsActive = true });
            _dataAssetTypes.Add(new AssetType() { Id = 5, Name = "Child AssetType 2", IsActive = true });
            IList<RelationshipType> _dataRelationshipTypes = new List<RelationshipType>();
            _dataRelationshipTypes.Add(new RelationshipType() { Id = 6, Name = "Parent-Child RelationshipType", IsActive = true });
            _dataRelationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Child-Parent RelationshipType 1", IsActive = true });
            _dataRelationshipTypes.Add(new RelationshipType() { Id = 8, Name = "Child-Parent RelationshipType 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
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
            IList<AssetTypeRelationshipType> _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>();
            _dataAssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
            { Id = 1, ParentAssetTypeId = 4, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 7, ChildParentRelationshipTypeId = 6, IsActive = true }); // count
            _dataAssetTypesRelationshipTypes.Add(new AssetTypeRelationshipType()
            { Id = 2, ParentAssetTypeId = 5, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 8, ChildParentRelationshipTypeId = 6, IsActive = false }); // NOT active
            IList<AssetType> _dataAssetTypes = new List<AssetType>();
            _dataAssetTypes.Add(new AssetType() { Id = 3, Name = "Child AssetType", IsActive = true });
            _dataAssetTypes.Add(new AssetType() { Id = 4, Name = "Parent AssetType 1", IsActive = true });
            _dataAssetTypes.Add(new AssetType() { Id = 5, Name = "Parent AssetType 2", IsActive = true });
            IList<RelationshipType> _dataRelationshipTypes = new List<RelationshipType>();
            _dataRelationshipTypes.Add(new RelationshipType() { Id = 6, Name = "Child-Parent RelationshipType", IsActive = true });
            _dataRelationshipTypes.Add(new RelationshipType() { Id = 7, Name = "Parent-Child RelationshipType 1", IsActive = true });
            _dataRelationshipTypes.Add(new RelationshipType() { Id = 8, Name = "Parent-Child RelationshipType 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
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
        public void Index_Child_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var controller = _controller;
            int assetTypeId = 1;

            // Act
            var result = controller.Index(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult), "View Result");
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_Index", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<IndexViewModel>), "View Model");
        }

        [TestMethod()]
        public void Index_Child_WhenProvidedAssetTypeIdIsValidParent_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }, // count
                new AssetTypeRelationshipType() { Id = 11, ParentAssetTypeId = 20, ChildAssetTypeId = 22, ParentChildRelationshipTypeId = 30, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType 1", IsActive = true },
                new AssetType() { Id = 22, Name = "Child AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Index(assetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }
        
        [TestMethod()]
        public void Index_Child_WhenProvidedChildAssetTypeIdIsValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 21, ChildAssetTypeId = 20, ParentChildRelationshipTypeId = 30, IsActive = true }, // count
                new AssetTypeRelationshipType() { Id = 11, ParentAssetTypeId = 22, ChildAssetTypeId = 20, ParentChildRelationshipTypeId = 30, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Child AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Parent AssetType 1", IsActive = true },
                new AssetType() { Id = 22, Name = "Parent AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 20;
            int expectedCount = 1;

            // Act
            var result = controller.Index(assetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as List<IndexViewModel>;
            Assert.AreEqual(expectedCount, vmResult.Count(), "ViewModel Count");
        }



        [TestMethod()]
        public void Create_Get_WhenProvidedAssetTypeIdIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 20;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Create", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateViewModel), "View Model");
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedAssetTypeIdIsValid_ReturnSuppliedAssetValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 20;

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
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 20;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as CreateViewModel;
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevels Count"); // Parent & Child
        }

        [TestMethod()]
        public void Create_Get_WhenProvidedSuccessMessage_ReturnViewData_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            controller.TempData["SuccessMessage"] = "Test Success Message";
            int assetTypeId = 20;

            // Act
            var result = controller.Create(assetTypeId);

            // Assert
            Assert.IsNotNull(controller.ViewData["SuccessMessage"], "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValidParent_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }}; 
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipType = "30",
                SelectedLinkAssetType = "5"
            };
            int newId = 1;

            // Act
            controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbAssetTypeRelationshipType = _dataAssetTypesRelationshipTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, dbAssetTypeRelationshipType.ParentAssetTypeId, "Parent AssetType Id");
            Assert.AreEqual(vmExpected.SelectedLinkAssetType, dbAssetTypeRelationshipType.ChildAssetTypeId.ToString(), "Child AssetType Id");
            Assert.AreEqual(vmExpected.SelectedParentChildRelationshipType, dbAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), "ParentChildRelationshipType Id");
            Assert.IsTrue(dbAssetTypeRelationshipType.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValidChild_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 21,
                SelectedRelationshipLevel = "Child",
                SelectedParentChildRelationshipType = "30",
                SelectedLinkAssetType = "20"
            };
            int newId = 1;

            // Act
            controller.Create(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbAssetTypeRelationshipType = _dataAssetTypesRelationshipTypes.FirstOrDefault(r => r.Id == newId);
            Assert.AreEqual(vmExpected.SelectedLinkAssetType, dbAssetTypeRelationshipType.ParentAssetTypeId.ToString(), "Parent AssetType Id");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, dbAssetTypeRelationshipType.ChildAssetTypeId, "Child AssetType Id");
            Assert.AreEqual(vmExpected.SelectedParentChildRelationshipType, dbAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), "ParentChildRelationshipType Id");
            Assert.IsTrue(dbAssetTypeRelationshipType.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipType = "30",
                SelectedLinkAssetType = "5"
            };

            // Act
            var result = controller.Create(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Parent-Child link created.", controller.TempData["SuccessMessage"], "Message");
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
        public void Create_Post_WhenProvidedLinkedRelationshipIsDuplicatedParent_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }}; // duplicated
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipType = "30",
                SelectedLinkAssetType = "21"
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
            Assert.IsNotNull(vmResult.SelectedParentChildRelationshipType, "Selected ParentChildRelationshipTypes");
            Assert.IsNotNull(vmResult.SelectedLinkAssetType, "Selected LinkedAssetType");
            Assert.AreEqual("Link already exists", controller.ViewData["ErrorMessage"], "Message");
        }

        [TestMethod()]
        public void Create_Post_WhenProvidedLinkedRelationshipIsDuplicatedChild_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }}; // duplicated
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new CreateViewModel()
            {
                SuppliedAssetTypeId = 21,
                SelectedRelationshipLevel = "Child",
                SelectedParentChildRelationshipType = "30",
                SelectedLinkAssetType = "20"
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
            Assert.IsNotNull(vmResult.SelectedParentChildRelationshipType, "Selected ParentChildRelationshipTypes");
            Assert.IsNotNull(vmResult.SelectedLinkAssetType, "Selected LinkedAssetType");
            Assert.AreEqual("Link already exists", controller.ViewData["ErrorMessage"], "Message");
        }



        [TestMethod()]
        public void DisplayParentChildRelationshipTypes_Get_WhenProvidedAssetTypeIdIsValidParent_ReturnActiveLinkedParentChildRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() { 
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType 1", IsActive = true },
                new AssetType() { Id = 22, Name = "Child AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }, // count
                new ParentChildRelationshipType() { Id = 31, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 42, IsActive = false }}; // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType 1", IsActive = true },
                new RelationshipType() { Id = 42, Name = "Child-Parent RelationshipType 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int suppliedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Parent";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayParentChildRelationshipTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayParentChildRelationshipTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.ParentChildRelationshipTypes.Count(), "ParentChildRelationshipTypes Count");
            Assert.IsNull(vmResult.ParentChildRelationshipTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedParentChildRelationshipTypeId, "Selected ParentChildRelationsihpType Id");
        }

        [TestMethod()]
        public void DisplayParentChildRelationshipTypes_Get_WhenProvidedAssetTypeIdIsValidChild_ReturnActiveLinkedParentChildRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(){
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 21, ChildAssetTypeId = 20, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Child AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Parent AssetType 1", IsActive = true },
                new AssetType() { Id = 22, Name = "Parent AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 41, ChildRelationshipTypeId = 40, IsActive = true }, // count
                new ParentChildRelationshipType() { Id = 31, ParentRelationshipTypeId = 42, ChildRelationshipTypeId = 40, IsActive = false }}; // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Child-Parent RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Parent-Child RelationshipType 1", IsActive = true },
                new RelationshipType() { Id = 42, Name = "Parent-Child RelationshipType 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int suppliedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Child";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayParentChildRelationshipTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayParentChildRelationshipTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.ParentChildRelationshipTypes.Count(), "ParentChildRelationshipTypes Count");
            Assert.IsNull(vmResult.ParentChildRelationshipTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedParentChildRelationshipTypeId, "Selected ParentChildRelationsihpType Id");
        }

        [TestMethod()]
        public void DisplayParentChildRelationshipTypes_Get_WhenProvidedAssetTypeIdIsValidParent_ReturnSelectedLinkedParentChildRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType 1", IsActive = true },
                new AssetType() { Id = 22, Name = "Child AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }, // count
                new ParentChildRelationshipType() { Id = 31, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 42, IsActive = false }}; // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType 1", IsActive = true },
                new RelationshipType() { Id = 42, Name = "Child-Parent RelationshipType 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int suppliedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Parent";
            int selectedParentChildRelationshipTypeId = 30;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayParentChildRelationshipTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayParentChildRelationshipTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.ParentChildRelationshipTypes.Count(), "ParentChildRelationshipTypes Count");
            Assert.IsNotNull(vmResult.ParentChildRelationshipTypes.FirstOrDefault(r => r.Selected), "Value selected in list");
            Assert.AreEqual(selectedParentChildRelationshipTypeId.ToString(), vmResult.SelectedParentChildRelationshipTypeId, "Selected ParentChildRelationsihpType Id");
        }

        [TestMethod()]
        public void DisplayParentChildRelationshipTypes_Get_WhenProvidedAssetTypeIdIsValidChild_ReturnSelectedLinkedParentChildRelationshipTypesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(){
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 21, ChildAssetTypeId = 20, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Child AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Parent AssetType 1", IsActive = true },
                new AssetType() { Id = 22, Name = "Parent AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 41, ChildRelationshipTypeId = 40, IsActive = true }, // count
                new ParentChildRelationshipType() { Id = 31, ParentRelationshipTypeId = 42, ChildRelationshipTypeId = 40, IsActive = false }}; // NOT active
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Child-Parent RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Parent-Child RelationshipType 1", IsActive = true },
                new RelationshipType() { Id = 42, Name = "Parent-Child RelationshipType 2", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int suppliedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Child";
            int selectedParentChildRelationshipTypeId = 30;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayParentChildRelationshipTypes(suppliedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayParentChildRelationshipTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.ParentChildRelationshipTypes.Count(), "ParentChildRelationshipTypes Count");
            Assert.IsNotNull(vmResult.ParentChildRelationshipTypes.FirstOrDefault(r => r.Selected), "Value selected in list");
            Assert.AreEqual(selectedParentChildRelationshipTypeId.ToString(), vmResult.SelectedParentChildRelationshipTypeId, "Selected ParentChildRelationsihpType Id");
        }



        // list all Asset Types in list that are active
        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidParent_ReturnActiveAssetTyepsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true }, 
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = false }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Parent";
            string selectedParentChildRelationshipTypeId = "30";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "LinkAssetTypes Count");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedAssetTypeId, "Selected LinkAssetType Id");
        }

        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidChild_ReturnActiveAssetTyepsFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear links
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = false }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 21;
            string selectedRelationshipLevelId = "Child";
            string selectedParentChildRelationshipTypeId = "30";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 1;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "Link AssetTypes Count");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedAssetTypeId, "Selected LinkAssetType Id");
        }

        // remove asset types already linked to supplied asset type that are active
        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidParent_ReturnAssetTypesNotLinkedFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }}; 
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Supplied Parent AssetType", IsActive = true }, // count: can be linked to self
                new AssetType() { Id = 21, Name = "Linked Child AssetType", IsActive = true }, // EXISTING link
                new AssetType() { Id = 22, Name = "NO Link Child AssetType", IsActive = true }}; // count: no link
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Parent";
            string selectedParentChildRelationshipTypeId = "30";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 2;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "LinkAssetTypes Count");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedAssetTypeId, "Selected LinkAssetType Id");
        }

        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidChild_ReturnAssetTypesNotLinkedFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 21, ChildAssetTypeId = 20, ParentChildRelationshipTypeId = 30, IsActive = true }}; 
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Supplied Child AssetType", IsActive = true }, // count: can be linked to self
                new AssetType() { Id = 21, Name = "Linked Parent AssetType", IsActive = true }, // EXISTING linked
                new AssetType() { Id = 22, Name = "NOT Linked Parent AssetType", IsActive = true }}; // count: no link
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Child";
            string selectedParentChildRelationshipTypeId = "30";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 2;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "Link AssetTypes Count");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedAssetTypeId, "Selected LinkAssetType Id");
        }

        // list all asset types whose link to the supplied asset type is NOT active
        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidParent_DisregardNotActiveLinks_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = false } }; // NOT active
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Supplied Parent AssetType", IsActive = true }, // count: can link to self
                new AssetType() { Id = 21, Name = "Linked Child AssetType", IsActive = true }}; // count: link NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Parent";
            string selectedParentChildRelationshipTypeId = "30";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 2;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "LinkAssetTypes Count");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedAssetTypeId, "Selected LinkAssetType Id");
        }

        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidChild_DisregardNotActiveLinks_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = false }}; // NOT active
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Supplied Child AssetType", IsActive = true }, // count: can link to self
                new AssetType() { Id = 21, Name = "Linked Parent AssetType", IsActive = true }}; // count: link NOT active
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Child";
            string selectedParentChildRelationshipTypeId = "30";
            int? selectedLinkedAssetTypeId = null;
            int expectedCount = 2;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.AssetTypes.Count(), "Link AssetTypes Count");
            Assert.IsNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value NOT selected in list");
            Assert.AreEqual(String.Empty, vmResult.SelectedAssetTypeId, "Selected LinkAssetType Id");
        }

        // list all asset types with selected asset type
        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidParent_ReturnSelectedAssetTypeFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }}; 
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Supplied Parent AssetType", IsActive = true }, 
                new AssetType() { Id = 21, Name = "Linked Child AssetType", IsActive = true }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Parent";
            string selectedParentChildRelationshipTypeId = "30";
            int selectedLinkedAssetTypeId = 21;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.IsNotNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value selected in list");
            Assert.AreEqual(selectedLinkedAssetTypeId.ToString(), vmResult.SelectedAssetTypeId, "Selected LinkAssetTypes Id");
        }

        [TestMethod()]
        public void DisplayLinkAssetTypes_Get_WhenProvidedAssetTypeIdIsValidChild_ReturnSelectedAssetTypeFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 21, ChildAssetTypeId = 20, ParentChildRelationshipTypeId = 30, IsActive = true }}; 
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Supplied Child AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Linked Parent AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int selectedAssetTypeId = 20;
            string selectedRelationshipLevelId = "Child";
            string selectedParentChildRelationshipTypeId = "30";
            int selectedLinkedAssetTypeId = 21;

            // Act
            var result = controller.DisplayLinkAssetTypes(selectedAssetTypeId, selectedRelationshipLevelId, selectedParentChildRelationshipTypeId, selectedLinkedAssetTypeId);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as DisplayLinkAssetTypesViewModel;
            Assert.IsNotNull(vmResult.AssetTypes.FirstOrDefault(r => r.Selected), "Value selected in list");
            Assert.AreEqual(selectedLinkedAssetTypeId.ToString(), vmResult.SelectedAssetTypeId, "Selected LinkAssetTypes Id");
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
        public void Edit_Get_WhenProvidedValuesAreValidParent_ReturnSuppliedAssetValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int id = 10;
            int assetTypeId = 20;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(assetTypeId, vmResult.SuppliedAssetTypeId, "Id");
            Assert.AreEqual("Parent AssetType", vmResult.SuppliedAssetTypeName, "Name");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValidChild_ReturnSuppliedAssetValuesFromDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int id = 10;
            int assetTypeId = 21;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(assetTypeId, vmResult.SuppliedAssetTypeId, "Id");
            Assert.AreEqual("Child AssetType", vmResult.SuppliedAssetTypeName, "Name");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValidParent_ReturnRelationshipLevels_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int id = 10;
            int assetTypeId = 20;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevels Count"); // parent / child
            Assert.AreEqual("Parent", vmResult.SelectedRelationshipLevel, "Selected RelationshipLevel");
        }

        [TestMethod()]
        public void Edit_Get_WhenProvidedValuesAreValidChild_ReturnRelationshipLevels_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int id = 10;
            int assetTypeId = 21;

            // Act
            var result = controller.Edit(id, assetTypeId);

            // Assert
            var viewResult = result as ViewResult;
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.AreEqual(2, vmResult.RelationshipLevels.Count(), "RelationshipLevels Count"); // parent / child
            Assert.AreEqual("Child", vmResult.SelectedRelationshipLevel, "Selected RelationshipLevel");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValidParent_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType 1", IsActive = true }, // old value
                new AssetType() { Id = 22, Name = "Child AssetType 2", IsActive = true }}; // new value
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "22"
            };

            // Act
            controller.Edit(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbAssetTypeRelationshipType = _dataAssetTypesRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, dbAssetTypeRelationshipType.ParentAssetTypeId, "Parent AssetType Id");
            Assert.AreEqual(vmExpected.SelectedAssetTypeId, dbAssetTypeRelationshipType.ChildAssetTypeId.ToString(), "Child AssetType Id");
            Assert.AreEqual(vmExpected.SelectedParentChildRelationshipTypeId, dbAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), "ParentChildRelationshipType Id");
            Assert.IsTrue(dbAssetTypeRelationshipType.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValidChild_UpdateDatabase_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType 1", IsActive = true }, // old value
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }, 
                new AssetType() { Id = 22, Name = "Parent AssetType 2", IsActive = true }}; // new value
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 21,
                SelectedRelationshipLevel = "Child",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "22"
            };

            // Act
            controller.Edit(vmExpected);

            // Assert
            Assert.IsTrue(_unitOfWork.Committed, "Transaction Committed");
            var dbAssetTypeRelationshipType = _dataAssetTypesRelationshipTypes.FirstOrDefault(r => r.Id == vmExpected.Id);
            Assert.AreEqual(vmExpected.SelectedAssetTypeId, dbAssetTypeRelationshipType.ParentAssetTypeId.ToString(), "Parent AssetType Id");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, dbAssetTypeRelationshipType.ChildAssetTypeId, "Child AssetType Id");
            Assert.AreEqual(vmExpected.SelectedParentChildRelationshipTypeId, dbAssetTypeRelationshipType.ParentChildRelationshipTypeId.ToString(), "ParentChildRelationshipType Id");
            Assert.IsTrue(dbAssetTypeRelationshipType.IsActive, "IsActive");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedViewModelIsValid_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType 1", IsActive = true }, // old value
                new AssetType() { Id = 22, Name = "Child AssetType 2", IsActive = true }}; // new value
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "22"
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, routeResult.RouteValues["id"], "Route Id");
            Assert.AreEqual("Parent-Child link updated.", controller.TempData["SuccessMessage"], "Message");
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
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel));
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedLinkedRelationshipIsDuplicatedParent_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }, // update record
                new AssetTypeRelationshipType() { Id = 11, ParentAssetTypeId = 20, ChildAssetTypeId = 22, ParentChildRelationshipTypeId = 30, IsActive = true }}; // duplicated record
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType 1", IsActive = true }, 
                new AssetType() { Id = 22, Name = "Child AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "22"
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "Relationship Levels");
            Assert.IsNotNull(vmResult.SelectedParentChildRelationshipTypeId, "Selected ParentChildRelationshipTypes");
            Assert.IsNotNull(vmResult.SelectedAssetTypeId, "Selected LinkedAssetType");
            Assert.AreEqual("Link already exists", controller.ViewData["ErrorMessage"], "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedLinkedRelationshipIsDuplicatedChild_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }, // update record
                new AssetTypeRelationshipType() { Id = 11, ParentAssetTypeId = 22, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }}; // duplicated record
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType 1", IsActive = true }, 
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true },
                new AssetType() { Id = 22, Name = "Parent AssetType 2", IsActive = true }};
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 21,
                SelectedRelationshipLevel = "Child",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "22"
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult), "View Result");
            var viewResult = result as ViewResult;
            Assert.AreEqual("Edit", viewResult.ViewName, "View Name");
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(EditViewModel), "View Model");
            var vmResult = viewResult.ViewData.Model as EditViewModel;
            Assert.IsNotNull(vmResult.RelationshipLevels, "Relationship Levels");
            Assert.IsNotNull(vmResult.SelectedParentChildRelationshipTypeId, "Selected ParentChildRelationshipTypes");
            Assert.IsNotNull(vmResult.SelectedAssetTypeId, "Selected LinkedAssetType");
            Assert.AreEqual("Link already exists", controller.ViewData["ErrorMessage"], "Message");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedLinkedRelationshipIsNotChangedParent_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 20,
                SelectedRelationshipLevel = "Parent",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "21"
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsFalse(_unitOfWork.Committed, "Transaction NOT Committed");
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, routeResult.RouteValues["id"], "Route Id");
        }

        [TestMethod()]
        public void Edit_Post_WhenProvidedLinkedRelationshipIsNotChangedChild_ReturnRouteValues_Test()
        {
            // Arrange
            var _dataAssetTypesRelationshipTypes = new List<AssetTypeRelationshipType>() {
                new AssetTypeRelationshipType() { Id = 10, ParentAssetTypeId = 20, ChildAssetTypeId = 21, ParentChildRelationshipTypeId = 30, IsActive = true }};
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationshipTypes);
            var _dataAssetTypes = new List<AssetType>() {
                new AssetType() { Id = 20, Name = "Parent AssetType", IsActive = true },
                new AssetType() { Id = 21, Name = "Child AssetType", IsActive = true }}; 
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_dataAssetTypes);
            var _dataParentChildRelationshipTypes = new List<ParentChildRelationshipType>() {
                new ParentChildRelationshipType() { Id = 30, ParentRelationshipTypeId = 40, ChildRelationshipTypeId = 41, IsActive = true }};
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            var _dataRelationshipTypes = new List<RelationshipType>() {
                new RelationshipType() { Id = 40, Name = "Parent-Child RelationshipType", IsActive = true },
                new RelationshipType() { Id = 41, Name = "Child-Parent RelationshipType", IsActive = true }};
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            var controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            var vmExpected = new EditViewModel()
            {
                Id = 10,
                SuppliedAssetTypeId = 21,
                SelectedRelationshipLevel = "Child",
                SelectedParentChildRelationshipTypeId = "30",
                SelectedAssetTypeId = "20"
            };

            // Act
            var result = controller.Edit(vmExpected);

            // Assert
            Assert.IsFalse(_unitOfWork.Committed, "Transaction NOT Committed");
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Route Result");
            var routeResult = result as RedirectToRouteResult;
            Assert.AreEqual("Details", routeResult.RouteValues["action"], "Route Action");
            Assert.AreEqual("AssetType", routeResult.RouteValues["controller"], "Route Controller");
            Assert.AreEqual(vmExpected.SuppliedAssetTypeId, routeResult.RouteValues["id"], "Route Id");
        }

    }
}