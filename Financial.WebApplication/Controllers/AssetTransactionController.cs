using Financial.Core;
using Financial.Core.ViewModels.AssetTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTransactionController : BaseController
    {
        public AssetTransactionController()
            : base()
        {
        }

        public AssetTransactionController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public ActionResult Index(int assetId)
        {
            // tranfer dto to vm
            var vmIndex = UOW.AssetTransactions.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .Select(r => new IndexViewModel(r))
                .ToList();

            // display view
            return PartialView("_Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create(int assetId)
        {
            // transfer dto to vm
            var dtoAsset = UOW.Assets.Get(assetId);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // transfer dto to sli
            var sliTransactionTypes = GetSelectListOfTransactionTypes(null);
            var sliTransactionCategories = GetSelectListOfTransactionCategories(null);
            var sliTransactionDescriptions = GetSelectListOfTransactionDescriptions(null);

            // display view
            return View("Create", new CreateViewModel(dtoAsset, dtoAssetType, sliTransactionTypes, sliTransactionCategories, sliTransactionDescriptions));
        }

        private List<SelectListItem> GetSelectListOfTransactionDescriptions(string selectedValue)
        {
            return UOW.TransactionDescriptions.GetAll()
                            .Where(r => r.IsActive)
                            .Select(r => new SelectListItem()
                            {
                                Value = r.Id.ToString(),
                                Selected = r.Id.ToString() == selectedValue,
                                Text = r.Name
                            })
                            .ToList();
        }

        private List<SelectListItem> GetSelectListOfTransactionCategories(string selectedValue)
        {
            return UOW.TransactionCategories.GetAll()
                            .Where(r => r.IsActive)
                            .Select(r => new SelectListItem()
                            {
                                Value = r.Id.ToString(),
                                Selected = r.Id.ToString() == selectedValue,
                                Text = r.Name
                            })
                            .ToList();
        }

        private List<SelectListItem> GetSelectListOfTransactionTypes(string selectedValue)
        {
            return UOW.TransactionTypes.GetAll()
                            .Where(r => r.IsActive)
                            .Select(r => new SelectListItem()
                            {
                                Value = r.Id.ToString(),
                                Selected = r.Id.ToString() == selectedValue,
                                Text = r.Name
                            })
                            .ToList();
        }
    }
}