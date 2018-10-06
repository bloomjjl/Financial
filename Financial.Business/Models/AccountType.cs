using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Models
{
    public class AccountType
    {
        public AccountType() { }
        public AccountType(Core.Models.AssetType dtoAssetType)
        {
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
        }
        public AccountType(Core.Models.AssetType dtoAssetType,
            Core.Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            AssetTypeId = dtoAssetType.Id;
            AssetTypeName = dtoAssetType.Name;
            AssetTypeSettingTypeId = dtoAssetTypeSettingType.Id;
        }

        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }
        public int AssetTypeSettingTypeId { get; set; }
    }
}
