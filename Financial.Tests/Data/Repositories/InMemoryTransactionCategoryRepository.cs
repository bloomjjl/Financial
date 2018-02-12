using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryTransactionCategoryRepository : InMemoryRepository<TransactionCategory>, ITransactionCategoryRepository
    {
        private List<TransactionCategory> _entities = null;

        public InMemoryTransactionCategoryRepository(IEnumerable<TransactionCategory> entities)
            : base(entities)
        {
            _entities = entities as List<TransactionCategory>;
        }
    }
}