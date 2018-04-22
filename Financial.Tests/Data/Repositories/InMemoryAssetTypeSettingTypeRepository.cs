using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryAssetTypeSettingTypeRepository : InMemoryRepository<AssetTypeSettingType>, IAssetTypeSettingTypeRepository
    {
        private List<AssetTypeSettingType> _entities = null;

        public InMemoryAssetTypeSettingTypeRepository(IEnumerable<AssetTypeSettingType> entities)
            : base(entities)
        {
            _entities = entities as List<AssetTypeSettingType>;
        }

        public AssetTypeSettingType GetActive(int assetTypeId, int settingTypeId)
        {
            throw new NotImplementedException();
        }






        public IEnumerable<AssetTypeSettingType> GetAllActiveForAssetType(int assetTypeId)
        {
            return _entities
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();
        }

        public IEnumerable<AssetTypeSettingType> GetAllActiveForSettingType(int settingTypeId)
        {
            return _entities
                .Where(r => r.IsActive)
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();
        }
    }
}
