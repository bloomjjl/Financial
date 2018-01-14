using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryAssetRepository : InMemoryRepository<Asset>, IAssetRepository
    {
        private List<Asset> _entities = null;

        public InMemoryAssetRepository(IEnumerable<Asset> entities)
            : base(entities)
        {
            _entities = entities as List<Asset>;
        }
    }
}
