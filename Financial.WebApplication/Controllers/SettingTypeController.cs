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
            // get TempData
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

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
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Encountered a problem. Try again.";
                return RedirectToAction("Index", "SettingType");
            }

            // check for duplicate
            var existingCount = _unitOfWork.SettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Count(r => r.Name == vmCreate.Name);
            if (existingCount > 0)
            {
                // display view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Create", vmCreate);
            }

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
            TempData["SuccessMessage"] = "Record created";
            return RedirectToAction("CreateLinkedAssetTypes", "AssetTypeSettingType", new { settingTypeId = dtoSettingType.Id });
        }

        [HttpGet]
        public ViewResult Edit(int id)
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
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Encountered a problem. Try again.";
                return RedirectToAction("Index", "SettingType");
            }

            // check for duplicate
            var existingCount = _unitOfWork.SettingTypes.GetAll()
                .Where(r => r.IsActive)
                .Where(r => r.Name == vmEdit.Name)
                .Count(r => r.Id != vmEdit.Id);
            if (existingCount > 0)
            {
                // display view
                ViewData["ErrorMessage"] = "Record already exists";
                return View("Edit", vmEdit);
            }

            // transfer vm to dto
            var dtoSettingType = _unitOfWork.SettingTypes.Get(vmEdit.Id);
            dtoSettingType.Name = vmEdit.Name;
            dtoSettingType.IsActive = vmEdit.IsActive;

            // update db
            _unitOfWork.SettingTypes.Update(dtoSettingType);
            _unitOfWork.CommitTrans();

            // display view
            TempData["SuccessMessage"] = "Record updated";
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