using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamitey;
using NUnit.Framework;
using Moq;
using NSubstitute;
using Financial.Data;
using Financial.Core;
using Financial.Core.Models;
using Financial.Data.Repositories;
using Financial.Data.RepositoryInterfaces;
using Financial.Tests.Mocks;
using Financial.Tests._DmitriNesteruk;


namespace Financial.Tests.Data.Repositories
{
    [TestFixture]
    public class AssetRepositoryTests
    {
        private Asset _dbAsset;
        private AssetSetting _dbAssetSetting;
        private AssetType _dbAssetType;
        private SettingType _dbSettingType;
        private DbSet<Asset> _mockAssetDbSet;
        private DbSet<AssetType> _mockAssetTypeDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private int _callCount;
        private AssetRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // setup fake model
            _dbAssetType = new AssetType { Id = 1, Name = "a", IsActive = true };
            _dbAsset = new Asset
            {
                Id = 2,
                AssetTypeId = _dbAssetType.Id,
                AssetType = _dbAssetType, // setup include
                Name = "b",
                IsActive = true
            };
            _dbSettingType = new SettingType { Id = 3, Name = "c", IsActive = true };
            _dbAssetSetting = new AssetSetting
            {
                Id = 4,
                AssetId = _dbAsset.Id,
                Asset = _dbAsset, // setup include
                SettingTypeId = _dbSettingType.Id,
                SettingType = _dbSettingType, // setup include
                Value = "d",
                IsActive = true
            };

            // setup DbContext
            Setup_FakeDbContext();

            // set up repository
            _repository = new AssetRepository(_fakeDbContext);
        }

        [TearDown]
        public void TearDown()
        {

        }



        [Test]
        public void Add_WhenAssetProvided_ShouldCallDbContextSetAssetProperty_Test()
        {
            Setup_Repository_MockDbContext(new List<Asset>());

            // Arrange
            var newAsset = new Asset { /*Id = 1,*/ AssetTypeId = 2, Name = "b", IsActive = true };

            // reset count for repository call
            _callCount = 0;

            // Act
            _repository.Add(newAsset);

            // Assert
            Assert.That(_callCount, Is.EqualTo(1));
        }

        [Test]
        public void Add_WhenAssetProvided_AddEntityToDbContext_Test()
        {
            // Arrange
            _fakeDbContext = new FinancialDbContext();

            _repository = new AssetRepository(_fakeDbContext);

            var expectedAsset = new Asset { /*Id = 1,*/ AssetTypeId = 2, Name = "b", IsActive = true };

            // Act
            _repository.Add(expectedAsset);
            var actualAsset = _fakeDbContext.Assets.Local.ToList()[0];

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualAsset.Id, Is.EqualTo(0), "Asset Id");
                Assert.That(actualAsset.Name, Is.EqualTo(expectedAsset.Name), "Asset Name");
                Assert.That(actualAsset.AssetTypeId, Is.EqualTo(expectedAsset.AssetTypeId), "AssetType Id");
                Assert.That(actualAsset.IsActive, Is.EqualTo(expectedAsset.IsActive), "IsActive");
            });
        }


        [Test]
        public void Get_WhenCalled_ReturnAsset_Test()
        {
            var result = _repository.Get(_dbAsset.Id);

            Assert.That(result, Is.InstanceOf<Asset>());
        }

        [Test]
        public void Get_WhenCalled_ReturnAssetValues_Test()
        {
            var result = _repository.Get(_dbAsset.Id);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(_dbAsset.Id), "Asset Id");
                Assert.That(result.AssetTypeId, Is.EqualTo(_dbAsset.AssetTypeId), "AssetType Id");
                Assert.That(result.AssetType.Name, Is.EqualTo(_dbAssetType.Name), "AssetType Name");
                Assert.That(result.Name, Is.EqualTo(_dbAsset.Name), "Asset Name");
                Assert.That(result.IsActive, Is.EqualTo(_dbAsset.IsActive), "IsActive");
            });
        }



        [Test]
        public void GetAllActiveOrderedByName_WhenCalled_ReturnAssetIEnumerable_Test()
        {
            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result, Is.InstanceOf<IEnumerable<Asset>>());
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenCalled_ReturnAssetValues_Test()
        {
            var result = _repository.GetAllActiveOrderedByName().ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Id, Is.EqualTo(_dbAsset.Id), "Asset Id");
                Assert.That(result[0].AssetTypeId, Is.EqualTo(_dbAsset.AssetTypeId), "AssetType Id");
                Assert.That(result[0].Name, Is.EqualTo(_dbAsset.Name), "Asset Name");
                Assert.That(result[0].IsActive, Is.EqualTo(_dbAsset.IsActive), "IsActive");
            });
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenIsActiveEqualsTrue_ReturnAsset_Test()
        {
            var fakeAssetTypes = new List<AssetType> {_dbAssetType};
            var fakeAssets = new List<Asset>
            {
                new Asset { Id = 1, AssetTypeId = _dbAssetType.Id, Name = "a", IsActive = true },
            };

            Setup_Repository_FakeDbContext(fakeAssets, fakeAssetTypes);

            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenIsActiveEqualsFalse_DoNotReturnAsset_Test()
        {
            var fakeAssetTypes = new List<AssetType> { _dbAssetType };
            var fakeAssets = new List<Asset>
            {
                new Asset { Id = 1, AssetTypeId = _dbAssetType.Id, Name = "a", IsActive = false },
            };

            Setup_Repository_FakeDbContext(fakeAssets, fakeAssetTypes);

            var result = _repository.GetAllActiveOrderedByName();

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetAllActiveOrderedByName_WhenMultipleAssetsFound_ReturnListSortedAscendingByAssetName_Test()
        {
            var fakeAssetTypes = new List<AssetType> { _dbAssetType };
            var fakeAssets = new List<Asset>
            {
                new Asset { Id = 1, AssetTypeId = _dbAssetType.Id, Name = "z", IsActive = true },
                new Asset { Id = 2, AssetTypeId = _dbAssetType.Id, Name = "a", IsActive = true }
            };

            Setup_Repository_FakeDbContext(fakeAssets, fakeAssetTypes);

            var result = _repository.GetAllActiveOrderedByName().ToList();

            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo("a"), "First Index");
                Assert.That(result[1].Name, Is.EqualTo("z"), "Second Index");
            });
        }



        // private methods


        private void Setup_FakeDbContext()
        {
            // setup dbContext
            Setup_FakeDbContext(
                new List<Asset> { _dbAsset }, 
                new List<AssetType> { _dbAssetType },
                new List<AssetSetting> { _dbAssetSetting },
                new List<SettingType> { _dbSettingType });
        }

        private void Setup_FakeDbContext(
            List<Asset> fakeAssetList,
            List<AssetType> fakeAssetTypeList,
            List<AssetSetting> fakeAssetSettingList,
            List<SettingType> fakeSettingTypeList)
        {
            // setup dbContext
            _fakeDbContext = MockFinancialDbContext.Create(
                assets: fakeAssetList,
                assetTypes: fakeAssetTypeList,
                assetSettings: fakeAssetSettingList,
                settingTypes: fakeSettingTypeList);
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

        private void Setup_Repository_FakeDbContext(List<Asset> fakeAssetList, List<AssetType> fakeAssetTypeList)
        {
            // setup DbContext
            Setup_FakeDbContext(fakeAssetList, fakeAssetTypeList);

            // set up repository
            _repository = new AssetRepository(_fakeDbContext);
        }

        private void Setup_Repository_MockDbContext(List<Asset> fakeAssets)
        {
            // setup DbSet
            _mockAssetDbSet = MockDbSet.Create<Asset>(fakeAssets);

            // setup DbContext
            _callCount = 0;
            _mockDbContext = new Mock<FinancialDbContext>();
            _mockDbContext.Setup(c => c.Set<Asset>())
                .Returns(_mockAssetDbSet)
                .Callback(() => _callCount++);

            // set up repository
            _repository = new AssetRepository(_mockDbContext.Object);
        }

    }
}
