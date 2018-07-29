using Financial.Business.ServiceInterfaces;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Financial.Business.Models;

namespace Financial.Business.Services
{
    public class AssetService : IAssetService
    {
        private IUnitOfWork _unitOfWork;

        public AssetService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public List<SelectListItem> GetSelectListOfAssets(string selectedId)
        {
            // transfer values from db
            var dbAssets = _unitOfWork.Assets.GetAllActiveOrderedByName();

            // transfer dto to sli
            var sliAssets = new List<SelectListItem>();
            foreach (var dtoAsset in dbAssets)
            {
                // add credit card account number to name
                var assetName = dtoAsset.Name;
                var svcAssetSetting = new AssetSettingService(_unitOfWork); 
                var assetNameInformation = svcAssetSetting.GetAccountIdentificationInformation(dtoAsset);

                sliAssets.Add(new SelectListItem()
                {
                    Value = dtoAsset.Id.ToString(),
                    Selected = dtoAsset.Id.ToString() == selectedId,
                    Text = string.Format("{0}{1}", assetName, assetNameInformation)
                });
            }
            return sliAssets;
        }


    }
}
