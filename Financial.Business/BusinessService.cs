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
        private IUnitOfWork _unitOfWork;

        public BusinessService()
        {
            _unitOfWork = new UnitOfWork();
            SetServices();
        }

        public BusinessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            SetServices();
        }

        private void SetServices()
        {
            AssetService = new AssetService(_unitOfWork);
            AssetSettingService = new AssetSettingService(_unitOfWork);
            AssetTypeService = new AssetTypeService(_unitOfWork);
            SelectListService = new SelectListService(_unitOfWork);
        }


        public IAssetService AssetService { get; private set; }
        public IAssetSettingService AssetSettingService { get; private set; }
        public IAssetTypeService AssetTypeService { get; private set; }
        public ISelectListService SelectListService { get; private set; }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
