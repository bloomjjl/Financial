﻿using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemoryTransactionTypeRepository : InMemoryRepository<TransactionType>, ITransactionTypeRepository
    {
        private List<TransactionType> _entities = null;

        public InMemoryTransactionTypeRepository(IEnumerable<TransactionType> entities)
            : base(entities)
        {
            _entities = entities as List<TransactionType>;
        }
    }
}