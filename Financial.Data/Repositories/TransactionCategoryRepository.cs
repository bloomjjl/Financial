using Financial.Core;
using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionCategory = Financial.Core.Models.TransactionCategory;

namespace Financial.Data.Repositories
{
    public class TransactionCategoryRepository : Repository<TransactionCategory>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }



        public IEnumerable<TransactionCategory> GetAllActiveOrderedByName()
        {
            return FinancialDbContext.TransactionCategories
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name);
        }
    }
}
