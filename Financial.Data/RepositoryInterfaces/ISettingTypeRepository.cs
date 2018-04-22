using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface ISettingTypeRepository : IRepository<SettingType>
    {
        SettingType GetActive(int id);
        IEnumerable<SettingType> GetAllOrderedByName();
        int CountMatching(string name);
        int CountMatching(int excludeId, string name);
    }
}
