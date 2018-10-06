using Financial.Business.Models;
using Financial.Business.ServiceInterfaces;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Business.Services
{
    public class AccountTypeSettingTypeService : IAccountTypeSettingTypeService
    {
        private IUnitOfWork _unitOfWork;

        public AccountTypeSettingTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public List<Business.Models.AttributeType> GetListOfActiveLinkedSettingTypes(int assetTypeId)
        {
            return _unitOfWork.SettingTypes.GetAll()// your starting point - table in the "from" statement
                .Where(r => r.IsActive)
                .ToList()
                .Join(_unitOfWork.AssetTypeSettingTypes.FindAll(r => r.IsActive), // the source table of the inner join
                st => st.Id, // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                atst => atst.Id, // Select the foreign key (the second part of the "on" clause)
                (st, atst) => new { SType = st, ATypeSType = atst }) // selection
                .Where(link => link.ATypeSType.AssetTypeId == assetTypeId)
                .Select(j => new Business.Models.AttributeType(j.SType, j.ATypeSType))
                .ToList();
        }

        public List<Business.Models.AccountType> GetListOfActiveLinkedAssetTypes(int settingTypeId)
        {
            return _unitOfWork.AssetTypes.GetAll()// your starting point - table in the "from" statement
                .Where(r => r.IsActive)
                .ToList()
                .Join(_unitOfWork.AssetTypeSettingTypes.FindAll(r => r.IsActive), // the source table of the inner join
                at => at.Id, // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                atst => atst.Id, // Select the foreign key (the second part of the "on" clause)
                (at, atst) => new { AType = at, ATypeSType = atst }) // selection
                .Where(link => link.ATypeSType.SettingTypeId == settingTypeId)
                .Select(j => new Business.Models.AccountType(j.AType, j.ATypeSType))
                .ToList();
        }

        public Business.Models.AccountType CreateLinkedSettingTypesGetModel(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.IsActive && r.Id == assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AccountType(dtoAssetType);
        }
        public Business.Models.AccountType EditLinkedSettingTypesGetModel(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.IsActive && r.Id == assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AccountType(dtoAssetType);
        }




        public int GetAssetTypeSettingTypeIdForLinkedAssetType(int assetTypeId, int settingTypeId)
        {
            // get link information
            var dbAssetTypeAttributeTypes = _unitOfWork.AssetTypeSettingTypes.GetAllActive()
                .Where(r => r.AssetTypeId == assetTypeId)
                .FirstOrDefault(r => r.SettingTypeId == settingTypeId);

            // validate
            if (dbAssetTypeAttributeTypes == null)
            {
                return 0;
            }

            return dbAssetTypeAttributeTypes.Id;
        }


        public List<AccountType> GetListOfLinkedAssetTypes(int settingTypeId)
        {
            // get linked setting types from db
            var dbAssetTypeAttributeTypes = _unitOfWork.AssetTypeSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();

            // transfer dto to bm
            var bmAssetTypes = new List<AccountType>();
            foreach (var dtoAssetTypeSettingType in dbAssetTypeAttributeTypes)
            {
                var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAssetTypeSettingType.AssetTypeId);
                if (dtoAssetType != null)
                {
                    bmAssetTypes.Add(new AccountType(dtoAssetType, dtoAssetTypeSettingType));
                }
            }

            return bmAssetTypes;
        }

        public List<AttributeType> GetListOfLinkedSettingTypes(int assetTypeId)
        {
            // get linked setting types from db
            var dbAssetTypeSettingTypes = _unitOfWork.AssetTypeSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();

            // transfer dto to bm
            var bmSettingTypes = new List<AttributeType>();
            foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
            {
                var dtoSettingType = _unitOfWork.SettingTypes.Get(dtoAssetTypeSettingType.SettingTypeId);
                if (dtoSettingType != null)
                {
                    bmSettingTypes.Add(new AttributeType(dtoSettingType, dtoAssetTypeSettingType));
                }
            }

            return bmSettingTypes;
        }

        public List<AttributeType> GetListOfSettingTypesWithLinkedAssetType(int assetTypeId)
        {
            var bmSettingTypes = new List<AttributeType>();

            // transfer from db
            var dbSettingTypes = _unitOfWork.SettingTypes.GetAllActive();

            foreach (var dtoSettingType in dbSettingTypes)
            {
                // check for existing link
                var dtoAssetTypeSettingType = _unitOfWork.AssetTypeSettingTypes.GetAllActive()
                    .Where(r => r.AssetTypeId == assetTypeId)
                    .FirstOrDefault(r => r.SettingTypeId == dtoSettingType.Id);
                if(dtoAssetTypeSettingType == null)
                {
                    // no link found
                    dtoAssetTypeSettingType = new Core.Models.AssetTypeSettingType();
                }

                // transfer dto to bm
                bmSettingTypes.Add(new AttributeType(dtoSettingType, dtoAssetTypeSettingType));
            }

            return bmSettingTypes;
        }


    }
}
