using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core;
using Financial.Core.Models;

namespace Financial.Tests.Mocks
{
    
    public static class MockFinancialDbContext
    {
        public static FinancialDbContext Create(
            List<Asset> assets = null,
            List<AssetSetting> assetSettings = null,
            List<AssetTransaction> assetTransactions = null,
            List<AssetType> assetTypes = null,
            List<SettingType> settingTypes = null,
            List<TransactionCategory> transactionCategories = null,
            List<TransactionDescription> transactionDescriptions = null,
            List<TransactionType> transactionTypes = null)
        {
            // setup dbContext
            return new FinancialDbContext
            {
                Assets = MockDbSet.Create<Asset>(assets),
                AssetSettings = MockDbSet.Create<AssetSetting>(assetSettings),
                AssetTransactions = MockDbSet.Create<AssetTransaction>(assetTransactions),
                AssetTypes = MockDbSet.Create<AssetType>(assetTypes),
                SettingTypes = MockDbSet.Create<SettingType>(settingTypes),
                TransactionCategories = MockDbSet.Create<TransactionCategory>(transactionCategories),
                TransactionDescriptions = MockDbSet.Create<TransactionDescription>(transactionDescriptions),
                TransactionTypes = MockDbSet.Create<TransactionType>(transactionTypes),
            };
        }
    }
}
