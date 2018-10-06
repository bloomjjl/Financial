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
    public class AssetTypeRepository : Repository<AssetType>, IAssetTypeRepository
    {
        public AssetTypeRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }



        public new AssetType Get(int id)
        {
            return FinancialDbContext.AssetTypes
                .Include("Asset")
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<AssetType> GetAllActiveOrderedByName()
        {
            return FinancialDbContext.AssetTypes
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToList();
        }
        public IEnumerable<AssetType> GetAllOrderedByName()
        {
            return FinancialDbContext.AssetTypes
                .OrderBy(r => r.Name)
                .ToList();
        }

        public int CountMatching(string name)
        {
            return FinancialDbContext.AssetTypes
                .Count(r => r.Name == name);
            
        }

        public int CountMatching(int excludeId, string name)
        {
            return FinancialDbContext.AssetTypes
                .Where(r => r.Id != excludeId)
                .Count(r => r.Name == name);
        }

    }
}
