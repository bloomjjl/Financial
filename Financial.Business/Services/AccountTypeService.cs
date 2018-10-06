using Financial.Business.ServiceInterfaces;
using Financial.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Financial.Business.Services
{
    public class AccountTypeService : IAccountTypeService
    {
        private IUnitOfWork _unitOfWork;

        public AccountTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Business.Models.AccountType GetAssetType(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AccountType(dtoAssetType);
        }


        public List<Business.Models.AccountType> IndexGetModelList()
        {
            return _unitOfWork.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .Select(r => new Business.Models.AccountType(r))
                .ToList();
        }
        public int CreatePostUpdateDatabase(Business.Models.AccountType bmAssetType)
        {
            // check for existing name
            var exists = _unitOfWork.AssetTypes.GetAllActive()
                .Any(r => r.Name == bmAssetType.AssetTypeName);
            if (exists)
            {
                return 0;
            }

            // transfer bm to dto
            var dtoAssetType = new Core.Models.AssetType()
            {
                Name = bmAssetType.AssetTypeName,
                IsActive = true,
            };

            // update db
            _unitOfWork.AssetTypes.Add(dtoAssetType);
            _unitOfWork.CommitTrans();

            // return new ID
            return dtoAssetType.Id;
        }
        public Business.Models.AccountType EditGetModel(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.IsActive && r.Id == assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AccountType(dtoAssetType);
        }
        public string EditPostUpdateDatabase(Business.Models.AccountType bmAssetType)
        {
            // get dto
            var dtoAssetType = _unitOfWork.AssetTypes.Get(bmAssetType.AssetTypeId);
            if (dtoAssetType == null)
            {
                return "Invalid Asset Type";
            }

            // transfer bm to dto
            dtoAssetType.Name = bmAssetType.AssetTypeName;

            // update db
            _unitOfWork.CommitTrans();

            return "Success";
        }
        public Business.Models.AccountType DetailsGetModel(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.IsActive && r.Id == assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AccountType(dtoAssetType);
        }









        public List<SelectListItem> GetAssetTypesDropDownList(int? selectedId)
        {
            return _unitOfWork.AssetTypes.FindAll(r => r.IsActive)
                .Select(r => new SelectListItem()
                {
                    Value = r.Id.ToString(),
                    Selected = r.Id == selectedId,
                    Text = r.Name
                })
                .ToList();
        }




       


 

    }
}
