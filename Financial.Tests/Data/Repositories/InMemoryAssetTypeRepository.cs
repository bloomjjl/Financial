using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryAssetTypeRepository : InMemoryRepository<AssetType>, IAssetTypeRepository
    {
        private List<AssetType> _entities = null;

        public InMemoryAssetTypeRepository(IEnumerable<AssetType> entities)
            : base(entities)
        {
            _entities = entities as List<AssetType>;
        }
    }
}
