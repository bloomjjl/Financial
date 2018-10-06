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
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }





        public IEnumerable<TransactionType> GetAllActiveOrderedByName()
        {
            return FinancialDbContext.TransactionTypes
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToList();
        }
    }
}
