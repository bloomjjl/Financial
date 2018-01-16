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
    public class AssetTypeRelationshipTypeControllerTestsBase
    {
        public AssetTypeRelationshipTypeControllerTestsBase()
        {
            // Fake Data
            _assetTypes = FakeAssetTypes.InitialFakeAssetTypes().ToList();
            _assetTypesRelationshipTypes = FakeAssetTypesRelationshipTypes.InitialFakeAssetTypesRelationshipTypes().ToList();
            _parentChildRelationshipTypes = FakeParentChildRelationshipTypes.InitialFakeParentChildRelationshipTypes().ToList();
            _relationshipTypes = FakeRelationshipTypes.InitialFakeRelationshipTypes().ToList();
            // Fake Repositories
            _repositoryAssetType = new InMemoryAssetTypeRepository(_assetTypes);
            _repositoryAssetTypeRelationshipType = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _repositoryParentChildRelationshipType = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _repositoryRelationshipType = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            // Fake Unit of Work
            _unitOfWork.AssetTypes = _repositoryAssetType;
            _unitOfWork.AssetTypesRelationshipTypes = _repositoryAssetTypeRelationshipType;
            _unitOfWork.ParentChildRelationshipTypes = _repositoryParentChildRelationshipType;
            _unitOfWork.RelationshipTypes = _repositoryRelationshipType;
            // Controller
            _controller = new AssetTypeRelationshipTypeController(_unitOfWork);
        }

        protected IList<AssetType> _assetTypes;
        protected IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes;
        protected IList<ParentChildRelationshipType> _parentChildRelationshipTypes;
        protected IList<RelationshipType> _relationshipTypes;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryAssetTypeRelationshipTypeRepository _repositoryAssetTypeRelationshipType;
        protected InMemoryParentChildRelationshipTypeRepository _repositoryParentChildRelationshipType;
        protected InMemoryRelationshipTypeRepository _repositoryRelationshipType;
        protected InMemoryUnitOfWork _unitOfWork = new InMemoryUnitOfWork();
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







        /*
        [TestMethod()]
        public void CreateLinkedRelationshipTypes_Child_WhenProvidedValuesAreValid_ReturnRouteValues_Test()
        {
            // Arrange
            AssetTypeRelationshipTypeController controller = _controller;
            int assetTypeId = 1;
            string relationshipLevel = "Parent";

            // Act
            var result = controller.CreateLinkedRelationshipTypes(assetTypeId, relationshipLevel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            var viewResult = result as PartialViewResult;
            Assert.AreEqual("_CreateLinkedRelationshipTypes", viewResult.ViewName);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(CreateLinkedRelationshipTypesViewModel));
        }

        [TestMethod()]
        public void CreateLinkedRelationshipTypes_Child_WhenProvidedParentValuesAreValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear all records
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 4, IsActive = true }); // count
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 3, ChildRelationshipTypeId = 5, IsActive = false }); // NOT active
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Parent RelationshipType", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Child RelationshipType 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Child RelationshipType 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 3;
            string relationshipLevel = "Parent";
            int expectedCount = 1;

            // Act
            var result = controller.CreateLinkedRelationshipTypes(assetTypeId, relationshipLevel);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedRelationshipTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.RelationshipTypes.Count(), "RelationshipTypes Count");
        }

        [TestMethod()]
        public void CreateLinkedRelationshipTypes_Child_WhenProvidedChildValuesAreValid_ReturnActiveValuesFromDatabase_Test()
        {
            // Arrange
            IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes = new List<AssetTypeRelationshipType>(); // clear all records
            IList<ParentChildRelationshipType> _parentChildRelationshipTypes = new List<ParentChildRelationshipType>();
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 1, ParentRelationshipTypeId = 4, ChildRelationshipTypeId = 3, IsActive = true }); // count
            _parentChildRelationshipTypes.Add(new ParentChildRelationshipType() { Id = 2, ParentRelationshipTypeId = 5, ChildRelationshipTypeId = 3, IsActive = false }); // NOT active
            IList<RelationshipType> _relationshipTypes = new List<RelationshipType>();
            _relationshipTypes.Add(new RelationshipType() { Id = 3, Name = "Child RelationshipType", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 4, Name = "Parent RelationshipType 1", IsActive = true });
            _relationshipTypes.Add(new RelationshipType() { Id = 5, Name = "Parent RelationshipType 2", IsActive = true });
            _unitOfWork.AssetTypesRelationshipTypes = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _unitOfWork.ParentChildRelationshipTypes = new InMemoryParentChildRelationshipTypeRepository(_parentChildRelationshipTypes);
            _unitOfWork.RelationshipTypes = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            AssetTypeRelationshipTypeController controller = new AssetTypeRelationshipTypeController(_unitOfWork);
            int assetTypeId = 3;
            string relationshipLevel = "Child";
            int expectedCount = 1;

            // Act
            var result = controller.CreateLinkedRelationshipTypes(assetTypeId, relationshipLevel);

            // Assert
            var viewResult = result as PartialViewResult;
            var vmResult = viewResult.ViewData.Model as CreateLinkedRelationshipTypesViewModel;
            Assert.AreEqual(expectedCount, vmResult.RelationshipTypes.Count(), "RelationshipTypes Count");
        }
        */
    }
}