using Financial.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business
{
    public interface IBusinessService
    {
        IAccountService AccountService { get; }
        IAccountSettingService AccountSettingService { get; }
        IAccountTransactionService AccountTransactionService { get; }
        IAccountTypeService AccountTypeService { get; }
        IAccountTypeSettingTypeService AccountTypeSettingTypeService { get; }
        ISettingTypeService SettingTypeService { get; }
    }
}
