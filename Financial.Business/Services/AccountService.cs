
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Financial.Business.Models;
using Financial.Business.ServiceInterfaces;
using Financial.Business.Utilities;
using Financial.Core.Models;
using Financial.Data;

namespace Financial.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountSettingService _assetSettingService;

        public AccountService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AccountService(
            IUnitOfWork unitOfWork, 
            IAccountSettingService assetSettingService)
        {
            _unitOfWork = unitOfWork;
            _assetSettingService = assetSettingService;
        }




        public List<Account> GetListOfAccounts()
        {
            // create list object to return
            var bmAccountList = new List<Account>();

            // get assets from db
            var dbAssetList = _unitOfWork.Assets.GetAllActiveOrderedByName();
            foreach (var dbAsset in dbAssetList)
            {
                // get asset type from db
                var dbAssetType = _unitOfWork.AssetTypes.Get(dbAsset.AssetTypeId);
                if (dbAssetType == null)
                    return new List<Account>();

                // add additional information to asset name
                if (dbAssetType.Id == AssetType.IdForCreditCard)
                {
                    var dbAssetSetting = _unitOfWork.AssetSettings.GetActive(dbAsset.Id, Core.Models.SettingType.IdForAccountNumber);
                    if(dbAssetSetting != null)
                        dbAsset.Name = AccountUtility.FormatAccountName(dbAsset.Name, dbAssetType.Id, dbAssetSetting.Value);
                }

                // transfer dto to bm
                bmAccountList.Add(new Account(dbAsset, dbAssetType));
            }

            return bmAccountList;
        }


        public string GetAccountIdentificationInformation(Account bmAccount)
        {
            if (bmAccount == null)
                return string.Empty;

            if (bmAccount.AssetTypeId == AssetType.IdForCreditCard)
            {
                var dtoAssetSetting = _unitOfWork.AssetSettings.GetActive(bmAccount.AssetId, Core.Models.SettingType.IdForAccountNumber);
                if (dtoAssetSetting == null)
                    return string.Empty;

                return $" ({dtoAssetSetting.Value})";
            }

            return string.Empty;
        }

        public List<SelectListItem> GetSelectListOfAccounts(int? selectedId = null)
        {
            // transfer values from db
            var dbAssets = _unitOfWork.Assets.GetAllActiveOrderedByName();

            // transfer dto to sli
            var sliAssets = new List<SelectListItem>();
            foreach (var dtoAsset in dbAssets)
            {
                // add credit card account number to name
                var assetName = dtoAsset.Name;
                var assetNameInformation = _assetSettingService.GetAccountIdentificationInformation(new Account(dtoAsset));

                sliAssets.Add(new SelectListItem()
                {
                    Value = dtoAsset.Id.ToString(),
                    Text = string.Format("{0}{1}", assetName, assetNameInformation),
                    Selected = dtoAsset.Id.ToString() == selectedId.ToString(),
                });
            }
            return sliAssets;
        }


    }
}
