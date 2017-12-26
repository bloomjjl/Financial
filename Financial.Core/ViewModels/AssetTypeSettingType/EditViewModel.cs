using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class EditViewModel
    {
        public EditViewModel() { }
        public EditViewModel(Models.SettingType dtoSettingType, Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            AssetTypeId = dtoAssetTypeSettingType.AssetTypeId;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            IsActive = dtoAssetTypeSettingType.IsActive;
        }


        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
