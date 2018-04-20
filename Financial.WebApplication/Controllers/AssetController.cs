﻿using Financial.Business;
using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.Asset;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetController : BaseController
    {
        public AssetController()
            : base()
        {
        }

        public AssetController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            try
            {
                // transfer dto to vm
                var dbAssets = UOW.Assets.GetAll()
                    .Where(r => r.IsActive);

                var vmIndex = new List<IndexViewModel>();
                foreach(var dtoAsset in dbAssets)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);
                    var assetNameAdditionalInformaiton = BS.AssetSettingService.GetAccountIdentificationInformation(dtoAsset);

                    vmIndex.Add(new IndexViewModel(dtoAsset, assetNameAdditionalInformaiton, dtoAssetType));
                }
 
                // display view
                return View("Index", vmIndex.OrderBy(r => r.AssetName));
            }
            catch (Exception)
            {
                return View("Index", new List<IndexViewModel>());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                // transfer dto to sli
                var sliAssetTypes = BS.AssetTypeService.GetAssetTypesDropDownList(null);

                // display view
                return View("Create", new CreateViewModel(sliAssetTypes));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // transfer vm to dto
                    var dtoAsset = new Asset()
                    {
                        AssetTypeId = Business.Utilities.DataTypeUtility.GetIntegerFromString(vmCreate.SelectedAssetTypeId),
                        Name = vmCreate.AssetName,
                        IsActive = true
                    };
                    UOW.Assets.Add(dtoAsset);

                    // update db
                    UOW.CommitTrans();

                    // display view
                    TempData["SuccessMessage"] = "Asset Created";
                    return RedirectToAction("Create", "AssetSetting", new { assetId = dtoAsset.Id });
                }
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            { 
                // transfer id to dto
                var dtoAsset = UOW.Assets.Get(id);
                if (dtoAsset != null)
                {
                    var sliAssetTypes = BS.AssetTypeService.GetAssetTypesDropDownList(dtoAsset.AssetTypeId);

                    // display view
                    return View("Edit", new EditViewModel(dtoAsset, sliAssetTypes));
                }
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            try
            { 
                // transfer vm to dto
                var dtoAsset = UOW.Assets.Get(vmEdit.Id);
                if (dtoAsset != null)
                {
                    dtoAsset.Name = vmEdit.Name;
                    dtoAsset.AssetTypeId = Business.Utilities.DataTypeUtility.GetIntegerFromString(vmEdit.SelectedAssetTypeId);

                    // update db
                    UOW.CommitTrans();

                    // display view with message
                    TempData["SuccessMessage"] = "Record updated.";
                    return RedirectToAction("Details", "Asset", new { id = vmEdit.Id });
                }
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        } 

        [HttpGet]
        public ActionResult Details(int id)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            try
            { 
                // transfer id to dto
                var dtoAsset = UOW.Assets.Get(id);
                if (dtoAsset != null)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

                    // display view with message
                    return View("Details", new DetailsViewModel(dtoAsset, dtoAssetType));
                }
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            { 
                // transfer id to dto
                var dtoAsset = UOW.Assets.Get(id);
                if (dtoAsset != null)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

                    // display view
                    return View("Delete", new DeleteViewModel(dtoAsset, dtoAssetType));
                }
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel vmDelete)
        {
            try
            { 
                // transfer vm to dto
                var dtoAsset = UOW.Assets.Get(vmDelete.Id);
                if (dtoAsset != null)
                {
                    dtoAsset.IsActive = false;

                    // update db
                    UOW.CommitTrans();

                    // display view with message
                    TempData["SuccessMessage"] = "Record Deleted";
                    return RedirectToAction("Index", "Asset");
                }
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem. Try again.";
                return RedirectToAction("Index", "Asset");
            }
        }
    }
}