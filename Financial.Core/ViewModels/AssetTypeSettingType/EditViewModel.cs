using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Core.ViewModels.AssetTypeSettingType
{
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public EditViewModel(Models.SettingType dtoSettingType, Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            Id = dtoAssetTypeSettingType.Id;
            AssetTypeId = dtoAssetTypeSettingType.AssetTypeId;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            IsActive = dtoAssetTypeSettingType.IsActive;
        }

        public EditViewModel(Models.AssetType dtoAssetType, Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            Id = dtoAssetTypeSettingType.Id;
            SettingTypeId = dtoAssetTypeSettingType.SettingTypeId;
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            IsActive = dtoAssetTypeSettingType.IsActive;
        }

        public int Id { get; set; }

        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }

        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }

        public bool IsActive { get; set; }
    }
}
