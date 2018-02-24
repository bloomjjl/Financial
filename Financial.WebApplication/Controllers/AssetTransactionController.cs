using Financial.Core;
using Financial.Core.ViewModels.AssetTransaction;
using Newtonsoft.Json;
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
        public ActionResult Index()
        {
            // create vm
            var vmIndex = new List<IndexViewModel>();

            // get active transactions
            var dbAssetTransactions = UOW.AssetTransactions.GetAll()
                .Where(r => r.IsActive)
                .ToList();

            // set initial value for total
            var total = 0.00M;

            // transfer dto to vm
            foreach (var dtoAssetTransaction in dbAssetTransactions)
            {
                var dtoAsset = UOW.Assets.GetAll()
                    .Where(r => r.IsActive)
                    .FirstOrDefault(r => r.Id == dtoAssetTransaction.AssetId);
                var dtoTransactionType = UOW.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
                var transactionType = string.Empty;
                if (dtoTransactionType.Name == "Income")
                {
                    transactionType = "Income";
                    total = total + dtoAssetTransaction.Amount;
                }
                else if (dtoTransactionType.Name == "Expense")
                {
                    transactionType = "Expense";
                    total = total - dtoAssetTransaction.Amount;
                }

                vmIndex.Add(new IndexViewModel(dtoAssetTransaction, dtoAsset, transactionType, total));
            }

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ActionResult DisplayForAsset(int assetId)
        {
            // tranfer dto to vm
            var vmDisplayForAsset = UOW.AssetTransactions.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .Select(r => new DisplayForAssetViewModel(r))
                .ToList();

            // display view
            return PartialView("_DisplayForAsset", vmDisplayForAsset);
        }

        [HttpGet]
        public ActionResult Create(int? assetId)
        {
            if(assetId == null || assetId <= 0)
            {
                return RedirectToAction("SelectAssetToCreate", "AssetTransaction");
            }

            // transfer id to dto 
            var dtoAsset = UOW.Assets.Get(GetIntegerFromString(assetId.ToString()));
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // transfer dto to sli
            var sliTransactionTypes = GetSelectListOfTransactionTypes(null);
            var sliTransactionCategories = GetSelectListOfTransactionCategories(null);
            var sliTransactionDescriptions = GetSelectListOfTransactionDescriptions(null);

            // display view
            return View("Create", new CreateViewModel(dtoAsset, dtoAssetType, DateTime.Now, sliTransactionTypes, sliTransactionCategories, sliTransactionDescriptions));
        }

        [HttpGet]
        public ActionResult SelectAssetToCreate()
        {
            // transfer dto to sli
            var sliAssets = GetSelectListOfAssets(null);

            // display view
            return View("SelectAssetToCreate", new SelectAssetToCreateViewModel(sliAssets));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectAssetToCreate(SelectAssetToCreateViewModel vmSelectedAssetToCreate)
        {
            // get selected id as integer
            var id = GetIntegerFromString(vmSelectedAssetToCreate.SelectedAssetId);

            // validate selected id
            if(id == 0)
            {
                RedirectToAction("SelectAssetToCreate", "AssetTransaction");
            }

            // display view
            return RedirectToAction("Create", "AssetTransaction", new { assetId = id });
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
                DueDate = Convert.ToDateTime(vmCreate.DueDate),
                Amount = vmCreate.Amount,
                Note = vmCreate.Note,
                IsActive = true
            });

            /*
            var selectedItems = JsonConvert.DeserializeObject(vmCreate.TransactionCategoriesSelected, typeof(List<int>));
            var blank1 = "";
            
            return RedirectToAction("Create", new { assetId = vmCreate.AssetId });
            */

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record created";
            return RedirectToAction("Details", "Asset", new { Id = vmCreate.AssetId });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // transfer id to dto 
            var dtoAssetTransaction = UOW.AssetTransactions.Get(id);
            var dtoAsset = UOW.Assets.Get(dtoAssetTransaction.AssetId);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // transfer dto to sli
            var sliTransactionTypes = GetSelectListOfTransactionTypes(dtoAssetTransaction.TransactionTypeId.ToString());
            var sliTransactionCategories = GetSelectListOfTransactionCategories(dtoAssetTransaction.TransactionCategoryId.ToString());

            // display view
            return View("Edit", new EditViewModel(dtoAssetTransaction, dtoAsset, dtoAssetType, sliTransactionTypes, sliTransactionCategories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // transfer vm to dto
            var dtoAssetTransaction = UOW.AssetTransactions.Get(vmEdit.Id);
            dtoAssetTransaction.TransactionTypeId = GetIntegerFromString(vmEdit.SelectedTransactionTypeId);
            dtoAssetTransaction.TransactionCategoryId = GetIntegerFromString(vmEdit.SelectedTransactionCategoryId);
            dtoAssetTransaction.CheckNumber = vmEdit.CheckNumber;
            dtoAssetTransaction.DueDate = Convert.ToDateTime(vmEdit.DueDate);
            dtoAssetTransaction.ClearDate = Convert.ToDateTime(vmEdit.ClearDate);
            dtoAssetTransaction.Amount = vmEdit.Amount;
            dtoAssetTransaction.Note = vmEdit.Note;

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated";
            return RedirectToAction("Details", "Asset", new { id = vmEdit.AssetId });
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

        private List<SelectListItem> GetSelectListOfAssets(string selectedValue)
        {
            return UOW.Assets.GetAll()
                        .Where(r => r.IsActive)
                        .Select(r => new SelectListItem()
                        {
                            Value = r.Id.ToString(),
                            Selected = r.Id.ToString() == selectedValue,
                            Text = r.Name
                        })
                        .OrderBy(sli => sli.Text)
                        .ToList();
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