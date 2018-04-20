using Financial.Core;
using Financial.Core.Models;
using Financial.Data;
using Financial.WebApplication.Models.ViewModels.AssetTransaction;
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
            List<AssetTransaction> dbAssetTransactions = GetAssetTransactionsOrderedByDueDate();

            // set initial value for total
            var total = 0.00M;

            // transfer dto to vm
            var index = 0;
            foreach (var dtoAssetTransaction in dbAssetTransactions)
            {
                Asset dtoAsset = GetAsset(dtoAssetTransaction.AssetId);

                // determine amount displayed
                var dtoTransactionType = UOW.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
                total = UpdateAssetTransactionTotal(total, dtoAssetTransaction.Amount, dtoTransactionType.Name);

                // add additional identifying info to asset title
                var assetNameAdditionalInformation = GetAccountNameAdditionalInformation(dtoAsset);

                // format clear date
                string clearDate = FormatDateToString(dtoAssetTransaction.ClearDate);

                // transfer to vm
                vmIndex.Add(new IndexViewModel(index, dtoAssetTransaction, clearDate, dtoAsset, assetNameAdditionalInformation, dtoTransactionType.Name, total));

                // increment index for sorting in view
                index++;
            }

            // display view
            return View("Index", vmIndex.OrderByDescending(r => r.Index));
        }

        private string FormatDateToString(DateTime date)
        {
            var formatedDate = string.Empty;

            if (date > new DateTime(0001, 1, 1))
            {
                formatedDate = date.ToString("MM/dd/yyyy");
            }

            return formatedDate;
        }

        private decimal UpdateAssetTransactionTotal(decimal initialBalance, decimal transactionAmount, string transactionType)
        { 
                if (transactionType == "Income")
                {
                    return initialBalance + transactionAmount;
                }
                else if (transactionType == "Expense")
                {
                    return initialBalance - transactionAmount;
                }

            return initialBalance;
        }

        private Asset GetAsset(int assetId)
        {
            return UOW.Assets.GetAll()
                .Where(r => r.IsActive)
                .FirstOrDefault(r => r.Id == assetId);
        }

        private List<AssetTransaction> GetAssetTransactionsOrderedByDueDate()
        {
            return UOW.AssetTransactions.GetAll()
                .Where(r => r.IsActive)
                .OrderBy(r => r.DueDate)
                .ToList();
        }

        private List<AssetTransaction> GetAssetTransactionsReverseOrderedByDueDate(int assetId)
        {
            return UOW.AssetTransactions.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetId == assetId)
                .OrderByDescending(r => r.DueDate)
                .ToList();
        }

        [HttpGet]
        public ActionResult DisplayForAsset(int assetId)
        {
            // transfer assetId from db
            var dbAssetTransactions = GetAssetTransactionsReverseOrderedByDueDate(assetId);

            // transfer dto to vm
            var vmDisplayForAsset = new List<DisplayForAssetViewModel>();
            foreach(var dtoAssetTransaction in dbAssetTransactions)
            {
                var dtoTransactionCategory = UOW.TransactionCategories.Get(dtoAssetTransaction.TransactionCategoryId);

                // update amount for expense to "-0.00"
                if(dtoAssetTransaction.TransactionTypeId == 1)
                {
                    dtoAssetTransaction.Amount = dtoAssetTransaction.Amount * -1;
                }

                // format clear date
                string clearDate = FormatDateToString(dtoAssetTransaction.ClearDate);

                // transfer to vm
                vmDisplayForAsset.Add(new DisplayForAssetViewModel(dtoAssetTransaction, clearDate, dtoTransactionCategory));
            }

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

            // add additional identifying info to asset title
            var assetNameAdditionalInformaiton = GetAccountNameAdditionalInformation(dtoAsset);

            // transfer dto to sli
            var sliTransactionTypes = GetSelectListOfTransactionTypes(null);
            var sliTransactionCategories = GetSelectListOfTransactionCategories(null);

            // display view
            return View("Create", new CreateViewModel(dtoAsset, assetNameAdditionalInformaiton,  dtoAssetType, DateTime.Now, sliTransactionTypes, sliTransactionCategories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // transfer vm to dto
            AddAssetTransaction(vmCreate);

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

        private void AddAssetTransaction(CreateViewModel vmCreate)
        {
            UOW.AssetTransactions.Add(new AssetTransaction()
            {
                AssetId = vmCreate.AssetId,
                TransactionTypeId = GetIntegerFromString(vmCreate.SelectedTransactionTypeId),
                TransactionCategoryId = GetIntegerFromString(vmCreate.SelectedTransactionCategoryId),
                CheckNumber = vmCreate.CheckNumber,
                DueDate = Convert.ToDateTime(vmCreate.DueDate),
                ClearDate = Convert.ToDateTime(vmCreate.ClearDate),
                Amount = vmCreate.Amount,
                Note = vmCreate.Note,
                IsActive = true
            });
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
            if (id == 0)
            {
                RedirectToAction("SelectAssetToCreate", "AssetTransaction");
            }

            // display view
            return RedirectToAction("Create", "AssetTransaction", new { assetId = id });
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
            UpdateAssetTransaction(vmEdit);

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Record updated";
            return RedirectToAction("Details", "Asset", new { id = vmEdit.AssetId });
        }

        private void UpdateAssetTransaction(EditViewModel vmEdit)
        {
            var dtoAssetTransaction = UOW.AssetTransactions.Get(vmEdit.Id);
            dtoAssetTransaction.TransactionTypeId = GetIntegerFromString(vmEdit.SelectedTransactionTypeId);
            dtoAssetTransaction.TransactionCategoryId = GetIntegerFromString(vmEdit.SelectedTransactionCategoryId);
            dtoAssetTransaction.CheckNumber = vmEdit.CheckNumber;
            dtoAssetTransaction.DueDate = Convert.ToDateTime(vmEdit.DueDate);
            dtoAssetTransaction.ClearDate = Convert.ToDateTime(vmEdit.ClearDate);
            dtoAssetTransaction.Amount = vmEdit.Amount;
            dtoAssetTransaction.Note = vmEdit.Note;
        }

        [HttpGet]
        public ActionResult EditAsset(int id, int assetId)
        {
            // transfer id to dto
            var dtoAssetTransaction = UOW.AssetTransactions.Get(id);
            var dtoAsset = UOW.Assets.Get(assetId);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);
            var dtoTransactionType = UOW.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
            var dtoTransactionCategory = UOW.TransactionCategories.Get(dtoAssetTransaction.TransactionCategoryId);

            // validate values to display
            dtoAssetTransaction.CheckNumber = FormatCheckNumber(dtoAssetTransaction.CheckNumber);
            dtoAssetTransaction.Note = FormatNote(dtoAssetTransaction.Note);

            // transfer dto to sli
            var sliAssets = GetSelectListOfAssets(dtoAsset.Id.ToString());

            // transfer to vm and display view
            return View("EditAsset", new EditAssetViewModel(dtoAssetTransaction, sliAssets, dtoAsset.Id.ToString(), dtoAssetType, dtoTransactionType.Name, dtoTransactionCategory.Name));
        }

        private string FormatCheckNumber(string checkNumber)
        {
            if (string.IsNullOrEmpty(checkNumber))
            {
                return string.Empty;
            }

            return checkNumber;
        }

        private string FormatNote(string note)
        {
            if (string.IsNullOrEmpty(note))
            {
                return string.Empty;
            }

            return note;
        }

        [HttpPost]
        public ActionResult EditAsset(EditAssetViewModel vmEditAsset)
        {
            if(!ModelState.IsValid)
            {
                return View("EditAsset", vmEditAsset);
            }

            // transfer vm to dto
            var dtoAssetTransaction = UOW.AssetTransactions.Get(vmEditAsset.Id);
            dtoAssetTransaction.AssetId = GetIntegerFromString(vmEditAsset.SelectedAssetId);

            // update db
            UOW.CommitTrans();

            // display view
            TempData["SuccessMessage"] = "Asset Name updated";
            return RedirectToAction("Edit", "AssetTransaction", new { id = vmEditAsset.Id });
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
            // transfer values from db
            var dbAssets = UOW.Assets.GetAll()
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToList();

            // transfer dto to sli
            var sliAssets = new List<SelectListItem>();
            foreach(var dtoAsset in dbAssets)
            {
                // add credit card account number to name
                var assetName = dtoAsset.Name;
                var assetNameInformation = GetAccountNameAdditionalInformation(dtoAsset);

                sliAssets.Add(new SelectListItem()
                {
                    Value = dtoAsset.Id.ToString(),
                    Selected = dtoAsset.Id.ToString() == selectedValue,
                    Text = assetName + assetNameInformation
                });
            }
            /*
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
            */
            return sliAssets;
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
                        .OrderBy(r => r.Text)
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