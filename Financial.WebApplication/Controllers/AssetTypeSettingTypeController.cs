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
                .Join(_unitOfWork.SettingTypes.GetAll().ToList(), atst => atst.SettingTypeId, st => st.Id, (atst, st) => new { atst, st })
                .Where(ratst => ratst.atst.AssetTypeId == assetTypeId)
                .Select(m => new IndexLinkedSettingTypesViewModel(m.st, m.atst))
                .ToList();

            // display view
            return PartialView("_IndexLinkedSettingTypes", vmIndexlinkedSettingTypes);
        }

        [ChildActionOnly]
        public ActionResult IndexLinkedAssetTypes(int settingTypeId)
        {
            // transfer db to vm
            var vmIndexlinkedAssetTypes = _unitOfWork.AssetTypesSettingTypes.GetAll()
                .Join(_unitOfWork.AssetTypes.GetAll().ToList(), atst => atst.SettingTypeId, at => at.Id, (atst, at) => new { atst, at })
                .Where(ratst => ratst.atst.SettingTypeId == settingTypeId)
                .Select(m => new IndexLinkedAssetTypesViewModel(m.at, m.atst))
                .ToList();

            // display view
            return PartialView("_IndexLinkedAssetTypes", vmIndexlinkedAssetTypes);
        }

        [HttpGet]
        public ViewResult CreateLinkedSettingTypes(int? assetTypeId)
        {
            // transfer db to vm
            var dtoAssetType = _unitOfWork.AssetTypes.Get((int)assetTypeId);
            var vmCreate = _unitOfWork.SettingTypes.GetAll()
                .Select(r => new CreateViewModel((int)assetTypeId, r))
                .ToList();
            var vmCreateLinkedSettingTypes = new CreateLinkedSettingTypesViewModel(dtoAssetType, vmCreate);
                            
            // display view
            return View("CreateLinkedSettingTypes", vmCreateLinkedSettingTypes);
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
            return RedirectToAction("Details", "AssetType", new { id = vmCreateLinkedSettingTypes.AssetTypeId });
        }

        [HttpGet]
        public ViewResult CreateLinkedAssetTypes(int? settingTypeId)
        {
            // transfer db to vm
            var dtoSettingType = _unitOfWork.SettingTypes.Get((int)settingTypeId);
            var vmCreate = _unitOfWork.AssetTypes.GetAll()
                .Select(r => new CreateViewModel((int)settingTypeId, r))
                .ToList();
            var vmCreateLinkedAssetTypes = new CreateLinkedAssetTypesViewModel(dtoSettingType, vmCreate);

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
            return RedirectToAction("Details", "SettingType", new { id = vmCreateLinkedAssetTypes.SettingTypeId });
        }
    }
}