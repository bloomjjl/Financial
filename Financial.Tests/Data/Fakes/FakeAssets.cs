using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Fakes
{
    public static class FakeAssets
    {
        public static IEnumerable<Asset> InitialFakeAssets()
        {
            yield return new Asset() { Id = 1, AssetTypeId = 2, Name = "AssetTypeName1", IsActive = true };
            yield return new Asset() { Id = 2, AssetTypeId = 1, Name = "AssetTypeName2", IsActive = true };
            yield return new Asset() { Id = 3, AssetTypeId = 2, Name = "AssetTypeName3", IsActive = false };
            yield return new Asset() { Id = 4, AssetTypeId = 2, Name = "AssetTypeName4", IsActive = true };
            yield return new Asset() { Id = 5, AssetTypeId = 2, Name = "AssetTypeName5", IsActive = true };
        }
    }
}
