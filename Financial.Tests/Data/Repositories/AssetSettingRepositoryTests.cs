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
    public class AssetSettingRepositoryTests
    {
        private Asset _dbAsset;
        private AssetSetting _dbAssetSetting;
        private AssetType _dbAssetType;
        private SettingType _dbSettingType;
        private DbSet<AssetSetting> _mockAssetSettingDbSet;
        private Mock<FinancialDbContext> _mockDbContext;
        private FinancialDbContext _fakeDbContext;
        private AssetSettingRepository _repository;

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

            // setup repository
            _repository = new AssetSettingRepository(_fakeDbContext);
        }

        [TearDown]
        public void TearDown()
        {

        }




        [Test]
        public void GetActive_WhenCalled_ReturnAssetSetting_Test()
        {           
            var result = _repository.GetActive(_dbAssetSetting.AssetId, _dbAssetSetting.SettingTypeId);

            Assert.That(result, Is.InstanceOf<AssetSetting>());
        }

        [Test]
        public void GetActive_WhenCalled_ReturnAssetSettingValues_Test()
        {
            var result = _repository.GetActive(_dbAssetSetting.AssetId, _dbAssetSetting.SettingTypeId);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(_dbAssetSetting.Id), "AssetSetting Id");
                Assert.That(result.AssetId, Is.EqualTo(_dbAssetSetting.AssetId), "Asset Id");
                Assert.That(result.Asset.Name, Is.EqualTo(_dbAsset.Name), "Asset Name");
                Assert.That(result.SettingTypeId, Is.EqualTo(_dbAssetSetting.SettingTypeId), "SettingType Id");
                Assert.That(result.SettingType.Name, Is.EqualTo(_dbSettingType.Name), "SettingType Name");
                Assert.That(result.Value, Is.EqualTo(_dbAssetSetting.Value), "AssetSetting Value");
                Assert.That(result.IsActive, Is.EqualTo(_dbAssetSetting.IsActive), "IsActive");
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

    }
}
