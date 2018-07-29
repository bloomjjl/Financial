using Financial.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAssetTypeSettingTypeService
    {
        Business.Models.AssetType CreateLinkedSettingTypesGetModel(int assetTypeId);
        Business.Models.AssetType EditLinkedSettingTypesGetModel(int assetTypeId);




        List<AssetType> GetListOfLinkedAssetTypes(int settingTypeId);

        List<SettingType> GetListOfLinkedSettingTypes(int assetTypeId);

        List<SettingType> GetListOfSettingTypesWithLinkedAssetType(int assetTypeId);

    }
}
