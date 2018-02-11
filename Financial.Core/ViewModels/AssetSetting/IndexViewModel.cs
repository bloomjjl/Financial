using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetSetting
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Models.AssetSetting dtoAssetSetting, int assetId, Models.SettingType dtoSettingType )
        {
            Id = dtoAssetSetting.Id;
            AssetId = assetId;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            AssetSettingValue = dtoAssetSetting.Value;
        }


        public int Id { get; set; }
        public int AssetId { get; set; }
        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }
        public string AssetSettingValue { get; set; }
    }
}
