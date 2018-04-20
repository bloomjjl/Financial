using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Repositories
{
    public class InMemorySettingTypeRepository : InMemoryRepository<SettingType>, ISettingTypeRepository
    {
        private List<SettingType> _entities = null;

        public InMemorySettingTypeRepository(IEnumerable<SettingType> entities)
            : base(entities)
        {
            _entities = entities as List<SettingType>;
        }
    }
}
