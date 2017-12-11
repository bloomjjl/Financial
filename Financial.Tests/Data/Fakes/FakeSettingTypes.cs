using Financial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Tests.Data.Fakes
{
    public class FakeSettingTypes
    {
        public static IEnumerable<SettingType> InitialFakeSettingTypes()
        {
            yield return new SettingType() { Id = 1, Name = "SettingTypeName1", IsActive = true };
            yield return new SettingType() { Id = 2, Name = "SettingTypeName2", IsActive = true };
            yield return new SettingType() { Id = 3, Name = "SettingTypeName3", IsActive = false };
            yield return new SettingType() { Id = 4, Name = "SettingTypeName4", IsActive = true };
            yield return new SettingType() { Id = 5, Name = "SettingTypeName5", IsActive = true };
        }
    }
}
