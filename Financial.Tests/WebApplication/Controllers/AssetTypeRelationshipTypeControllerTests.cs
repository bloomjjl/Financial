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
            _relationshipTypes = FakeRelationshipTypes.InitialFakeRelationshipTypes().ToList();
            // Fake Repositories
            _repositoryAssetType = new InMemoryAssetTypeRepository(_assetTypes);
            _repositoryAssetTypeRelationshipType = new InMemoryAssetTypeRelationshipTypeRepository(_assetTypesRelationshipTypes);
            _repositoryRelationshipType = new InMemoryRelationshipTypeRepository(_relationshipTypes);
            // Fake Unit of Work
            _unitOfWork.AssetTypes = _repositoryAssetType;
            _unitOfWork.AssetTypesRelationshipTypes = _repositoryAssetTypeRelationshipType;
            _unitOfWork.RelationshipTypes = _repositoryRelationshipType;
            // Controller
            _controller = new AssetTypeRelationshipTypeController(_unitOfWork);
        }

        protected IList<AssetType> _assetTypes;
        protected IList<AssetTypeRelationshipType> _assetTypesRelationshipTypes;
        protected IList<RelationshipType> _relationshipTypes;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryAssetTypeRelationshipTypeRepository _repositoryAssetTypeRelationshipType;
        protected InMemoryRelationshipTypeRepository _repositoryRelationshipType;
        protected InMemoryUnitOfWork _unitOfWork = new InMemoryUnitOfWork();
        protected AssetTypeRelationshipTypeController _controller;
    }


    [TestClass()]
    public class AssetTypeRelationshipTypeControllerTests : AssetTypeRelationshipTypeControllerTestsBase
    {
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

    }
}