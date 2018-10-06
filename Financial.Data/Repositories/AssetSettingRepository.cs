using Financial.Core;
using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.Repositories
{
    public class AssetSettingRepository : Repository<AssetSetting>, IAssetSettingRepository
    {
        public AssetSettingRepository(FinancialDbContext context)
            : base(context)
        {
        }

        private FinancialDbContext FinancialDbContext
        {
            get { return _context as FinancialDbContext; }
        }


        public AssetSetting GetActive(int assetId, int settingTypeId)
        {
            return FinancialDbContext.AssetSettings
                .Include(r => r.Asset)
                .Include(r => r.Asset.AssetType)
                .Include(r => r.SettingType)
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .FirstOrDefault(r => r.SettingTypeId == settingTypeId);
        }

        public IEnumerable<AssetSetting> GetAllActiveForAsset(int assetId)
        {
            return FinancialDbContext.AssetSettings
                .Include(r => r.Asset)
                .Include(r => r.SettingType)
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .ToList();
        }

        public IEnumerable<AssetSetting> GetAllActiveForSettingType(int settingTypeId)
        {
            return FinancialDbContext.AssetSettings
                .Include(r => r.Asset)
                .Include(r => r.SettingType)
                .Where(r => r.IsActive)
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();
        }

    }
}
