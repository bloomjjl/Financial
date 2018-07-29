using Financial.Business.ServiceInterfaces;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financial.Business.Models;

namespace Financial.Business.Services
{
    public class SettingTypeService : ISettingTypeService
    {
        private IUnitOfWork _unitOfWork;

        public SettingTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public SettingType GetSettingType(int settingTypeId)
        {
            var dtoSettingType = _unitOfWork.SettingTypes.Get(settingTypeId);
            if (dtoSettingType == null)
            {
                return null;
            }
            return new SettingType(dtoSettingType);
        }

        public List<SettingType> GetListOfSettingTypes()
        {
            // get all active setting types from db
            return _unitOfWork.SettingTypes.GetAllActive()
                .Select(r => new SettingType(r))
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmSettingType"></param>
        /// <returns>
        /// positive integer = record added.
        /// zero integer = name already exists.
        /// </returns>
        public int AddSettingType(SettingType bmSettingType)
        {
            // check for existing name
            var exists = _unitOfWork.SettingTypes.GetAllActive()
                .Any(r => r.Name == bmSettingType.SettingTypeName);
            if (exists)
            {
                return 0;
            }

            // transfer bm to dto
            var dtoSettingType = new Core.Models.SettingType()
            {
                Name = bmSettingType.SettingTypeName,
                IsActive = true,
            };

            // update db
            _unitOfWork.SettingTypes.Add(dtoSettingType);
            _unitOfWork.CommitTrans();

            // return new ID
            return dtoSettingType.Id;
        }

        public bool EditSettingType(SettingType bmSettingType)
        {
            // get dto
            var dtoSettingType = _unitOfWork.SettingTypes.Get(bmSettingType.SettingTypeId);
            if (dtoSettingType == null)
            {
                return false;
            }

            // transfer bm to dto
            dtoSettingType.Name = bmSettingType.SettingTypeName;

            // update db
            _unitOfWork.CommitTrans();

            return true;
        }


    }
}
