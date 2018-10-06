using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Models
{
    public class AccountTypeSettingType
    {
        public AccountTypeSettingType() { }
        /*
        public AssetTypeSettingType(AssetType assetType, Core.Models.SettingType settingType)
        {
            AssetTypeId = assetType.Id;
            AssetTypeName = assetType.Name;
            SettingTypeId = settingType.Id;
            SettingTypeName = settingType.Name;
        }
        */
        /*
        public AssetTypeSettingType(Core.Models.AssetTypeSettingType assetTypeSettingType, 
            AssetType assetType, 
            Core.Models.SettingType settingType)
        {
            AssetTypeSettingTypeId = assetTypeSettingType.Id;
            AssetTypeId = assetType.Id;
            AssetTypeName = assetType.Name;
            SettingTypeId = settingType.Id;
            SettingTypeName = settingType.Name;
            //IsActive = assetTypeSettingType.IsActive;
        }
        */
        public int AssetTypeSettingTypeId { get; set; }
        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }
        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }

    }
}
