using Financial.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business
{
    public interface IBusinessService : IDisposable
    {
        IAssetSettingService AssetSettingService { get; }
        IAssetTypeService AssetTypeService { get; }
    }
}
