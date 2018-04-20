using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAssetSettingService : IDisposable
    {
        string GetAccountIdentificationInformation(Core.Models.Asset dtoAsset);
    }
}
