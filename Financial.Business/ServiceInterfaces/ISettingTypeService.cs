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
        AttributeType GetSettingType(int settingTypeId);
        List<AttributeType> GetListOfSettingTypes();
        int AddSettingType(AttributeType bmSettingType);
        bool EditSettingType(AttributeType bmSettingType);

    }
}
