using Financial.Business.ServiceInterfaces;
using Financial.Business.Services;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business
{
    public class BusinessService : IBusinessService
    {
        public BusinessService(IUnitOfWork unitOfWork)
        {
            SetServices(unitOfWork);
        }

        private void SetServices(IUnitOfWork unitOfWork)
        {
            AccountSettingService = new AccountSettingService(unitOfWork);

            AccountService = new AccountService(unitOfWork, AccountSettingService);
            AccountTransactionService = new AccountTransactionService(unitOfWork);
            AccountTypeService = new AccountTypeService(unitOfWork);
            AccountTypeSettingTypeService = new AccountTypeSettingTypeService(unitOfWork);
            SettingTypeService = new SettingTypeService(unitOfWork);
        }


        public IAccountService AccountService { get; private set; }
        public IAccountSettingService AccountSettingService { get; private set; }
        public IAccountTransactionService AccountTransactionService { get; private set; }
        public IAccountTypeService AccountTypeService { get; private set; }
        public IAccountTypeSettingTypeService AccountTypeSettingTypeService { get; private set; }
        public ISettingTypeService SettingTypeService { get; private set; }

    }
}
