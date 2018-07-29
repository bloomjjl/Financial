using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface IAssetTypeRepository : IRepository<AssetType>
    {
        AssetType GetActive(int id);
        IEnumerable<AssetType> GetAllOrderedByName();
        IEnumerable<AssetType> GetAllActiveOrderedByName();
        int CountMatching(string name);
        int CountMatching(int excludeId, string name);
    }
}
