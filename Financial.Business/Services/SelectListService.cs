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
    public class SelectListService : ISelectListService
    {
        private IUnitOfWork _unitOfWork;

        public SelectListService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public SelectListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public List<SelectListItem> TransactionCategories(string selectedId)
        {
            return _unitOfWork.TransactionCategories.GetAll()
                        .Where(r => r.IsActive)
                        .Select(r => new SelectListItem()
                        {
                            Value = r.Id.ToString(),
                            Selected = r.Id.ToString() == selectedId,
                            Text = r.Name
                        })
                        .OrderBy(r => r.Text)
                        .ToList();
        }

        public List<SelectListItem> TransactionDescriptions(string selectedId)
        {
            return _unitOfWork.TransactionDescriptions.GetAll()
                        .Where(r => r.IsActive)
                        .Select(r => new SelectListItem()
                        {
                            Value = r.Id.ToString(),
                            Selected = r.Id.ToString() == selectedId,
                            Text = r.Name
                        })
                        .ToList();
        }

        public List<SelectListItem> TransactionTypes(string selectedId)
        {
            return _unitOfWork.TransactionTypes.GetAll()
                        .Where(r => r.IsActive)
                        .Select(r => new SelectListItem()
                        {
                            Value = r.Id.ToString(),
                            Selected = r.Id.ToString() == selectedId,
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
