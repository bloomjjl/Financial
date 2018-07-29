using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Database
{ 
    public static class FakeAssetTransactions
    {
        public static IEnumerable<AssetTransaction> InitialFakeAssetTransactions()
        {
            yield return new AssetTransaction() { Id = 1, AssetId = 2, TransactionTypeId = 4, TransactionCategoryId = 5, TransactionDescriptionId = 2, Amount = 1.11M, IsActive = true };
            yield return new AssetTransaction() { Id = 2, AssetId = 1, TransactionTypeId = 5, TransactionCategoryId = 4, TransactionDescriptionId = 4, Amount = 2.22M, IsActive = true };
            yield return new AssetTransaction() { Id = 3, AssetId = 2, TransactionTypeId = 1, TransactionCategoryId = 4, TransactionDescriptionId = 5, Amount = 3.33M, IsActive = false };
            yield return new AssetTransaction() { Id = 4, AssetId = 5, TransactionTypeId = 2, TransactionCategoryId = 1, TransactionDescriptionId = 5, Amount = 4.44M, IsActive = true };
            yield return new AssetTransaction() { Id = 5, AssetId = 4, TransactionTypeId = 1, TransactionCategoryId = 2, TransactionDescriptionId = 1, Amount = 5.55M, IsActive = true };
        }
    }
}
