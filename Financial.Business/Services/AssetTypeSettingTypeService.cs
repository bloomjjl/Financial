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
    public class AssetTypeSettingTypeService : IAssetTypeSettingTypeService
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeSettingTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public List<Business.Models.SettingType> GetListOfActiveLinkedSettingTypes(int assetTypeId)
        {
            return _unitOfWork.SettingTypes.GetAll()// your starting point - table in the "from" statement
                .Where(r => r.IsActive)
                .ToList()
                .Join(_unitOfWork.AssetTypeSettingTypes.FindAll(r => r.IsActive), // the source table of the inner join
                st => st.Id, // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                atst => atst.Id, // Select the foreign key (the second part of the "on" clause)
                (st, atst) => new { SType = st, ATypeSType = atst }) // selection
                .Where(link => link.ATypeSType.AssetTypeId == assetTypeId)
                .Select(j => new Business.Models.SettingType(j.SType, j.ATypeSType))
                .ToList();
        }

        public List<Business.Models.AssetType> GetListOfActiveLinkedAssetTypes(int settingTypeId)
        {
            return _unitOfWork.AssetTypes.GetAll()// your starting point - table in the "from" statement
                .Where(r => r.IsActive)
                .ToList()
                .Join(_unitOfWork.AssetTypeSettingTypes.FindAll(r => r.IsActive), // the source table of the inner join
                at => at.Id, // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                atst => atst.Id, // Select the foreign key (the second part of the "on" clause)
                (at, atst) => new { AType = at, ATypeSType = atst }) // selection
                .Where(link => link.ATypeSType.SettingTypeId == settingTypeId)
                .Select(j => new Business.Models.AssetType(j.AType, j.ATypeSType))
                .ToList();
        }

        public Business.Models.AssetType CreateLinkedSettingTypesGetModel(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.IsActive && r.Id == assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AssetType(dtoAssetType);
        }
        public Business.Models.AssetType EditLinkedSettingTypesGetModel(int assetTypeId)
        {
            var dtoAssetType = _unitOfWork.AssetTypes.Find(r => r.IsActive && r.Id == assetTypeId);
            if (dtoAssetType == null)
            {
                return null;
            }
            return new Business.Models.AssetType(dtoAssetType);
        }




        public int GetAssetTypeSettingTypeIdForLinkedAssetType(int assetTypeId, int settingTypeId)
        {
            // get link information
            var dtoAssetTypeSettingType = _unitOfWork.AssetTypeSettingTypes.GetAllActive()
                .Where(r => r.AssetTypeId == assetTypeId)
                .FirstOrDefault(r => r.SettingTypeId == settingTypeId);

            // validate
            if (dtoAssetTypeSettingType == null)
            {
                return 0;
            }

            return dtoAssetTypeSettingType.Id;
        }


        public List<AssetType> GetListOfLinkedAssetTypes(int settingTypeId)
        {
            // get linked setting types from db
            var dbAssetTypeSettingTypes = _unitOfWork.AssetTypeSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList();

            // transfer dto to bm
            var bmAssetTypes = new List<AssetType>();
            foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
            {
                var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAssetTypeSettingType.AssetTypeId);
                if (dtoAssetType != null)
                {
                    bmAssetTypes.Add(new AssetType(dtoAssetType, dtoAssetTypeSettingType));
                }
            }

            return bmAssetTypes;
        }

        public List<SettingType> GetListOfLinkedSettingTypes(int assetTypeId)
        {
            // get linked setting types from db
            var dbAssetTypeSettingTypes = _unitOfWork.AssetTypeSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList();

            // transfer dto to bm
            var bmSettingTypes = new List<SettingType>();
            foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
            {
                var dtoSettingType = _unitOfWork.SettingTypes.Get(dtoAssetTypeSettingType.SettingTypeId);
                if (dtoSettingType != null)
                {
                    bmSettingTypes.Add(new SettingType(dtoSettingType, dtoAssetTypeSettingType));
                }
            }

            return bmSettingTypes;
        }

        public List<SettingType> GetListOfSettingTypesWithLinkedAssetType(int assetTypeId)
        {
            var bmSettingTypes = new List<SettingType>();

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
                bmSettingTypes.Add(new SettingType(dtoSettingType, dtoAssetTypeSettingType));
            }

            return bmSettingTypes;
        }


    }
}
