using System;
using System.Collections.Generic;
using System.Linq;
using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;

namespace Financial.Business.Tests.Fakes.Repositories
{
    public class InMemoryAssetTypeRepository : InMemoryRepository<AssetType>, IAssetTypeRepository
    {
        private List<AssetType> _entities = null;

        public InMemoryAssetTypeRepository(IEnumerable<AssetType> entities)
            : base(entities)
        {
            _entities = entities as List<AssetType>;
        }




        public int CountMatching(string name)
        {
            return _entities
                .Count(r => r.Name == name);
        }

        public int CountMatching(int excludeId, string name)
        {
            return _entities
                .Where(r => r.Id != excludeId)
                .Count(r => r.Name == name);
        }

        public IEnumerable<AssetType> GetAllActiveOrderedByName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetType> GetAllOrderedByName()
        {
            return _entities
                .OrderBy(r => r.Name)
                .ToList();
        }
    }
}
