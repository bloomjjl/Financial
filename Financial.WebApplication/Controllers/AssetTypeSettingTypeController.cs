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

            // display view
            TempData["SuccessMessage"] = "Linked setting types created.";
            return RedirectToAction("Index", "AssetType", new { id = vmCreateLinkedSettingTypes.AssetTypeId });
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
            var dtoSettingType = UOW.SettingTypes.Get((int)settingTypeId);
            var vmCreate = UOW.AssetTypes.GetAll()
                .Where(r => r.IsActive)
                .Select(r => new CreateViewModel((int)settingTypeId, r))
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

            // display view
            TempData["SuccessMessage"] = "Linked asset types created";
            return RedirectToAction("Index", "SettingType", new { id = vmCreateLinkedAssetTypes.SettingTypeId });
        }

        [HttpGet]
        public ViewResult EditLinkedSettingTypes(int assetTypeId)
        {
            // transfer dto for Id
            var dtoAssetType = UOW.AssetTypes.Get(assetTypeId);

            // transfer dto to vm
            var vmEdit = UOW.SettingTypes.GetAll()
                .Where(r => r.IsActive)
                .ToList()
                .Join(UOW.AssetTypesSettingTypes.GetAll(),
                    st => st.Id, atst => atst.SettingTypeId,
                    (st, atst) => new EditViewModel(st, atst))
                .Where(st => st.IsActive)
                .ToList();
/*            var vmEditList = UOW.AssetTypesSettingTypes.GetAll()
                .Where(r => r.AssetTypeId == assetTypeId)
                .Join(UOW.SettingTypes.GetAll().Where(r => r.IsActive).ToList(), 
                    atst => atst.SettingTypeId, st => st.Id, 
                    (atst, st) =>  new (st, atst))
                    .
                .ToList();
*/
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

            // display view
            return RedirectToAction("Details", "AssetType", new { id = vmEditLinkedSettingTypes.AssetTypeId });
        }

        [HttpGet]
        public ViewResult EditLinkedAssetTypes(int settingTypeId)
        {
            // transfer dto for id
            var dtoSettingType = UOW.SettingTypes.Get(settingTypeId);

            // transfer db to vm
            var vmEditList = UOW.AssetTypesSettingTypes.GetAll()
                .Where(r => r.SettingTypeId == settingTypeId)
                .Join(UOW.AssetTypes.GetAll().Where(r => r.IsActive).ToList(),
                atst => atst.AssetTypeId, at => at.Id,
                (atst, at) => new EditViewModel(at, atst))
                .ToList();

            // display view
            return View("EditLinkedAssetTypes", new EditLinkedAssetTypesViewModel(dtoSettingType, vmEditList));
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

            // display view
            return RedirectToAction("Details", "SettingType", new { id = vmEditLinkedAssetTypes.SettingTypeId });
        }
    }
}