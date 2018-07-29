using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Repositories
{
    public class InMemoryRelationshipTypeRepository : InMemoryRepository<RelationshipType>, IRelationshipTypeRepository
    {
        private List<RelationshipType> _entities = null;

        public InMemoryRelationshipTypeRepository(IEnumerable<RelationshipType> entities)
            : base(entities)
        {
            _entities = entities as List<RelationshipType>;
        }
    }
}
