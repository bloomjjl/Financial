using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.AssetType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class AssetTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AssetTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AssetTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ViewResult Index()
        {
            // transfer db to vm
            var vmIndex = _unitOfWork.AssetTypes.GetAll()
                .Select(r => new IndexViewModel(r))
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create()
        {
            // display view
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // transfer vm to dto
            var dtoAssetType = new AssetType()
            {
                Name = vmCreate.Name,
                IsActive = true
            };

            // transfer dto to db
            _unitOfWork.AssetTypes.Add(dtoAssetType);
            _unitOfWork.CommitTrans();

            // display View
            return RedirectToAction("CreateLinkedSettingTypes", "AssetTypeSettingType", new { assetTypeId = dtoAssetType.Id });
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            // transfer db to vm
            var vmEdit = new EditViewModel(_unitOfWork.AssetTypes.Get((int)id));

            // display view
            return View("Edit", vmEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // transfer vm to dto
            var dtoAssetType = _unitOfWork.AssetTypes.Get(vmEdit.Id);
            dtoAssetType.Name = vmEdit.Name;
            dtoAssetType.IsActive = vmEdit.IsActive;

            // update db
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Index", "AssetType");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            // transfer dto to vm
            var dtoAssetType = _unitOfWork.AssetTypes.Get((int)id);
            var vmDetails = new DetailsViewModel(dtoAssetType);

            // display view
            return View("Details", vmDetails);
        }
    }
}