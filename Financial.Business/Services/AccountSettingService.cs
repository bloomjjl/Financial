using Financial.Business.ServiceInterfaces;
using Financial.Business.Models;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Core.Models;
using AssetType = Financial.Business.Models.AccountType;
using SettingType = Financial.Core.Models.SettingType;

namespace Financial.Business.Services
{
    public class AccountSettingService : IAccountSettingService
    {
        private IUnitOfWork _unitOfWork;

        public AccountSettingService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AccountSettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public string GetAccountIdentificationInformation(Account bmAccount)
        {
            throw new NotImplementedException();
        }
    }
}
