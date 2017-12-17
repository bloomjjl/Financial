using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
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
    }
}
