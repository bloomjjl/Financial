using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Database
{
    public static class FakeAssetSettings
    {
        public static IEnumerable<AssetSetting> InitialFakeAssetSettings()
        {
            yield return new AssetSetting() { Id = 1, AssetId = 2, SettingTypeId = 4, Value = "AssetSetting1", IsActive = true };
            yield return new AssetSetting() { Id = 2, AssetId = 1, SettingTypeId = 4, Value = "AssetSetting2", IsActive = true };
            yield return new AssetSetting() { Id = 3, AssetId = 1, SettingTypeId = 2, Value = "AssetSetting3", IsActive = false };
            yield return new AssetSetting() { Id = 4, AssetId = 5, SettingTypeId = 2, Value = "AssetSetting4", IsActive = true };
            yield return new AssetSetting() { Id = 5, AssetId = 4, SettingTypeId = 1, Value = "AssetSetting5", IsActive = true };
        }
    }
}
