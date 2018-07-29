using Financial.Business.ServiceInterfaces;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Services
{
    public class AssetSettingService : IAssetSettingService
    {
        private IUnitOfWork _unitOfWork;

        public AssetSettingService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetSettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public string GetAccountIdentificationInformation(Core.Models.Asset dtoAsset)
        {
            var additionalInformation = string.Empty;

            // (Credit Card)
            if (dtoAsset.AssetTypeId == 3)
            {
                var dtoAssetSetting = _unitOfWork.AssetSettings.GetAll()
                    .Where(r => r.IsActive)
                    .FirstOrDefault(r => r.AssetId == dtoAsset.Id);

                // (Credit Card)
                additionalInformation = string.Format(" ({0})", dtoAssetSetting.Value);
            }

            return additionalInformation;
        }

    }
}
