using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Financial.Business.Services;
using Financial.Business.Tests.Fakes.Repositories;

namespace Financial.Business.Tests.Services
{
    public class AssetTypeServiceTestsBase : ServiceTestsBase
    {
        public AssetTypeServiceTestsBase()
        {
            _service = new AssetTypeService(_unitOfWork);
        }

        protected AssetTypeService _service;
    }


    [TestClass()]
    public class AssetTypeServiceTests : AssetTypeServiceTestsBase
    {
        [TestMethod()]
        public void GetAssetType_WhenProvidedValidInput_ReturnValue_Test()
        {
            // Assemble
            var assetTypeId = 1;

            // Act
            var result = _service.GetAssetType(assetTypeId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Business.Models.AssetType), "Result Type");
            Assert.AreEqual(assetTypeId, result.AssetTypeId, "Asset Type Id");
        }

        [TestMethod()]
        public void GetAssetType_WhenProvidedInvalidAssetTypeId_ReturnValue_Test()
        {
            // Assemble
            var assetTypeId = 0;

            // Act
            var result = _service.GetAssetType(assetTypeId);

            // Assert
            Assert.IsNull(result, "Result");
        }

        [TestMethod()]
        public void GetAssetType_WhenProvidedValidInputIsNotActive_ReturnValue_Test()
        {
            // Assemble
            var _fakeAssetTypes = new List<Core.Models.AssetType>() {
                new Core.Models.AssetType() { Id = 10, Name = "Name 1", IsActive = false }
            };
            _unitOfWork.AssetTypes = new InMemoryAssetTypeRepository(_fakeAssetTypes);
            var service = new AssetTypeService(_unitOfWork);

            // Act
            var result = _service.GetAssetType(_fakeAssetTypes[0].Id);

            // Assert
            Assert.IsNull(result, "Result");
        }
    }
}