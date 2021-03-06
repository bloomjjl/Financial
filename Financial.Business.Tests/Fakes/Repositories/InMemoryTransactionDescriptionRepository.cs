﻿using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Tests.Fakes.Repositories
{
    public class InMemoryTransactionDescriptionRepository : InMemoryRepository<TransactionDescription>, ITransactionDescriptionRepository
    {
        private List<TransactionDescription> _entities = null;

        public InMemoryTransactionDescriptionRepository(IEnumerable<TransactionDescription> entities)
            : base(entities)
        {
            _entities = entities as List<TransactionDescription>;
        }
    }
}