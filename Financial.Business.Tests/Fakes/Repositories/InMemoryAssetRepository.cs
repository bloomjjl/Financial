using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Repositories
{
    public class InMemoryAssetRepository : InMemoryRepository<Asset>, IAssetRepository
    {
        private List<Asset> _entities = null;

        public InMemoryAssetRepository(IEnumerable<Asset> entities)
            : base(entities)
        {
            _entities = entities as List<Asset>;
        }


        public IEnumerable<Asset> GetAllActiveOrderedByName()
        {
            return _entities
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToList();
        }
    }
}
