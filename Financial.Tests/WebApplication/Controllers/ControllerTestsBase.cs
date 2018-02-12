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
using Financial.Core.ViewModels.Asset;

namespace Financial.Tests.WebApplication.Controllers
{
    public class ControllerTestsBase
    {
        public ControllerTestsBase()
        {
            // Fake Data
            _dataAssets = FakeAssets.InitialFakeAssets().ToList();
            //_dataAssetRelationships = FakeAssets.InitialFakeAssetRelationships().ToList();
            _dataAssetSettings = FakeAssetSettings.InitialFakeAssetSettings().ToList();
            _dataAssetTransactions = FakeAssetTransactions.InitialFakeAssetTransactions().ToList();
            _dataAssetTypes = FakeAssetTypes.InitialFakeAssetTypes().ToList();
            _dataAssetTypesRelationsihpTypes = FakeAssetTypesRelationshipTypes.InitialFakeAssetTypesRelationshipTypes().ToList();
            _dataAssetTypesSettingTypes = FakeAssetTypesSettingTypes.InitialFakeAssetTypesSettingTypes().ToList();
            _dataParentChildRelationshipTypes = FakeParentChildRelationshipTypes.InitialFakeParentChildRelationshipTypes().ToList();
            _dataRelationshipTypes = FakeRelationshipTypes.InitialFakeRelationshipTypes().ToList();
            _dataSettingTypes = FakeSettingTypes.InitialFakeSettingTypes().ToList();
            _dataTransactionCategories = FakeTransactionCategories.InitialFakeTransactionCategories().ToList();
            _dataTransactionDescriptions = FakeTransactionDescriptions.InitialFakeTransactionDescriptions().ToList();
            _dataTransactionTypes = FakeTransactionTypes.InitialFakeTransactionTypes().ToList();

            // Fake Repositories
            _repositoryAsset = new InMemoryAssetRepository(_dataAssets);
            //_repositoryAssetRelationship = new InMemoryAssetRelationshipRepository(_dataAssetRelationships);
            _repositoryAssetSetting = new InMemoryAssetSettingRepository(_dataAssetSettings);
            _repositoryAssetTransaction = new InMemoryAssetTransactionRepository(_dataAssetTransactions);
            _repositoryAssetType = new InMemoryAssetTypeRepository(_dataAssetTypes);
            _repositoryAssetTypeRelationshipType = new InMemoryAssetTypeRelationshipTypeRepository(_dataAssetTypesRelationsihpTypes);
            _repositoryAssetTypeSettingType = new InMemoryAssetTypeSettingTypeRepository(_dataAssetTypesSettingTypes);
            _repositoryParentChildRelationshipType = new InMemoryParentChildRelationshipTypeRepository(_dataParentChildRelationshipTypes);
            _repositoryRelationshipType = new InMemoryRelationshipTypeRepository(_dataRelationshipTypes);
            _repositorySettingType = new InMemorySettingTypeRepository(_dataSettingTypes);
            _repositoryTransactionCategory = new InMemoryTransactionCategoryRepository(_dataTransactionCategories);
            _repositoryTransactionDescription = new InMemoryTransactionDescriptionRepository(_dataTransactionDescriptions);
            _repositoryTransactionType = new InMemoryTransactionTypeRepository(_dataTransactionTypes);

            // Fake Unit of Work
            _unitOfWork = new InMemoryUnitOfWork()
            {
                Assets = _repositoryAsset,
                //AssetRelationships = _repositoryAssetRelationship,
                AssetSettings = _repositoryAssetSetting,
                AssetTransactions = _repositoryAssetTransaction,
                AssetTypes = _repositoryAssetType,
                AssetTypesRelationshipTypes = _repositoryAssetTypeRelationshipType,
                AssetTypesSettingTypes = _repositoryAssetTypeSettingType,
                ParentChildRelationshipTypes = _repositoryParentChildRelationshipType,
                RelationshipTypes = _repositoryRelationshipType,
                SettingTypes = _repositorySettingType,
                TransactionCategories = _repositoryTransactionCategory,
                TransactionDescriptions = _repositoryTransactionDescription,
                TransactionTypes = _repositoryTransactionType
            };
        }

        protected IList<Asset> _dataAssets;
        //protected IList<AssetRelationship> _dataAssetRelationships;
        protected IList<AssetSetting> _dataAssetSettings;
        protected IList<AssetTransaction> _dataAssetTransactions;
        protected IList<AssetType> _dataAssetTypes;
        protected IList<AssetTypeRelationshipType> _dataAssetTypesRelationsihpTypes;
        protected IList<AssetTypeSettingType> _dataAssetTypesSettingTypes;
        protected IList<ParentChildRelationshipType> _dataParentChildRelationshipTypes;
        protected IList<RelationshipType> _dataRelationshipTypes;
        protected IList<SettingType> _dataSettingTypes;
        protected IList<TransactionCategory> _dataTransactionCategories;
        protected IList<TransactionDescription> _dataTransactionDescriptions;
        protected IList<TransactionType> _dataTransactionTypes;

        protected InMemoryAssetRepository _repositoryAsset;
        //protected InMemoryAssetRelationshipRepository _repositoryAssetRelationship;
        protected InMemoryAssetSettingRepository _repositoryAssetSetting;
        protected InMemoryAssetTransactionRepository _repositoryAssetTransaction;
        protected InMemoryAssetTypeRepository _repositoryAssetType;
        protected InMemoryAssetTypeRelationshipTypeRepository _repositoryAssetTypeRelationshipType;
        protected InMemoryAssetTypeSettingTypeRepository _repositoryAssetTypeSettingType;
        protected InMemoryParentChildRelationshipTypeRepository _repositoryParentChildRelationshipType;
        protected InMemoryRelationshipTypeRepository _repositoryRelationshipType;
        protected InMemorySettingTypeRepository _repositorySettingType;
        protected InMemoryTransactionCategoryRepository _repositoryTransactionCategory;
        protected InMemoryTransactionDescriptionRepository _repositoryTransactionDescription;
        protected InMemoryTransactionTypeRepository _repositoryTransactionType;

        protected InMemoryUnitOfWork _unitOfWork;
    }

}