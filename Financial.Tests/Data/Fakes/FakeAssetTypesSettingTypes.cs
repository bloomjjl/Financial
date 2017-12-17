using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Fakes
{
    public class FakeAssetTypesSettingTypes
    {
        public static IEnumerable<AssetTypeSettingType> InitialFakeAssetTypesSettingTypes()
        {
            yield return new AssetTypeSettingType() { Id = 1, AssettTypeId = 1, SettingTypeId = 4, IsActive = true };
            yield return new AssetTypeSettingType() { Id = 2, AssettTypeId = 1, SettingTypeId = 4, IsActive = true };
            yield return new AssetTypeSettingType() { Id = 3, AssettTypeId = 1, SettingTypeId = 2, IsActive = false };
            yield return new AssetTypeSettingType() { Id = 4, AssettTypeId = 5, SettingTypeId = 2, IsActive = true };
            yield return new AssetTypeSettingType() { Id = 5, AssettTypeId = 4, SettingTypeId = 1, IsActive = true };
        }
    }
}
