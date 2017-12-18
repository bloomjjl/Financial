using Financial.Core;
using Financial.Core.Models;
using Financial.Core.ViewModels.SettingType;
using Financial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financial.WebApplication.Controllers
{
    public class SettingTypeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public SettingTypeController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public SettingTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ViewResult Index()
        {
            // transfer db to vm
            var vmIndex = _unitOfWork.SettingTypes.GetAll()
                .Select(r => new IndexViewModel(r))
                .ToList();

            // display view
            return View("Index", vmIndex);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel vmCreate)
        {
            // transfer vm to dto
            var dtoSettingType = new SettingType()
            {
                Name = vmCreate.Name,
                IsActive = true
            };

            // update db
            _unitOfWork.SettingTypes.Add(dtoSettingType);
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("CreateLinkedAssetTypes", "AssetTypeSettingType", new { settingTypeId = dtoSettingType.Id });
        }

        [HttpGet]
        public ViewResult Edit(int? id)
        {
            // transfer db to vm
            var vmEdit = _unitOfWork.SettingTypes.GetAll()
                .Select(r => new EditViewModel(r))
                .FirstOrDefault(r => r.Id == id);

            // display view
            return View("Edit", vmEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel vmEdit)
        {
            // transfer vm to dto
            var dtoSettingType = _unitOfWork.SettingTypes.Get(vmEdit.Id);
            dtoSettingType.Name = vmEdit.Name;
            dtoSettingType.IsActive = vmEdit.IsActive;

            // update db
            _unitOfWork.SettingTypes.Update(dtoSettingType);
            _unitOfWork.CommitTrans();

            // display view
            return RedirectToAction("Index", "SettingType");
        }

        [HttpGet]
        public ViewResult Details(int? id)
        {
            // transfer dto to vm
            var vmDetails = _unitOfWork.SettingTypes.GetAll()
                .Select(r => new DetailsViewModel(r))
                .FirstOrDefault(r => r.Id == id);

            // display view
            return View("Details", vmDetails);
        }
    }
}