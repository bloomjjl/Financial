using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {

        }

        public CreateViewModel(int assetTypeId, Models.SettingType dtoSettingType)
        {
            AssetTypeId = assetTypeId;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            IsActive = true;
        }

        public CreateViewModel(int settingTypeId, Models.AssetType dtoAssetType)
        {
            SettingTypeId = settingTypeId;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            IsActive = true;
        }


        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
