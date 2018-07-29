using Financial.Core.Models;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.WebApplication.Tests.Fakes.Repositories
{
    public class InMemorySettingTypeRepository : InMemoryRepository<SettingType>, ISettingTypeRepository
    {
        private List<SettingType> _entities = null;

        public InMemorySettingTypeRepository(IEnumerable<SettingType> entities)
            : base(entities)
        {
            _entities = entities as List<SettingType>;
        }



        public IEnumerable<SettingType> GetAllOrderedByName()
        {
            return _entities
                .OrderBy(r => r.Name)
                .ToList();
        }

        public int CountMatching(string name)
        {
            return _entities
                .Count(r => r.Name == name);
        }

        public int CountMatching(int excludeId, string name)
        {
            return _entities
                .Where(r => r.Id != excludeId)
                .Count(r => r.Name == name);
        }
    }
}
