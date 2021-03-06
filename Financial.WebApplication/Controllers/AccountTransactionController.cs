﻿using Financial.Business;
using Financial.Business.ServiceInterfaces;
using Financial.Business.Utilities;
using Financial.Core;
using Financial.Core.Models;
using Financial.Data;
using Financial.WebApplication.Models.ViewModels.AccountTransaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.Business.Models;

namespace Financial.WebApplication.Controllers
{
    public class AccountTransactionController : BaseController
    {
        private IBusinessService _businessService;

        public AccountTransactionController(IBusinessService businessService)
            : base()
        {
            _businessService = businessService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            try
            {
                if (TempData["ErrorMessage"] != null)
                    ViewData["ErrorMessage"] = TempData["ErrorMessage"];
                if (TempData["SuccessMessage"] != null)
                    ViewData["SuccessMessage"] = TempData["SuccessMessage"];

                // transfer bm to vm
                var vmIndex = _businessService.AccountTransactionService.GetListOfActiveTransactions()
                    .OrderByDescending(r => r.DueDate)
                    .Select(r => new IndexViewModel(r))
                    .ToList();

                return View("Index", vmIndex);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return View("Index", new List<IndexViewModel>());
            }
        }

        [HttpGet]
        public ActionResult Create(int? assetId)
        {
            try
            {
                var bmAccount = _businessService.AccountTransactionService.GetAccountForTransaction(assetId);
                var sliAccounts = _businessService.AccountTransactionService.GetAccountSelectList(assetId.ToString());
                var sliTransactionTypes = _businessService.AccountTransactionService.GetTransactionTypeSelectList(null);
                var sliTransactionCategory = _businessService.AccountTransactionService.GetTransactionCategorySelectList(null);

                /*
                // get bm
                var bmAssetTransaction = _businessService.AccountTransactionService.GetTransactionOptions(assetId);
                if (bmAssetTransaction == null)
                {
                    TempData["ErrorMessage"] = "Unable to create record. Try again.";
                    return RedirectToAction("Index", "AccountTransaction");
                }

                // transfer bm to vm
                var vmCreate = new CreateViewModel(bmAssetTransaction);
                */
                return View("Create", new CreateViewModel(sliAccounts, sliTransactionTypes, sliTransactionCategory));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Problem creating record. Try again.";
                    return RedirectToAction("Index", "AccountTransaction");
                }

                // transfer vm to bm
                var bmAssetTransaction = new Business.Models.AccountTransaction()
                {
                    AssetId = DataTypeUtility.GetIntegerFromString(vmCreate.SelectedAccountId),
                    TransactionTypeId = DataTypeUtility.GetIntegerFromString(vmCreate.SelectedTransactionTypeId),
                    TransactionCategoryId = DataTypeUtility.GetIntegerFromString(vmCreate.SelectedTransactionCategoryId),
                    CheckNumber = vmCreate.CheckNumber,
                    DueDate = Convert.ToDateTime(vmCreate.DueDate),
                    ClearDate = Convert.ToDateTime(vmCreate.ClearDate),
                    Amount = vmCreate.Amount,
                    Note = vmCreate.Note,
                };

                // update db
                if (!_businessService.AccountTransactionService.AddTransaction(bmAssetTransaction))
                {
                    //bmAssetTransaction = _businessService.AccountTransactionService.GetTransactionOptions(vmCreate.AssetId);
                    //vmCreate.Assets = bmAssetTransaction.AssetSelectList;
                    //vmCreate.TransactionTypes = bmAssetTransaction.TransactionTypeSelectList;
                    //vmCreate.TransactionCategories= bmAssetTransaction.TransactionCategorySelectList;
                    ViewData["ErrorMessage"] = "Problem creating record";
                    return View("Create", vmCreate);
                }

                TempData["SuccessMessage"] = "Record created";
                return RedirectToAction("Details", "Account", new { Id = vmCreate.AssetId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                // get bm
                var bmAssetTransaction = _businessService.AccountTransactionService.GetTransactionToEdit(id);
                if (bmAssetTransaction == null)
                {
                    TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                    return RedirectToAction("Index", "AccountTransaction");
                }

                return View("Edit", new EditViewModel(bmAssetTransaction));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Problem editing record. Try again.";
                    return RedirectToAction("Index", "AccountTransaction");
                }

                // transfer vm to bm
                var bmAssetTransaction = new Business.Models.AccountTransaction()
                {
                    AssetTransactionId = vmEdit.Id,
                    AssetId = vmEdit.AssetId,
                    TransactionTypeId = DataTypeUtility.GetIntegerFromString(vmEdit.SelectedTransactionTypeId),
                    TransactionCategoryId = DataTypeUtility.GetIntegerFromString(vmEdit.SelectedTransactionCategoryId),
                    CheckNumber = vmEdit.CheckNumber,
                    DueDate = Convert.ToDateTime(vmEdit.DueDate),
                    ClearDate = Convert.ToDateTime(vmEdit.ClearDate),
                    Amount = vmEdit.Amount,
                    Note = vmEdit.Note,
                };

                // update db
                if (!_businessService.AccountTransactionService.UpdateTransaction(bmAssetTransaction))
                {
                    ViewData["ErrorMessage"] = "Problem updating record";
                    return View("Edit", vmEdit);
                }

                TempData["SuccessMessage"] = "Record updated";
                return RedirectToAction("Details", "Account", new { id = vmEdit.AssetId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                // get bm
                var bmAssetTransaction = _businessService.AccountTransactionService.GetTransactionToDelete(id);

                // tranfer bm to vm
                return View("Delete", new DeleteViewModel(bmAssetTransaction));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel vmDelete)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Problem deleting record. Try again.";
                    return RedirectToAction("Index", "AccountTransaction");
                }

                // update db
                if (!_businessService.AccountTransactionService.DeleteTransaction(vmDelete.Id))
                {
                    ViewData["ErrorMessage"] = "Problem deleting record";
                    return View("Delete", vmDelete);
                }

                TempData["SuccessMessage"] = "Record deleted";
                return RedirectToAction("Index", "AccountTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }




        [HttpGet]
        public ActionResult DisplayForAsset(int assetId)
        {
            try
            {
                /*
                // transfer assetId from db
                var dbAssetTransactions = _unitOfWork.AssetTransactions.GetAllActiveByDescendingDueDate(assetId);

                // transfer dto to vm
                var vmDisplayForAsset = new List<DisplayForAssetViewModel>();
                foreach(var dtoAssetTransaction in dbAssetTransactions)
                {
                    var dtoTransactionCategory = _unitOfWork.TransactionCategories.Get(dtoAssetTransaction.TransactionCategoryId);

                    // is Expense?
                    if(dtoAssetTransaction.TransactionTypeId == 1)
                    {
                        // update to "-0.00"
                        dtoAssetTransaction.Amount = dtoAssetTransaction.Amount * -1;
                    }

                    // format date
                    string clearDate = DataTypeUtility.GetDateValidatedToShortDateString(dtoAssetTransaction.ClearDate);

                    // transfer to vm
                    vmDisplayForAsset.Add(new DisplayForAssetViewModel(dtoAssetTransaction, clearDate, dtoTransactionCategory));
                }
                */
                // display view
                return PartialView("_DisplayForAsset", new List<DisplayForAssetViewModel>());
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Encountered problem";
                return PartialView("_DisplayForAsset", new List<DisplayForAssetViewModel>());
            }
        }

        /*
         * TODO: implement service
        [HttpGet]
        public ActionResult SelectAssetToCreate()
        {
            try
            {
                // transfer dto to sli
                var sliAssets = _businessService.AssetService.GetSelectListOfAssets(null);

                // display view
                return View("SelectAssetToCreate", new SelectAssetToCreateViewModel(sliAssets));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetTransaction");
            }
        }
        */

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
                    return RedirectToAction("Create", "AccountTransaction", new { assetId = id });
                }
                TempData["ErrorMessage"] = "Value must be selected.";
                return RedirectToAction("SelectAssetToCreate", "AccountTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }

        /*
         * TODO: implement service
        [HttpGet]
        public ActionResult EditAsset(int id, int assetId)
        {
            try
            {
                // transfer id to dto
                var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(id);
                var dtoAsset = _unitOfWork.Assets.Get(assetId);
                if (dtoAssetTransaction != null && dtoAsset != null)
                {
                    var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAsset.AssetTypeId);
                    var dtoTransactionType = _unitOfWork.TransactionTypes.Get(dtoAssetTransaction.TransactionTypeId);
                    var dtoTransactionCategory = _unitOfWork.TransactionCategories.Get(dtoAssetTransaction.TransactionCategoryId);
                    if (dtoAssetType != null && dtoTransactionType != null && dtoTransactionCategory != null)
                    {
                        // validate values to display
                        dtoAssetTransaction.CheckNumber = TransactionUtility.FormatCheckNumber(dtoAssetTransaction.CheckNumber);
                        dtoAssetTransaction.Note = TransactionUtility.FormatTransactionNote(dtoAssetTransaction.Note);

                        // transfer dto to sli
                        var sliAssets = _businessService.AssetService.GetSelectListOfAssets(dtoAsset.Id.ToString());

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
        */

        [HttpPost]
        public ActionResult EditAsset(EditAssetViewModel vmEditAsset)
        {
            try
            { 
                /*
                if (ModelState.IsValid)
                {
                    // transfer vm to dto
                    var dtoAssetTransaction = _unitOfWork.AssetTransactions.Get(vmEditAsset.Id);
                    if (dtoAssetTransaction != null)
                    {
                        dtoAssetTransaction.AssetId = DataTypeUtility.GetIntegerFromString(vmEditAsset.SelectedAssetId);

                        // update db
                        _unitOfWork.CommitTrans();

                        // display view
                        TempData["SuccessMessage"] = "Asset Name updated";
                        return RedirectToAction("Edit", "AccountTransaction", new { id = vmEditAsset.Id });
                    }
                }
                */
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "AccountTransaction");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "AccountTransaction");
            }
        }


    }
}