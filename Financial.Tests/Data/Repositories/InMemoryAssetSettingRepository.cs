using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryAssetSettingRepository : InMemoryRepository<AssetSetting>, IAssetSettingRepository
    {
        private List<AssetSetting> _entities = null;

        public InMemoryAssetSettingRepository(IEnumerable<AssetSetting> entities)
            : base(entities)
        {
            _entities = entities as List<AssetSetting>;
        }




        public AssetSetting GetActive(int assetId, int settingTypeId)
        {
            return _entities
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .FirstOrDefault(r => r.SettingTypeId == settingTypeId);
        }

        public IEnumerable<AssetSetting> GetAllActiveForAsset(int assetId)
        {
            return _entities
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .ToList();
        }

        public IEnumerable<AssetSetting> GetAllActiveForSettingType(int settingTypeId)
        {
            return _entities
                .Where(r => r.IsActive)
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();
        }
    }
}
