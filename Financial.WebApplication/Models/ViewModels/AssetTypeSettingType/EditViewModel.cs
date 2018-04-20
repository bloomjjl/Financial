using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Models.ViewModels.AssetTypeSettingType
{
    public class EditViewModel
    {
        public EditViewModel()
        {
        }

        public EditViewModel(Core.Models.SettingType dtoSettingType,
            Core.Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            Id = dtoAssetTypeSettingType.Id;
            AssetTypeId = dtoAssetTypeSettingType.AssetTypeId;
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            IsActive = dtoAssetTypeSettingType.IsActive;
        }

        public EditViewModel(Core.Models.AssetType dtoAssetType,
            Core.Models.AssetTypeSettingType dtoAssetTypeSettingType)
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
