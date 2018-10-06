using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Financial.Core.Models;
using System.Data.Entity;
using Moq;
using Financial.Core;
using Financial.Data.Repositories;
using Financial.Tests.Mocks;

namespace Financial.Tests.Data.Repositories
{
    [TestFixture]
    public class AssetTypeRepositoryTests
    {
        private Asset _dbAsset;
        private AssetType _dbAssetType;
        private DbSet<Asset> _mockAssetDbSet;
        private DbSet<AssetType> _mockAssetTypeDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private int _callCount;
        private AssetTypeRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // setup fake model
            _dbAssetType = new AssetType { Id = 1, Name = "a", IsActive = true };
            _dbAsset = new Asset { Id = 2, AssetTypeId = _dbAssetType.Id, Name = "b", IsActive = true };

            // setup DbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new AssetTypeRepository(_fakeDbContext);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void Get_WhenCalled_ReturnAssetType_Test()
        {
            var result = _repository.Get(_dbAssetType.Id);

            Assert.That(result, Is.InstanceOf<AssetType>());
        }


        // private methods



        private void Setup_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext(
                new List<Asset> { _dbAsset },
                new List<AssetType> { _dbAssetType });
        }

        private void Setup_FakeDbContext(
            List<Asset> fakeAssetList,
            List<AssetType> fakeAssetTypeList)
        {
            // setup dbContext
            _fakeDbContext = MockFinancialDbContext.Create(
                assets: fakeAssetList,
                assetTypes: fakeAssetTypeList);
        }

    }
}
