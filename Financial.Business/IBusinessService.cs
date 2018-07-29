using Financial.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business
{
    public interface IBusinessService
    {
        IAssetService AssetService { get; }
        IAssetSettingService AssetSettingService { get; }
        IAssetTypeService AssetTypeService { get; }
        IAssetTypeSettingTypeService AssetTypeSettingTypeService { get; }
        ISettingTypeService SettingTypeService { get; }
    }
}
