﻿using Financial.Core;
using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }




        public IEnumerable<Asset> GetAllActiveOrderedByName()
        {
            return FinancialDbContext.Assets
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToList();
        }
    }
}
