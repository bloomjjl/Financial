using Financial.Core;
using Financial.Core.Models;
using Financial.WebApplication.Models.ViewModels.AssetSetting;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetSettingController : BaseController
    {
        public AssetSettingController()
            : base()
        {
        }

        public AssetSettingController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [HttpGet]
        public ActionResult Index(int assetId)
        {
            try
            {
                // transfer id to dto
                var dtoAsset = UOW.Assets.Get(assetId);
                if (dtoAsset != null)
                {
                    // get list of linked setting types
                    var dbAssetTypeSettingTypes = UOW.AssetTypesSettingTypes.GetAllActiveForAssetType(dtoAsset.AssetTypeId);

                    // create & transfer values to vm
                    var vmIndex = new List<IndexViewModel>();
                    foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
                    {
                        // transfer to dto
                        var dtoSettingType = UOW.SettingTypes.Get(dtoAssetTypeSettingType.SettingTypeId);
                        var dtoAssetSetting = UOW.AssetSettings.GetActive(dtoAsset.Id, dtoSettingType.Id);

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
                var dtoAsset = UOW.Assets.Get(assetId);
                if (dtoAsset != null)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

                    // transfer dto to vm
                    var vmCreate = new List<CreateViewModel>();
                    var dbATST = UOW.AssetTypesSettingTypes.GetAllActiveForAssetType(dtoAsset.AssetTypeId);
                    foreach(var dtoATST in dbATST)
                    {
                        var dtoSettingType = UOW.SettingTypes.Get(dtoATST.SettingTypeId);
                        vmCreate.Add(new CreateViewModel(dtoAsset, dtoSettingType));
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
                            UOW.AssetSettings.Add(new AssetSetting()
                            {
                                AssetId = vm.AssetId,
                                SettingTypeId = vm.SettingTypeId,
                                Value = vm.Value,
                                IsActive = true
                            });
                        }

                        // update db
                        UOW.CommitTrans();

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
                var dtoAsset = UOW.Assets.Get(assetId);
                if (dtoAsset != null)
                {
                    var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

                    // get list of linked setting types
                    var dbAssetTypeSettingTypes = UOW.AssetTypesSettingTypes.GetAllActiveForAssetType(dtoAsset.AssetTypeId);

                    // create & transfer values to vm
                    var vmEdit = new List<EditViewModel>();
                    foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
                    {
                        // transfer to dto
                        var dtoSettingType = UOW.SettingTypes.Get(dtoAssetTypeSettingType.SettingTypeId);
                        var dtoAssetSetting = UOW.AssetSettings.GetActive(dtoAsset.Id, dtoSettingType.Id);
                        
                        // validate dto & update vm
                        var vm = dtoAssetSetting == null
                            ? new EditViewModel(new AssetSetting(), dtoAsset, dtoSettingType)
                            : new EditViewModel(dtoAssetSetting, dtoAsset, dtoSettingType);
                        vmEdit.Add(vm);
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
                            UOW.AssetSettings.Add(new AssetSetting()
                            {
                                AssetId = vmEditLinkedSettingTypes.AssetId,
                                SettingTypeId = vmEdit.SettingTypeId,
                                Value = vmEdit.Value,
                                IsActive = true
                            });
                        }
                        else
                        {
                            var dtoAssetSetting = UOW.AssetSettings.Get(vmEdit.Id);
                            dtoAssetSetting.Value = vmEdit.Value;
                        }
                    }

                    // update db
                    UOW.CommitTrans();

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