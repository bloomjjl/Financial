using Financial.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.ServiceInterfaces
{
    public interface ISettingTypeService
    {
        SettingType GetSettingType(int settingTypeId);
        List<SettingType> GetListOfSettingTypes();
        int AddSettingType(SettingType bmSettingType);
        bool EditSettingType(SettingType bmSettingType);

    }
}
