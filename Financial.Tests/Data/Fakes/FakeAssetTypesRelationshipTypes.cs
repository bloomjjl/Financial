using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Fakes
{
    public class FakeAssetTypesRelationshipTypes
    {
        public static IEnumerable<AssetTypeRelationshipType> InitialFakeAssetTypesRelationshipTypes()
        {
            yield return new AssetTypeRelationshipType() { Id = 1, ParentAssetTypeId = 2, ChildAssetTypeId = 4, ParentChildRelationshipTypeId = 5, ChildParentRelationshipTypeId = 1, IsActive = true };
            yield return new AssetTypeRelationshipType() { Id = 2, ParentAssetTypeId = 4, ChildAssetTypeId = 5, ParentChildRelationshipTypeId = 1, ChildParentRelationshipTypeId = 2, IsActive = true };
            yield return new AssetTypeRelationshipType() { Id = 3, ParentAssetTypeId = 3, ChildAssetTypeId = 3, ParentChildRelationshipTypeId = 3, ChildParentRelationshipTypeId = 3, IsActive = false };
            yield return new AssetTypeRelationshipType() { Id = 4, ParentAssetTypeId = 5, ChildAssetTypeId = 1, ParentChildRelationshipTypeId = 2, ChildParentRelationshipTypeId = 4, IsActive = true };
            yield return new AssetTypeRelationshipType() { Id = 5, ParentAssetTypeId = 1, ChildAssetTypeId = 2, ParentChildRelationshipTypeId = 4, ChildParentRelationshipTypeId = 5, IsActive = true };
        }
    }
}
