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
            var dbSettingTypes = _unitOfWork.SettingTypes.GetAll();
            var vmCreate = new List<CreateViewModel>();
            foreach(var dtoSettingType in dbSettingTypes)
            {
                vmCreate.Add(new CreateViewModel((int)assetTypeId, dtoSettingType));
            }
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
                    AssettTypeId = vmCreate.AssetTypeId,
                    SettingTypeId = vmCreate.SettingTypeId,
                    IsActive = vmCreate.IsActive
                });
            }

            // complete db update
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Details", "AssetType", new { assetTypeId = vmCreateLinkedSettingTypes.AssetTypeId });
        }
    }
}