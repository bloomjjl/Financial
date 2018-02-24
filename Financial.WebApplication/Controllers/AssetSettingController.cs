using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.AssetSetting;
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
            // transfer id to dto
            var dtoAsset = UOW.Assets.Get(assetId);

            // validate dto
            if(dtoAsset == null)
            {
                return PartialView("_Index", new List<IndexViewModel>());
            }

            // get list of linked setting types
            var dbAssetTypeSettingTypes = UOW.AssetTypesSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == dtoAsset.AssetTypeId)
                .ToList();

            // create & transfer values to vm
            var vmIndex = new List<IndexViewModel>();
            foreach(var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
            {
                // transfer to dto
                var dtoSettingType = UOW.SettingTypes.Get(dtoAssetTypeSettingType.SettingTypeId);
                var dtoAssetSetting = UOW.AssetSettings.GetAll()
                    .Where(r => r.IsActive)
                    .Where(r => r.AssetId == dtoAsset.Id)
                    .FirstOrDefault(r => r.SettingTypeId == dtoSettingType.Id);

                // validate dto
                if (dtoAssetSetting == null)
                {
                    vmIndex.Add(new IndexViewModel(new AssetSetting(), assetId, dtoSettingType));
                }
                else
                { 
                    vmIndex.Add(new IndexViewModel(dtoAssetSetting, assetId, dtoSettingType));
                }
            }
            
            // display view
            return PartialView("_Index", vmIndex);
        }

        [HttpGet]
        public ActionResult Create(int assetId)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // transfer id to dto
            var dtoAsset = UOW.Assets.Get(assetId);
            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // transfer dto to vm
            var vmCreate = UOW.AssetTypesSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == dtoAsset.AssetTypeId)
                .Join(UOW.SettingTypes.GetAll(),
                    atst => atst.SettingTypeId, st => st.Id,
                    (atst, st) => new { dtoAsset, st })
                .Where(j => j.st.IsActive)
                .Select(j => new CreateViewModel(dtoAsset, j.st))
                .ToList();

            // display view
            return View("Create", new CreateLinkedSettingTypesViewModel(dtoAsset, dtoAssetType, vmCreate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLinkedSettingTypesViewModel vmCreate)
        {
            // transfer vm to dto
            foreach(var vm in vmCreate.CreateViewModels)
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

        [HttpGet]
        public ActionResult Edit(int assetId)
        {
            // transfer id to dto
            var dtoAsset = UOW.Assets.Get(assetId);
            
            // validate dto
            if(dtoAsset == null)
            {
                return RedirectToAction("Index", "Asset");
            }

            var dtoAssetType = UOW.AssetTypes.Get(dtoAsset.AssetTypeId);

            // get list of linked setting types
            var dbAssetTypeSettingTypes = UOW.AssetTypesSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == dtoAsset.AssetTypeId)
                .ToList();

            // create & transfer values to vm
            var vmEdit = new List<EditViewModel>();
            foreach (var dtoAssetTypeSettingType in dbAssetTypeSettingTypes)
            {
                // transfer to dto
                var dtoSettingType = UOW.SettingTypes.Get(dtoAssetTypeSettingType.SettingTypeId);
                var dtoAssetSetting = UOW.AssetSettings.GetAll()
                    .Where(r => r.IsActive)
                    .Where(r => r.AssetId == dtoAsset.Id)
                    .FirstOrDefault(r => r.SettingTypeId == dtoSettingType.Id);

                // validate dto
                if (dtoAssetSetting == null)
                {
                    vmEdit.Add(new EditViewModel(new AssetSetting(), dtoAsset, dtoSettingType));
                }
                else
                {
                    vmEdit.Add(new EditViewModel(dtoAssetSetting, dtoAsset, dtoSettingType));
                }
            }

            // display view
            return View("Edit", new EditLinkedSettingTypesViewModel(dtoAsset, dtoAssetType, vmEdit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLinkedSettingTypesViewModel vmEditLinkedSettingTypes)
        {
            // transfer vm to dto
            foreach(var vmEdit in vmEditLinkedSettingTypes.EditViewModels)
            {
                // new entry?
                if(vmEdit.Id == 0)
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
    }
}