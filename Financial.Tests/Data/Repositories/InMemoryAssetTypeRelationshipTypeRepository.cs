using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryAssetTypeRelationshipTypeRepository : InMemoryRepository<AssetTypeRelationshipType>, IAssetTypeRelationshipTypeRepository
    {
        private List<AssetTypeRelationshipType> _entities = null;

        public InMemoryAssetTypeRelationshipTypeRepository(IEnumerable<AssetTypeRelationshipType> entities)
            : base(entities)
        {
            _entities = entities as List<AssetTypeRelationshipType>;
        }
    }
}
