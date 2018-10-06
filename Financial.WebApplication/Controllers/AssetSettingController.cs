using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.AssetSetting;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Financial.Business;

namespace Financial.WebApplication.Controllers
{
    public class AssetSettingController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IBusinessService _businessService;

        public AssetSettingController(IUnitOfWork unitOfWork, IBusinessService businessService)
            : base()
        {
            _unitOfWork = unitOfWork;
            _businessService = businessService;
        }

        [HttpGet]
        public ActionResult Index(int assetId)
        {
            try
            {
                // transfer id to dto
                var dtoAsset = _unitOfWork.Assets.Get(assetId);
                if (dtoAsset != null)
                {
                    // get list of linked setting types
                    var dbAssetSettings = _unitOfWork.AssetSettings.GetAllActiveForAsset(dtoAsset.Id);

                    // create & transfer values to vm
                    var vmIndex = new List<IndexViewModel>();
                    foreach (var dtoAssetSetting in dbAssetSettings)
                    {
                        // transfer to dto
                        var dtoSettingType = _unitOfWork.SettingTypes.GetActive(dtoAssetSetting.SettingTypeId);

                        // validate dto & update vm
                        var vm = dtoAssetSetting == null 
                            ? new IndexViewModel(new AssetSetting(), assetId, dtoSettingType)
                            : new IndexViewModel(dtoAssetSetting, assetId, dtoSettingType);
                        vmIndex.Add(vm);
                    }
                    // display view
                    return PartialView("_Index", vmIndex);
                }
                return PartialView("_Index", new List<IndexViewModel>());
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "AssetSetting");
            }
        }

        [HttpGet]
        public ActionResult Create(int assetId)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            try
            {
                // transfer id to dto
                var dtoAsset = _unitOfWork.Assets.Get(assetId);
                if (dtoAsset != null)
                {
                    var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAsset.AssetTypeId);

                    // transfer dto to vm
                    var vmCreate = new List<CreateViewModel>();
                    var dbAssetTypeSettingTypes = _unitOfWork.AssetTypeSettingTypes.GetAllActiveForAssetType(dtoAsset.AssetTypeId);
                    foreach(var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
                    {
                        var dtoSettingType = _unitOfWork.SettingTypes.GetActive(dtoAssetTypeSettingType.SettingTypeId);
                        if (dtoSettingType != null)
                        {
                            vmCreate.Add(new CreateViewModel(dtoAsset, dtoSettingType));
                        }
                    }

                    // display view
                    return View("Create", new CreateLinkedSettingTypesViewModel(dtoAsset, dtoAssetType, vmCreate));
                }
                TempData["ErrorMessage"] = "Unable to create record. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered problem";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLinkedSettingTypesViewModel vmCreate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // validate vm 
                    if (vmCreate.CreateViewModels != null)
                    {
                        // transfer vm to dto
                        foreach (var vm in vmCreate.CreateViewModels)
                        {
                            _unitOfWork.AssetSettings.Add(new AssetSetting()
                            {
                                AssetId = vm.AssetId,
                                SettingTypeId = vm.SettingTypeId,
                                Value = vm.Value,
                                IsActive = true
                            });
                        }

                        // update db
                        _unitOfWork.CommitTrans();

                        // display view with message
                        TempData["SuccessMessage"] = "Records created";
                        return RedirectToAction("Details", "Asset", new { id = vmCreate.AssetId });
                    }
                    TempData["SuccessMessage"] = "No Linked Setting Types to Update";
                    return RedirectToAction("Details", "Asset", new { id = vmCreate.AssetId });
                }
                TempData["ErrorMessage"] = "Unable to create record. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpGet]
        public ActionResult Edit(int assetId)
        {
            try
            {
                // transfer id to dto
                var dtoAsset = _unitOfWork.Assets.Get(assetId);
                if (dtoAsset != null)
                {
                    var dtoAssetType = _unitOfWork.AssetTypes.Get(dtoAsset.AssetTypeId);

                    // get list of linked setting types
                    var dbAssetTypeSettingTypes = _unitOfWork.AssetTypeSettingTypes.GetAllActiveForAssetType(dtoAsset.AssetTypeId);

                    // create & transfer values to vm
                    var vmEdit = new List<EditViewModel>();
                    foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
                    {
                        // transfer to dto
                        var dtoSettingType = _unitOfWork.SettingTypes.GetActive(dtoAssetTypeSettingType.SettingTypeId);
                        if (dtoSettingType != null)
                        {
                            var dtoAssetSetting = _unitOfWork.AssetSettings.GetActive(dtoAsset.Id, dtoSettingType.Id);
                            if (dtoAssetSetting != null)
                            {
                                vmEdit.Add(new EditViewModel(dtoAssetSetting, dtoAsset, dtoSettingType));
                            }
                        }
                    }

                    // display view
                    return View("Edit", new EditLinkedSettingTypesViewModel(dtoAsset, dtoAssetType, vmEdit));
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "Asset");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLinkedSettingTypesViewModel vmEditLinkedSettingTypes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // transfer vm to dto
                    foreach (var vmEdit in vmEditLinkedSettingTypes.EditViewModels)
                    {
                        // new entry?
                        if (vmEdit.Id == 0)
                        {
                            // YES. Create record
                            _unitOfWork.AssetSettings.Add(new AssetSetting()
                            {
                                AssetId = vmEditLinkedSettingTypes.AssetId,
                                SettingTypeId = vmEdit.SettingTypeId,
                                Value = vmEdit.Value,
                                IsActive = true
                            });
                        }
                        else
                        {
                            var dtoAssetSetting = _unitOfWork.AssetSettings.Get(vmEdit.Id);
                            dtoAssetSetting.Value = vmEdit.Value;
                        }
                    }

                    // update db
                    _unitOfWork.CommitTrans();

                    // display view with message
                    TempData["SuccessMessage"] = "Records updated";
                    return RedirectToAction("Details", "Asset", new { id = vmEditLinkedSettingTypes.AssetId });
                }
                TempData["ErrorMessage"] = "Unable to edit record. Try again.";
                return RedirectToAction("Index", "Asset");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Encountered Problem";
                return RedirectToAction("Index", "Asset");
            }
        }


    }
}