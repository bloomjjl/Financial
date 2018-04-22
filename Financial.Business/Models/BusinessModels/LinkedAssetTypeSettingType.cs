using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Models.BusinessModels
{
    public class LinkedAssetTypeSettingType : AssetTypeSettingType
    {
        public LinkedAssetTypeSettingType() { }
        public LinkedAssetTypeSettingType(AssetType assetType, SettingType settingType)
        {
            AssetTypeId = assetType.Id;
            AssetTypeName = assetType.Name;
            SettingTypeId = settingType.Id;
            SettingTypeName = settingType.Name;
        }
        public LinkedAssetTypeSettingType(AssetTypeSettingType assetTypeSettingType, AssetType assetType, SettingType settingType)
        {
            Id = assetTypeSettingType.Id;
            AssetTypeId = assetType.Id;
            AssetTypeName = assetType.Name;
            SettingTypeId = settingType.Id;
            SettingTypeName = settingType.Name;
            IsActive = assetTypeSettingType.IsActive;
        }

        public string AssetTypeName { get; set; }
        public string SettingTypeName { get; set; }

    }
}
