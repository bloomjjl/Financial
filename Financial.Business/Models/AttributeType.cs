using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Models
{
    public class AttributeType
    {
        public AttributeType() { }
        public AttributeType(Core.Models.SettingType dtoSettingType)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
        }
        public AttributeType(Core.Models.SettingType dtoSettingType, 
            Core.Models.AssetTypeSettingType dtoAssetTypeSettingType)
        {
            SettingTypeId = dtoSettingType.Id;
            SettingTypeName = dtoSettingType.Name;
            AssetTypeSettingTypeId = dtoAssetTypeSettingType.Id;
        }
        public int SettingTypeId { get; set; }
        public string SettingTypeName { get; set; }
        public int AssetTypeSettingTypeId { get; set; }

    }
}
