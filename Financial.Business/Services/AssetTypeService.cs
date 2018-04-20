using Financial.Business.ServiceInterfaces;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Financial.Business.Services
{
    public class AssetTypeService : IAssetTypeService
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
