using Financial.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAccountTypeSettingTypeService
    {
        Business.Models.AccountType CreateLinkedSettingTypesGetModel(int assetTypeId);
        Business.Models.AccountType EditLinkedSettingTypesGetModel(int assetTypeId);




        List<AccountType> GetListOfLinkedAssetTypes(int settingTypeId);

        List<AttributeType> GetListOfLinkedSettingTypes(int assetTypeId);

        List<AttributeType> GetListOfSettingTypesWithLinkedAssetType(int assetTypeId);

    }
}
