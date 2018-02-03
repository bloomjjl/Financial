using Financial.Core.Models;
using Financial.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
