using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetSetting
{
    public class EditViewModel
    {
        public EditViewModel() { }

        public EditViewModel(Models.AssetSetting dtoAssetSetting, Models.Asset dtoAsset, Models.SettingType dtoSettingType)
        {
            Id = dtoAssetSetting.Id;
            AssetId = dtoAsset.Id;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            Value = dtoAssetSetting.Value;
            IsActive = dtoAssetSetting.IsActive;
        }

        public int Id { get; set; }
        public int AssetId { get; set; }
        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}
