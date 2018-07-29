using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;

namespace Financial.Business.Tests.Fakes.Database
{
    public static class FakeAssetTypes
    {
        public static IEnumerable<AssetType> InitialFakeAssetTypes()
        {
            yield return new AssetType() { Id = 1, Name = "AssetTypeName1", IsActive = true };
            yield return new AssetType() { Id = 2, Name = "AssetTypeName2", IsActive = true };
            yield return new AssetType() { Id = 3, Name = "AssetTypeName3", IsActive = false };
            yield return new AssetType() { Id = 4, Name = "AssetTypeName4", IsActive = true };
            yield return new AssetType() { Id = 5, Name = "AssetTypeName5", IsActive = true };
        }
    }
}
