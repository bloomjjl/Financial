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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // transfer vm to dto
            UOW.AssetTransactions.Add(new Core.Models.AssetTransaction() {
                AssetId = vmCreate.AssetId,
                TransactionTypeId = GetIntegerFromString(vmCreate.SelectedTransactionTypeId),
                TransactionCategoryId = GetIntegerFromString(vmCreate.SelectedTransactionCategoryId),
                TransactionDescriptionId = GetIntegerFromString(vmCreate.SelectedTransactionDescriptionId),
                CheckNumber = vmCreate.CheckNumber,
                TransactionDate = vmCreate.Date,
                Amount = vmCreate.Amount,
                Note = vmCreate.Note,
                IsActive = true
            });

            return RedirectToAction("Create", new { assetId = vmCreate.AssetId });

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record created";
            return RedirectToAction("Details", "Asset", new { Id = vmCreate.AssetId });
        }

        [HttpGet]
        //public List<SelectListItem> AddMultipleSelectedIndex(List<SelectListItem> sliProvided, string selectedValue)
        public string AddMultipleSelectedIndex(string selectedValues)
        {
            /*
            for (int i = 0; i < sliProvided.Count; i++)
            {
                if(sliProvided[i].Value == selectedValue)
                {
                    sliProvided[i].Selected = true;
                }
            }

            return sliProvided;
            */
            //selectedValues.Add(addValue);

            return selectedValues;
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