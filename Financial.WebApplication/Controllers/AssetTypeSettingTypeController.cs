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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            // display view
            return View();
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
            return RedirectToAction("Details", "AssetType", new { assetTypeId = vmCreateLinkedSettingTypes.AssetTypeId });
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
            return RedirectToAction("Details", "SettingType", new { settingTypeId = vmCreateLinkedAssetTypes.SettingTypeId });
        }
    }
}