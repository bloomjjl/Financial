using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryParentChildRelationshipTypeRepository : InMemoryRepository<ParentChildRelationshipType>, IParentChildRelationshipTypeRepository
    {
        private List<ParentChildRelationshipType> _entities = null;

        public InMemoryParentChildRelationshipTypeRepository(IEnumerable<ParentChildRelationshipType> entities)
            : base(entities)
        {
            _entities = entities as List<ParentChildRelationshipType>;
        }
    }
}
