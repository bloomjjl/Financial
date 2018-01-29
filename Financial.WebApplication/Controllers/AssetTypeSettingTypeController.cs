using Financial.Core;
using Financial.Core.ViewModels.AssetTypeSettingType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeSettingTypeController : BaseController
    {
        public AssetTypeSettingTypeController()
            : base()
        {
        }

        public AssetTypeSettingTypeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedSettingTypes(int assetTypeId)
        {
            // transfer dto to vm
            var vmIndexlinkedSettingTypes = UOW.SettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Join(UOW.AssetTypesSettingTypes.GetAll(),
                    st => st.Id, atst => atst.SettingTypeId, 
                    (st, atst) => new IndexLinkedSettingTypesViewModel(st, atst))
                .Where(atst => atst.IsActive)
                .Where(atst => atst.AssetTypeId == assetTypeId)
                .ToList();

            // display view
            return PartialView("_IndexLinkedSettingTypes", vmIndexlinkedSettingTypes);
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedAssetTypes(int settingTypeId)
        {
            // transfer dto to vm
            var vmIndexlinkedAssetTypes = UOW.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .Join(UOW.AssetTypesSettingTypes.GetAll(),
                    at => at.Id, atst => atst.AssetTypeId,
                    (at, atst) => new IndexLinkedAssetTypesViewModel(at, atst))
                .Where(atst => atst.IsActive)
                .Where(atst => atst.SettingTypeId == settingTypeId)
                .ToList();

            // display view
            return PartialView("_IndexLinkedAssetTypes", vmIndexlinkedAssetTypes);
        }

        [HttpGet]
        public ViewResult CreateLinkedSettingTypes(int assetTypeId)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // transfer dto to vm
            var dtoAssetType = UOW.AssetTypes.Get(assetTypeId);
            var vmCreate = UOW.SettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new CreateViewModel(assetTypeId, r))
                .ToList();
                            
            // display view
            return View("CreateLinkedSettingTypes", new CreateLinkedSettingTypesViewModel(dtoAssetType, vmCreate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedSettingTypes(CreateLinkedSettingTypesViewModel vmCreateLinkedSettingTypes)
        {
            // transfer vm to db
            foreach(var vmCreate in vmCreateLinkedSettingTypes.CreateViewModels)
            {
                UOW.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                {
                    AssetTypeId = vmCreate.AssetTypeId,
                    SettingTypeId = vmCreate.SettingTypeId,
                    IsActive = vmCreate.IsActive
                });
            }

            // complete db update
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Linked setting types created.";
            return RedirectToAction("Create", "AssetTypeRelationshipType", new { assetTypeId = vmCreateLinkedSettingTypes.AssetTypeId });
        }

        [HttpGet]
        public ViewResult CreateLinkedAssetTypes(int? settingTypeId)
        {
            // get messages from other controllers to display in view
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }

            // transfer dto to vm
            var dtoSettingType = UOW.SettingTypes.Get(GetIntegerFromString(settingTypeId.ToString()));
            var vmCreate = UOW.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new CreateViewModel(dtoSettingType.Id, r))
                .ToList();

            // display view
            return View("CreateLinkedAssetTypes", new CreateLinkedAssetTypesViewModel(dtoSettingType, vmCreate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedAssetTypes(CreateLinkedAssetTypesViewModel vmCreateLinkedAssetTypes)
        {
            // transfer vm to db
            foreach (var vmCreate in vmCreateLinkedAssetTypes.CreateViewModels)
            {
                UOW.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                {
                    AssetTypeId = vmCreate.AssetTypeId,
                    SettingTypeId = vmCreate.SettingTypeId,
                    IsActive = vmCreate.IsActive
                });
            }

            // complete db update
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Linked asset types created";
            return RedirectToAction("Index", "SettingType", new { id = vmCreateLinkedAssetTypes.SettingTypeId });
        }

        [HttpGet]
        public ViewResult EditLinkedSettingTypes(int assetTypeId)
        {
            // transfer dto for Id
            var dtoAssetType = UOW.AssetTypes.Get(assetTypeId);

            // get all setting types to display
            var dbSettingTypes = UOW.SettingTypes.GetAll()
                .Where(r => r.IsActive)
                .ToList();

            // store values in vm
            var vmEdit = new List<EditViewModel>();
            foreach(var dtoSettingType in dbSettingTypes)
            {
                // look for existing link
                var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.GetAll()
                    .Where(r => r.AssetTypeId == dtoAssetType.Id)
                    .Where(r => r.SettingTypeId == dtoSettingType.Id)
                    .FirstOrDefault(r => r.IsActive);

                // validate link found
                if(dtoAssetTypeSettingType == null)
                {
                    dtoAssetTypeSettingType = new Core.Models.AssetTypeSettingType();
                }

                // transfer dto to vm
                vmEdit.Add(new EditViewModel(dtoSettingType, dtoAssetTypeSettingType));
            }

            // display view
            return View("EditLinkedSettingTypes", new EditLinkedSettingTypesViewModel(dtoAssetType, vmEdit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLinkedSettingTypes(EditLinkedSettingTypesViewModel vmEditLinkedSettingTypes)
        {
            // transfer vm to dto
            foreach(var vmEdit in vmEditLinkedSettingTypes.EditViewModels)
            {
                var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.Get(vmEdit.Id);
                dtoAssetTypeSettingType.IsActive = vmEdit.IsActive;
            }

            // complete db update
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Linked setting types updated.";
            return RedirectToAction("Details", "AssetType", new { id = vmEditLinkedSettingTypes.AssetTypeId });
        }

        [HttpGet]
        public ViewResult EditLinkedAssetTypes(int settingTypeId)
        {
            // transfer dto for id
            var dtoSettingType = UOW.SettingTypes.Get(settingTypeId);

            // get all asset types to display
            var dbAssetTypes = UOW.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .ToList();

            // store values in vm
            var vmEdit = new List<EditViewModel>();
            foreach (var dtoAssetType in dbAssetTypes)
            {
                // look for existing link
                var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.GetAll()
                    .Where(r => r.AssetTypeId == dtoAssetType.Id)
                    .Where(r => r.SettingTypeId == dtoSettingType.Id)
                    .FirstOrDefault(r => r.IsActive);

                // validate link found
                if (dtoAssetTypeSettingType == null)
                {
                    dtoAssetTypeSettingType = new Core.Models.AssetTypeSettingType();
                }

                // transfer dto to vm
                vmEdit.Add(new EditViewModel(dtoAssetType, dtoAssetTypeSettingType));
            }

            // display view
            return View("EditLinkedAssetTypes", new EditLinkedAssetTypesViewModel(dtoSettingType, vmEdit));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLinkedAssetTypes(EditLinkedAssetTypesViewModel vmEditLinkedAssetTypes)
        {
            // transfer vm to dto
            foreach(var vmEditList in vmEditLinkedAssetTypes.EditViewModels)
            {
                var dtoAssetTypeSettingType = UOW.AssetTypesSettingTypes.Get(vmEditList.Id);
                dtoAssetTypeSettingType.IsActive = vmEditList.IsActive;
            }

            // update db
            UOW.CommitTrans();

            // display view with message
            TempData["SuccessMessage"] = "Linked asset types updated.";
            return RedirectToAction("Details", "SettingType", new { id = vmEditLinkedAssetTypes.SettingTypeId });
        }
    }
}