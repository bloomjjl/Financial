using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryAssetTransactionRepository : InMemoryRepository<AssetTransaction>, IAssetTransactionRepository
    {
        private List<AssetTransaction> _entities = null;

        public InMemoryAssetTransactionRepository(IEnumerable<AssetTransaction> entities)
            : base(entities)
        {
            _entities = entities as List<AssetTransaction>;
        }
    }
}
