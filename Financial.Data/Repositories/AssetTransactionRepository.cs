using Financial.Core;
using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.Repositories
{
    public class AssetTransactionRepository : Repository<AssetTransaction>, IAssetTransactionRepository
    {
        public AssetTransactionRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }



        public IEnumerable<AssetTransaction> GetAllActiveByDueDate()
        {
            return FinancialDbContext.AssetTransactions
                .Where(r => r.IsActive)
                .OrderBy(r => r.DueDate)
                .ToList();
        }

        public IEnumerable<AssetTransaction> GetAllActiveByDescendingDueDate(int assetId)
        {
            return FinancialDbContext.AssetTransactions
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .OrderByDescending(r => r.DueDate)
                .ToList();
        }
    }
}
