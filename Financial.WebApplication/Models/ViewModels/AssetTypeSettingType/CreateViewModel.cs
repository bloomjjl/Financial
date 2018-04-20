using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {

        }

        public CreateViewModel(int assetTypeId, Core.Models.SettingType dtoSettingType)
        {
            AssetTypeId = assetTypeId;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            IsActive = false;
        }

        public CreateViewModel(int settingTypeId, Core.Models.AssetType dtoAssetType)
        {
            SettingTypeId = settingTypeId;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            IsActive = false;
        }


        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
