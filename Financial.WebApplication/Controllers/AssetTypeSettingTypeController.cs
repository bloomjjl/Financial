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
    public class AssetTypeSettingTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeSettingTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetTypeSettingTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedSettingTypes(int assetTypeId)
        {
            // transfer db to vm
            var vmIndexlinkedSettingTypes = _unitOfWork.AssetTypesSettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList()
                .Join(_unitOfWork.SettingTypes.GetAll().Where(r => r.IsActive).ToList(), 
                    atst => atst.SettingTypeId, st => st.Id, 
                    (atst, st) => new IndexLinkedSettingTypesViewModel(st, atst))
                .ToList();

            // display view
            return PartialView("_IndexLinkedSettingTypes", vmIndexlinkedSettingTypes);
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedAssetTypes(int settingTypeId)
        {
            // transfer db to vm
            var vmIndexlinkedAssetTypes = _unitOfWork.AssetTypesSettingTypes.GetAll()
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList()
                .Join(_unitOfWork.AssetTypes.GetAll().Where(r => r.IsActive).ToList(), 
                    atst => atst.AssetTypeId, at => at.Id, 
                    (atst, at) => new IndexLinkedAssetTypesViewModel(at, atst))
                .ToList();

            // display view
            return PartialView("_IndexLinkedAssetTypes", vmIndexlinkedAssetTypes);
        }

        [HttpGet]
        public ViewResult CreateLinkedSettingTypes(int assetTypeId)
        {
            // transfer td
            var previousSuccessMessage = string.Empty;
            if (TempData["SuccessMessage"] != null)
            {
                previousSuccessMessage = TempData["SuccessMessage"].ToString();
            }

            // transfer db to vm
            var dtoAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);
            var vmCreate = _unitOfWork.SettingTypes.GetAll()
                .Select(r => new CreateViewModel(assetTypeId, r))
                .ToList();
                            
            // display view
            return View("CreateLinkedSettingTypes", new CreateLinkedSettingTypesViewModel(dtoAssetType, previousSuccessMessage, vmCreate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedSettingTypes(CreateLinkedSettingTypesViewModel vmCreateLinkedSettingTypes)
        {
            // transfer vm to db
            foreach(var vmCreate in vmCreateLinkedSettingTypes.CreateViewModels)
            {
                _unitOfWork.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                {
                    AssetTypeId = vmCreate.AssetTypeId,
                    SettingTypeId = vmCreate.SettingTypeId,
                    IsActive = vmCreate.IsActive
                });
            }

            // complete db update
            _unitOfWork.CommitTrans();

            // display view
            TempData["SuccessMessage"] = string.Format("{0}, {1}", vmCreateLinkedSettingTypes.SuccessMessage, "Linked Setting Types Created");
            return RedirectToAction("Index", "AssetType", new { id = vmCreateLinkedSettingTypes.AssetTypeId });
        }

        [HttpGet]
        public ViewResult CreateLinkedAssetTypes(int? settingTypeId)
        {
            // transfer td
            var previousSuccessMessage = string.Empty;
            if (TempData["SuccessMessage"] != null)
            {
                previousSuccessMessage = TempData["SuccessMessage"].ToString();
            }

            // transfer db to vm
            var dtoSettingType = _unitOfWork.SettingTypes.Get((int)settingTypeId);
            var vmCreate = _unitOfWork.AssetTypes.GetAll()
                .Select(r => new CreateViewModel((int)settingTypeId, r))
                .ToList();
            var vmCreateLinkedAssetTypes = new CreateLinkedAssetTypesViewModel(dtoSettingType, previousSuccessMessage, vmCreate);

            // display view
            return View("CreateLinkedAssetTypes", vmCreateLinkedAssetTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLinkedAssetTypes(CreateLinkedAssetTypesViewModel vmCreateLinkedAssetTypes)
        {
            // transfer vm to db
            foreach (var vmCreate in vmCreateLinkedAssetTypes.CreateViewModels)
            {
                _unitOfWork.AssetTypesSettingTypes.Add(new Core.Models.AssetTypeSettingType()
                {
                    AssetTypeId = vmCreate.AssetTypeId,
                    SettingTypeId = vmCreate.SettingTypeId,
                    IsActive = vmCreate.IsActive
                });
            }

            // complete db update
            _unitOfWork.CommitTrans();

            // display view
            TempData["SuccessMessage"] = string.Format("{0}, {1}", vmCreateLinkedAssetTypes.SuccessMessage, "Linked Asset Types Created");
            return RedirectToAction("Index", "SettingType", new { id = vmCreateLinkedAssetTypes.SettingTypeId });
        }

        [HttpGet]
        public ViewResult EditLinkedSettingTypes(int assetTypeId)
        {
            // transfer dto for Id
            var dtoAssetType = _unitOfWork.AssetTypes.Get(assetTypeId);

            // transfer db to vm
            var vmEditList = _unitOfWork.AssetTypesSettingTypes.GetAll()
                .Where(r => r.AssetTypeId == assetTypeId)
                .ToList()
                .Join(_unitOfWork.SettingTypes.GetAll().Where(r => r.IsActive).ToList(), 
                    atst => atst.SettingTypeId, st => st.Id, 
                    (atst, st) =>  new EditViewModel(st, atst))
                .ToList();

            // display view
            return View("EditLinkedSettingTypes", new EditLinkedSettingTypesViewModel(dtoAssetType, vmEditList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLinkedSettingTypes(EditLinkedSettingTypesViewModel vmEditLinkedSettingTypes)
        {
            // transfer vm to dto
            foreach(var vmEdit in vmEditLinkedSettingTypes.EditViewModels)
            {
                var dtoAssetTypeSettingType = _unitOfWork.AssetTypesSettingTypes.Get(vmEdit.Id);
                dtoAssetTypeSettingType.IsActive = vmEdit.IsActive;
            }

            // complete db update
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Details", "AssetType", new { id = vmEditLinkedSettingTypes.AssetTypeId });
        }

        [HttpGet]
        public ViewResult EditLinkedAssetTypes(int settingTypeId)
        {
            // transfer dto for id
            var dtoSettingType = _unitOfWork.SettingTypes.Get(settingTypeId);

            // transfer db to vm
            var vmEditList = _unitOfWork.AssetTypesSettingTypes.GetAll()
                .Where(r => r.SettingTypeId == settingTypeId)
                .ToList()
                .Join(_unitOfWork.AssetTypes.GetAll().Where(r => r.IsActive).ToList(),
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
                var dtoAssetTypeSettingType = _unitOfWork.AssetTypesSettingTypes.Get(vmEditList.Id);
                dtoAssetTypeSettingType.IsActive = vmEditList.IsActive;
            }

            // update db
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Details", "SettingType", new { id = vmEditLinkedAssetTypes.SettingTypeId });
        }
    }
}