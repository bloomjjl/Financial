﻿using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.Repositories
{
    public class AssetRelationshipRepository : Repository<AssetRelationship>, IAssetRelationshipRepository
    {
        public AssetRelationshipRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }
    }
}
