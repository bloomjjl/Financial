using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using Financial.Data.RepositoryInterfaces;
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



        public IEnumerable<AssetTransaction> GetAllActiveByDescendingDueDate(int assetId)
        {
            return _entities
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .OrderByDescending(r => r.DueDate)
                .ToList();
        }

        public IEnumerable<AssetTransaction> GetAllActiveByDueDate()
        {
            return _entities
                .Where(r => r.IsActive)
                .OrderBy(r => r.DueDate)
                .ToList();
        }


    }
}
