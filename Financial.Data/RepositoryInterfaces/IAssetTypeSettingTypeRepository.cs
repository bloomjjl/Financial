using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface IAssetTypeSettingTypeRepository : IRepository<AssetTypeSettingType>
    {
        AssetTypeSettingType GetActive(int assetTypeId, int settingTypeId);
        IEnumerable<AssetTypeSettingType> GetAllActiveForAssetType(int assetTypeId);
        IEnumerable<AssetTypeSettingType> GetAllActiveForSettingType(int settingTypeId);
    }
}
