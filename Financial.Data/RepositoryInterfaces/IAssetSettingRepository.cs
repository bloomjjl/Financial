﻿using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface IAssetSettingRepository : IRepository<AssetSetting>
    {
        AssetSetting GetActive(int assetId, int settingTypeId);
        IEnumerable<AssetSetting> GetAllActiveForAsset(int assetId);
        IEnumerable<AssetSetting> GetAllActiveForSettingType(int settingTypeId);
    }
}
