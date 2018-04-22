using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface IAssetTransactionRepository : IRepository<AssetTransaction>
    {
        IEnumerable<AssetTransaction> GetAllActiveByDueDate();
        IEnumerable<AssetTransaction> GetAllActiveByDescendingDueDate(int assetId);
    }
}
