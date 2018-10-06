using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;
using Financial.Core.Models;

namespace Financial.Business.ServiceInterfaces
{
    public interface IAccountSettingService
    {
        string GetAccountIdentificationInformation(Account bmAccount);
    }
}
