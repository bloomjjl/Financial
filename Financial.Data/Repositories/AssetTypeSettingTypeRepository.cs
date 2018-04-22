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
    public class AssetTypeSettingTypeRepository : Repository<AssetTypeSettingType>, IAssetTypeSettingTypeRepository
    {
        public AssetTypeSettingTypeRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }



        public AssetTypeSettingType Get(int assetTypeId, int settingTypeId)
        {
            return FinancialDbContext.AssetTypesSettingTypes
                .Where(r => r.AssetTypeId == assetTypeId)
                .FirstOrDefault(r => r.SettingTypeId == settingTypeId);
        }

        public AssetTypeSettingType GetActive(int assetTypeId, int settingTypeId)
        {
            return FinancialDbContext.AssetTypesSettingTypes
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == assetTypeId)
                .FirstOrDefault(r => r.SettingTypeId == settingTypeId);
        }

        public IEnumerable<AssetTypeSettingType> GetAllForAssetType(int assetTypeId)
        {
            return FinancialDbContext.AssetTypesSettingTypes
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();
        }

        public IEnumerable<AssetTypeSettingType> GetAllForSettingType(int settingTypeId)
        {
            return FinancialDbContext.AssetTypesSettingTypes
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();
        }

        public IEnumerable<AssetTypeSettingType> GetAllActiveForAssetType(int assetTypeId)
        {
            return FinancialDbContext.AssetTypesSettingTypes
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();
        }

        public IEnumerable<AssetTypeSettingType> GetAllActiveForSettingType(int settingTypeId)
        {
            return FinancialDbContext.AssetTypesSettingTypes
                .Where(r => r.IsActive)
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();
        }
    }
}
