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
    public class SettingTypeRepository : Repository<SettingType>, ISettingTypeRepository
    {
        public SettingTypeRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }




        public IEnumerable<SettingType> GetAllOrderedByName()
        {
            return FinancialDbContext.SettingTypes
                .OrderBy(r => r.Name)
                .ToList();
        }

        public int CountMatching(string name)
        {
            return FinancialDbContext.SettingTypes
                .Count(r => r.Name == name);
        }

        public int CountMatching(int excludeId, string name)
        {
            return FinancialDbContext.SettingTypes
                .Where(r => r.Id != excludeId)
                .Count(r => r.Name == name);
        }


    }
}
