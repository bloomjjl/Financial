using Financial.Business.Utilities;
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
            try
            {
                // create vm
                var vmIndex = new List<IndexViewModel>();

                // get active transactions
                List<AssetTransaction> dbAssetTransactions = UOW.AssetTransactions.GetAllActiveByDueDate().ToList();

                // set initial value for total
                var total = 0.00M;

                // transfer dto to vm
                var index = 0;
                foreach (var dtoAssetTransaction in dbAssetTransactions)
                {
                    var dtoAsset = UOW.Assets.GetActive(dtoAssetTransaction.AssetId);
                    var dtoTransactionType = UOW.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
                    if (dtoAsset != null && dtoTransactionType != null)
                    {
                        // determine amount displayed
                        total = TransactionUtility.CalculateTransaction(total, dtoAssetTransaction.Amount, dtoTransactionType.Name);

                        // add additional identifying info to asset title
                        var assetNameAdditionalInformation = BS.AssetSettingService.GetAccountIdentificationInformation(dtoAsset);

                        // format clear date
                        string clearDate = DataTypeUtility.GetDateValidatedToString(dtoAssetTransaction.ClearDate);

                        // transfer to vm
                        vmIndex.Add(new IndexViewModel(index, dtoAssetTransaction, clearDate, dtoAsset, assetNameAdditionalInformation, dtoTransactionType.Name, total));
                        // increment index for sorting in view
                        index++;
                    }
                }

                // display view
                return View("Index", vmIndex.OrderByDescending(r => r.Index).ToList());
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return View("Index", new List<IndexViewModel>());
            }
        }

        [HttpGet]
        public ActionResult DisplayForAsset(int assetId)
        {
            try
            {
                // transfer assetId from db
                var dbAssetTransactions = UOW.AssetTransactions.GetAllActiveByDescendingDueDate(assetId);

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
                    string clearDate = DataTypeUtility.GetDateValidatedToString(dtoAssetTransaction.ClearDate);

                    // transfer to vm
                    vmDisplayForAsset.Add(new DisplayForAssetViewModel(dtoAssetTransaction, clearDate, dtoTransactionCategory));
                }

                // display view
                return PartialView("_DisplayForAsset", vmDisplayForAsset);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Encountered problem";
                return PartialView("_DisplayForAsset", new List<DisplayForAssetViewModel>());
            }
        }

        [HttpGet]
        public ActionResult Create(int? assetId)
        {
            try
            {
                // transfer id to dto 
                var dtoAsset = UOW.Assets.Get(DataTypeUtility.GetIntegerFromString(assetId.ToString()));
                if (dtoAsset != null)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);
                    if (dtoAssetType != null)
                    {
                        // add additional identifying info to asset title
                        var assetNameAdditionalInformaiton = BS.AssetSettingService.GetAccountIdentificationInformation(dtoAsset);

                        // transfer dto to sli
                        var sliTransactionTypes = BS.SelectListService.TransactionTypes(null);
                        var sliTransactionCategories = BS.SelectListService.TransactionCategories(null);

                        // display view
                        return View("Create", new CreateViewModel(dtoAsset, assetNameAdditionalInformaiton, dtoAssetType, DateTime.Now, sliTransactionTypes, sliTransactionCategories));
                    }
                }
                TempData["ErrorMessage"] = "Unable to create record. Try again.";
                return RedirectToAction("SelectAssetToCreate", "AssetTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            try
            {
                // transfer vm to dto
                UOW.AssetTransactions.Add(new AssetTransaction()
                {
                    AssetId = vmCreate.AssetId,
                    TransactionTypeId = DataTypeUtility.GetIntegerFromString(vmCreate.SelectedTransactionTypeId),
                    TransactionCategoryId = DataTypeUtility.GetIntegerFromString(vmCreate.SelectedTransactionCategoryId),
                    CheckNumber = vmCreate.CheckNumber,
                    DueDate = Convert.ToDateTime(vmCreate.DueDate),
                    ClearDate = Convert.ToDateTime(vmCreate.ClearDate),
                    Amount = vmCreate.Amount,
                    Note = vmCreate.Note,
                    IsActive = true
                });

                // update db
                UOW.CommitTrans();

                // display view with message
                TempData["SuccessMessage"] = "Record created";
                return RedirectToAction("Details", "Asset", new { Id = vmCreate.AssetId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpGet]
        public ActionResult SelectAssetToCreate()
        {
            try
            {
                // transfer dto to sli
                var sliAssets = BS.AssetService.GetSelectListOfAssets(null);

                // display view
                return View("SelectAssetToCreate", new SelectAssetToCreateViewModel(sliAssets));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectAssetToCreate(SelectAssetToCreateViewModel vmSelectedAssetToCreate)
        {
            try
            {
                // get & validate selected id
                var id = DataTypeUtility.GetIntegerFromString(vmSelectedAssetToCreate.SelectedAssetId);
                if (id > 0)
                {
                    // display view
                    return RedirectToAction("Create", "AssetTransaction", new { assetId = id });
                }
                TempData["ErrorMessage"] = "Value must be selected.";
                return RedirectToAction("SelectAssetToCreate", "AssetTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            { 
                // transfer id to dto 
                var dtoAssetTransaction = UOW.AssetTransactions.Get(id);
                if (dtoAssetTransaction != null)
                {
                    var dtoAsset = UOW.Assets.Get(dtoAssetTransaction.AssetId);
                    if (dtoAsset != null)
                    {
                        var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);
                        if (dtoAssetType != null)
                        {

                            // transfer dto to sli
                            var sliTransactionTypes = BS.SelectListService.TransactionTypes(dtoAssetTransaction.TransactionTypeId.ToString());
                            var sliTransactionCategories = BS.SelectListService.TransactionCategories(dtoAssetTransaction.TransactionCategoryId.ToString());

                            // display view
                            return View("Edit", new EditViewModel(dtoAssetTransaction, dtoAsset, dtoAssetType, sliTransactionTypes, sliTransactionCategories));
                        }
                        TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                        return RedirectToAction("Details", "Asset", new { id = id });
                    }
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            try
            { 
                // transfer vm to dto
                var dtoAssetTransaction = UOW.AssetTransactions.Get(vmEdit.Id);
                dtoAssetTransaction.TransactionTypeId = DataTypeUtility.GetIntegerFromString(vmEdit.SelectedTransactionTypeId);
                dtoAssetTransaction.TransactionCategoryId = DataTypeUtility.GetIntegerFromString(vmEdit.SelectedTransactionCategoryId);
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
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpGet]
        public ActionResult EditAsset(int id, int assetId)
        {
            try
            {
                // transfer id to dto
                var dtoAssetTransaction = UOW.AssetTransactions.Get(id);
                var dtoAsset = UOW.Assets.Get(assetId);
                if (dtoAssetTransaction != null && dtoAsset != null)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);
                    var dtoTransactionType = UOW.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
                    var dtoTransactionCategory = UOW.TransactionCategories.Get(dtoAssetTransaction.TransactionCategoryId);
                    if (dtoAssetType != null && dtoTransactionType != null && dtoTransactionCategory != null)
                    {
                        // validate values to display
                        dtoAssetTransaction.CheckNumber = TransactionUtility.FormatCheckNumber(dtoAssetTransaction.CheckNumber);
                        dtoAssetTransaction.Note = TransactionUtility.FormatTransactionNote(dtoAssetTransaction.Note);

                        // transfer dto to sli
                        var sliAssets = BS.AssetService.GetSelectListOfAssets(dtoAsset.Id.ToString());

                        // transfer to vm and display view
                        return View("EditAsset", new EditAssetViewModel(dtoAssetTransaction, sliAssets, dtoAsset.Id.ToString(), dtoAssetType, dtoTransactionType.Name, dtoTransactionCategory.Name));
                    }
                    TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                    return RedirectToAction("Edit", "AssetTransaction", new { id = id });
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }

        [HttpPost]
        public ActionResult EditAsset(EditAssetViewModel vmEditAsset)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    // transfer vm to dto
                    var dtoAssetTransaction = UOW.AssetTransactions.Get(vmEditAsset.Id);
                    if (dtoAssetTransaction != null)
                    {
                        dtoAssetTransaction.AssetId = DataTypeUtility.GetIntegerFromString(vmEditAsset.SelectedAssetId);

                        // update db
                        UOW.CommitTrans();

                        // display view
                        TempData["SuccessMessage"] = "Asset Name updated";
                        return RedirectToAction("Edit", "AssetTransaction", new { id = vmEditAsset.Id });
                    }
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AssetTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }


    }
}